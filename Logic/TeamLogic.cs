﻿using Data;
using System;
using Model;
using System.Collections.Generic;
using Data.Context;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Logic
{
  public  class TeamLogic
    {
        private static TeamRepository TeamRepo;

        public TeamLogic()
        {
            TeamRepo = new TeamRepository(new TeamsqlContext());
        }

        public TeamLogic(ITeamContext context)
        {
            TeamRepo = new TeamRepository(context);
        }
        public List<Team> TeamsOphalen()
        {
         
            return TeamRepo.Teams;
        }

        public List<Taak> GetTaken(int docentid)
        {
            return TeamRepo.Taken(docentid);
        }

        public List<Docent> DocentenOphalen(int teamid)
        {
            return TeamRepo.DocentenInTeamOphalen(teamid);
        }
        public void VoegDocentToeAanTeam(int DocentID, int TeamID)
        {
            TeamRepo.VoegDocentToeAanTeam(DocentID, TeamID);
        }
        public void VerwijderDocentUitTeam(int docentid)
        {
            TeamRepo.VerwijderDocentUitTeam(docentid);
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            return TeamRepo.TeamleiderNaamMetTeamleiderId(teamleiderId);
        }
        public Team TeamOphalenMetID(int ID)
        {
            return TeamRepo.TeamOphalenMetID(ID);
        }
        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            return TeamRepo.CurriculumEigenaarNaamMetCurriculumEigenaarId(curriculumeigenaarId);
        }

        public int HaalTeamIDOpMetString(string id)
        {
            return TeamRepo.HaalTeamIdOpMetIDString(id);
        }

        public List<Docent> HaalDocentenZonderTeamOp()
        {
            return TeamRepo.HaalDocentenZonderTeamOp();
        }

        public Docent HaalDocentOpMetID(int id)
        {
            return TeamRepo.HaalDocentOpMetID(id);
        }

        public List<Taak> HaalTakenOpVoorTeamleider(string medewerkerid)
        {
            return TeamRepo.HaalTakenOpVoorTeamleider(medewerkerid);
        }
    }
}