﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Logic;
using ProjectinternDB.Models;
using System.Security.Claims;
using Model.Onderwijsdelen;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Microsoft.AspNetCore.Http;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Teamleider")]
    public class VoorkeurController : Controller
    {
        private VoorkeurLogic _voorkeurLogic;

        public VoorkeurController(IVoorkeurContext context)
        {
            _voorkeurLogic = new VoorkeurLogic(context);
        }

        public IActionResult Index()
        {
            return View();
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
            List<Onderdeel  > Onderdelen = new List<Onderdeel>();
 
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

        public IActionResult userInlog(string User_id)
        {
            User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _voorkeurLogic.krijgUser_id(User_id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult InvoegenVoorkeur(VoorkeurViewModel objTraject, IFormCollection formCollection)
        {
            var Prioriteit = HttpContext.Request.Form["Prioriteit"];
            var EenheidId = HttpContext.Request.Form["EenheidId"];
            var OnderdeelId = HttpContext.Request.Form["OnderdeelId"];
            var TaakId = HttpContext.Request.Form["TaakId"];
            int prioriteit = Convert.ToInt32(Prioriteit);
            string eenheid = EenheidId;
            string onderdeel = OnderdeelId;
            string taak = TaakId;
            _voorkeurLogic.AddVoorkeur(objTraject.TrajectId, eenheid, onderdeel, taak, prioriteit, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index");
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
    }
}