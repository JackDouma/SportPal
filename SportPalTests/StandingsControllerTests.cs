using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportPal.Controllers;
using SportPal.Data;
using SportPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportPalTests
{
    [TestClass]
    public class StandingsControllerTests
    {
        // variables for all tests
        private ApplicationDbContext context;
        StandingsController controller;

        // this method will run first before every test
        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            context = new ApplicationDbContext(options);

            var league = new League { LeagueId = 10, Name = "NHL", Sport = "Hockey", Organizer = "Gary" };
            context.Add(league);

            for (var x = 50; x < 75; x++)
            {
                var team = new Standing { StandingId = x, Team = "Team " + x.ToString(), Coach = "Mike", Points = 20, Wins = 9, Losses = 4, Ties = 2, League = league };
                context.Add(team);
            }

            context.SaveChanges();

            controller = new StandingsController(context);
        }

        [TestMethod]
        public void IndexLoadsView()
        {
            // act
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexLoadsStandings()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Standing> model = (List<Standing>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.Standing.ToList(), model);
        }

        #region "Details"
        [TestMethod]
        public void DetailsNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsNoProductsTableLoads404()
        {
            // arrange
            context.Standing = null;

            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(20).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Details(70).Result;

            // assert 
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsProduct()
        {
            // act
            var result = (ViewResult)controller.Details(70).Result;

            // assert 
            Assert.AreEqual(context.Standing.Find(70), result.Model);
        }
        #endregion
    }
}