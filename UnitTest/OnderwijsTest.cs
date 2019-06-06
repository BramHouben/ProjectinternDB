﻿using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Onderwijsdelen;

namespace UnitTest
{
    [TestClass]
    public class OnderwijsTest
    {
        OnderwijsLogic onderwijsLogic;
        [TestInitialize]
        public void Init()
        {
            onderwijsLogic = new OnderwijsLogic();
        }

        [TestMethod]
        public void Onderwijstaaknaam()
        {
            // arrange

            //act
            string taaknaam = onderwijsLogic.OnderwijstaakNaam(1);
            //assert
            Assert.AreEqual("LP-Coach", taaknaam);
        }

        [TestMethod]
        public void TakenOphalen()
        {
            // arrange

            //act

            //assert
            Assert.IsTrue(onderwijsLogic.TakenOphalen() != null);
        }

        [TestMethod]
        public void TaakToevoegen()
        {
            //Arrange
            Taak taak = new Taak(66, "Onderwijstaak");
            Taak taak2 = new Taak(1, "Test");// bestaat al 
            //act
            onderwijsLogic.TaakAanmaken(taak);
            onderwijsLogic.TaakAanmaken(taak2);
            //assert
            Assert.IsTrue(onderwijsLogic.TaakOphalen(66) != null);
            Assert.IsTrue(onderwijsLogic.TaakOphalen(1).TaakNaam != "Test");
        }

        [TestMethod]
        public void Taakverwijderen()
        {
            //Arrange

            //Act
            onderwijsLogic.TaakVerwijderen(1);
            //Assert
            Assert.AreEqual(null,onderwijsLogic.TaakOphalen(1));
        }

        [TestMethod]
        public void UpdateTaak()
        {
            //Arrange
            Taak taak = new Taak {Omschrijving = "TestUpdate",AantalKlassen = 22,BenodigdeUren = 120, EenheidNaam = "TestUpdateEeenheidNaam",OnderdeelId = 1,OnderdeelNaam = "TestUpdateOnderdeelNaam",TaakId = 1,TaakNaam = "TestUpdateTaakNaam",TrajectNaam = "TestUpdateTrajectNaam"};
            //Act
            BlokeigenaarLogic blokeigenaarLogic = new BlokeigenaarLogic();
            blokeigenaarLogic.UpdateTaak(taak);
            //Assert
            Assert.AreEqual(onderwijsLogic.TaakOphalen(1).Omschrijving,taak.Omschrijving);
            Assert.AreEqual(onderwijsLogic.TaakOphalen(1).TaakNaam, taak.TaakNaam);
            Assert.AreEqual(onderwijsLogic.TaakOphalen(1).BenodigdeUren, taak.BenodigdeUren);
            Assert.AreEqual(onderwijsLogic.TaakOphalen(1).AantalKlassen, taak.AantalKlassen);
            Assert.AreEqual(onderwijsLogic.TaakOphalen(1).OnderdeelNaam, taak.OnderdeelNaam);
        }
}
}