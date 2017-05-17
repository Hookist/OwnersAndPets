using Microsoft.VisualStudio.TestTools.UnitTesting;
using OwnersProject.Controllers;
using OwnersProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace OwnersProject.Tests
{
    [TestClass]
    public class TestOwnerController
    {
        [TestMethod]
        public async Task GetReturnsOwner()
        {
            // Arrange
            var controller = new OwnerController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:16547/api/Owner/")
            };
            controller.Configuration = new HttpConfiguration();

            // Act
            var responce = await controller.Get(controller.Request, 5);

            // Assert
            Owner owner = new Owner();
            Assert.IsTrue(responce.TryGetContentValue<Owner>(out owner));
            Assert.AreEqual(5, owner.Id);
        }

        [TestMethod]
        public async Task GetReturnsOwners()
        {
            // Arrange
            var controller = new OwnerController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:16547/api/Owner")
            };
            controller.Configuration = new HttpConfiguration();

            // Act
            var responce = await controller.Get(new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:16547/api/Owner")
            });

            // Assert
            IEnumerable<Owner> owners = new List<Owner>();
            Assert.IsTrue(responce.TryGetContentValue<IEnumerable<Owner>>(out owners));
            Assert.AreEqual(5, owners.Count());
        }

        [TestMethod]
        public async void PostInsertOwner()
        {
            // Arrange
            var controller = new OwnerController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:16547/api/Owner")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "owner" } });


            //Act
            Owner owner = new Owner() { Id = 42, Name = "Owner1" };
            var response = await controller.Post(controller.Request, owner);

            //Assert 
            Assert.AreEqual("http://localhost:16547/api/Owner/42", response.Headers.Location.AbsoluteUri);

        }

        [TestMethod]
        public async void PostUpdateOwner()
        {
            // Arrange
            var controller = new OwnerController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:16547/api/Owner")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "owner" } });


            //Act
            Owner owner = new Owner() { Id = 42, Name = "Owner1" };
            var response = await controller.Put(controller.Request, owner.Id, owner);

            //Assert 
            Assert.AreEqual("http://localhost:16547/api/Owner/42", response.Headers.Location.AbsoluteUri);

        }


    }
}
