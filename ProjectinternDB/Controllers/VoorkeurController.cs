﻿using Data;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Teamleider")]
    public class VoorkeurController : Controller
    {
        private VoorkeurLogic _voorkeurLogic;
        private TeamLogic _teamLogic = new TeamLogic();

        public VoorkeurController(IVoorkeurContext context)
        {
            _voorkeurLogic = new VoorkeurLogic(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Voorkeur()
        {
            List<Traject> TrajectLijst = new List<Traject>();
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int teamid = _teamLogic.HaalTeamIDOpMetString(id);
            Traject traject = _voorkeurLogic.GetTrajectByID(teamid);
            TrajectLijst.Add(traject);

            TrajectLijst.Insert(0, new Traject { TrajectId = 0, TrajectNaam = "Select" });

            ViewBag.ListOfTraject = TrajectLijst;
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            VoorkeurViewModel VKVmodel = new VoorkeurViewModel();
            VKVmodel.MedewerkerList = _voorkeurLogic.GetDocentenList(User_id);

            return View(VKVmodel);
        }

        public IActionResult VoorkeurUitslag()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(_voorkeurLogic.OphalenVoorkeur(id));
        }

        public JsonResult GetEenheid(int TrajectId)
        {
            List<Eenheid> Eenheden = new List<Eenheid>();

            Eenheden = _voorkeurLogic.GetEenhedenByTrajectId(TrajectId);
            Eenheden.Insert(0, new Eenheid { EenheidId = 0, EenheidNaam = "Select" });

            return Json(new SelectList(Eenheden, "EenheidId", "EenheidNaam"));
        }

        public JsonResult GetOnderdeel(int EenheidId)
        {
            List<Onderdeel> Onderdelen = new List<Onderdeel>();

            Onderdelen = _voorkeurLogic.GetOnderdelenByEenheidId(EenheidId);
            Onderdelen.Insert(0, new Onderdeel { OnderdeelId = 0, OnderdeelNaam = "Select" });

            return Json(new SelectList(Onderdelen, "OnderdeelId", "OnderdeelNaam"));
        }

        public JsonResult GetTaak(int OnderdeelId)
        {
            List<Taak> Taken = new List<Taak>();

            Taken = _voorkeurLogic.GetTakenByOnderdeelId(OnderdeelId);
            Taken.Insert(0, new Taak { TaakId = 0, TaakNaam = "Select" });

            return Json(new SelectList(Taken, "TaakId", "TaakNaam"));
        }

        public JsonResult GetTaakInfo(int TaakId)
        {
            string taakinfo = _voorkeurLogic.GetTaakInfo(TaakId);

            return Json(taakinfo);
        }

        public JsonResult GetTaakTijd(int TaakId)
        {
            int Taaktijd = _voorkeurLogic.GetTaakTijd(TaakId);

            return Json(Taaktijd);
        }

        public IActionResult userInlog(string User_id)
        {
            User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _voorkeurLogic.KrijgUser_id(User_id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult InvoegenVoorkeur(VoorkeurViewModel objTraject, IFormCollection formCollection)
        {
            var persid = HttpContext.Request.Form["Docent"];
            var Prioriteit = HttpContext.Request.Form["Prioriteit"];
            var EenheidId = HttpContext.Request.Form["EenheidId"];
            var OnderdeelId = HttpContext.Request.Form["OnderdeelId"];
            var TaakId = HttpContext.Request.Form["TaakId"];

            string eenheid = EenheidId;
            string onderdeel = OnderdeelId;
            string taak = TaakId;
            string id = persid;

            if (_voorkeurLogic.KijkenVoorDubbel(objTraject.TrajectId, eenheid, onderdeel, taak, id) != true)
            {
                return RedirectToAction("Voorkeur");
            }
            else
            {
                _voorkeurLogic.AddVoorkeur(objTraject.TrajectId, eenheid, onderdeel, taak, id);
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
            _voorkeurLogic.DeleteVoorkeur(id);
            return RedirectToAction("Index");
        }

        public IActionResult PrioriteitGeven(int id)
        {
            VoorkeurViewModel VkModel = new VoorkeurViewModel();
            VkModel.BekwaamheidId = id;
            Voorkeur voorkeur = _voorkeurLogic.GetVoorkeurInfo(id);
            VkModel.Semester = voorkeur.EenheidNaam;
            VkModel.TrajectNaam = voorkeur.TrajectNaam;
            VkModel.Taak_naam = voorkeur.TaakNaam;
            VkModel.Onderdeel_naam = voorkeur.OnderdeelNaam;
            //_voorkeurLogic.BekwaamInfo(id, BKModel);
            //var tupleData = new Tuple<int, Taak>(id, );

            return View(VkModel);
        }

        public IActionResult InvoegenVoorkeurDocent(int prioriteit, int id)
        {
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //int id = VKVmodel.Bekwaam_id;
            _voorkeurLogic.InvoegenTaakVoorkeur(id, prioriteit, User_id);

            return RedirectToAction("VoorkeurUitslag");
        }
    }
}