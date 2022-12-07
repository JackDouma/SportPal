using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public void DetailsNoTeamsTableLoads404()
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
        public void DetailsValidIdLoadsTeam()
        {
            // act
            var result = (ViewResult)controller.Details(70).Result;

            // assert 
            Assert.AreEqual(context.Standing.Find(70), result.Model);
        }
        #endregion
        //controller.ModelState.AddModelError("put a descriptive key name here", 60);
        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            var result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }

        #region "Edit"
        [TestMethod]
        public void EditNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditNoTeamsTableLoads404()
        {
            // arrange
            context.Standing = null;

            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(20).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Edit(70).Result;

            // assert 
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsTeam()
        {
            // act
            var result = (ViewResult)controller.Edit(70).Result;

            // assert 
            Assert.AreEqual(context.Standing.Find(70), result.Model);
        }
        #endregion

        #region "Delete"
        [TestMethod]
        public void DeleteNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteNoTeamsTableLoads404()
        {
            // arrange
            context.Standing = null;

            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(20).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(70).Result;

            // assert 
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsTeam()
        {
            // act
            var result = (ViewResult)controller.Delete(70).Result;

            // assert 
            Assert.AreEqual(context.Standing.Find(70), result.Model);
        }
        #endregion
        [TestMethod]
        public void DeleteConfirmedDeletesTeam()
        {
            // act
            var result = (ViewResult)controller.DeleteConfirmed(70).Result;

            // assert 
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}