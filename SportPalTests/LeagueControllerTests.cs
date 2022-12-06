using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportPal.Controllers;
using SportPal.Data;
using System;


namespace SportPalTests
{
    [TestClass]
    public class LeagueControllerTests
    {
        // variables for all tests
        private ApplicationDbContext context;
        LeaguesController controller;

        // this method will run first before every test
        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            context = new ApplicationDbContext(options);

            var controller = new LeaguesController(context);
        }

        [TestMethod]
        public void IndexLoadsView()
        {
            // act

            // assert

        }
    }
}