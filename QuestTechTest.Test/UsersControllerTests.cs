using LiteDB;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestTechTest.Controllers;
using QuestTechTest.Mediator;
using QuestTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuestTechTest.Test
{
    public class UsersControllerTests : IClassFixture<DatabaseFixture>
    {

        private IServiceProvider serviceProvider;
        private UsersController usersController;
        private ILiteCollection<User> userCollection;
        private User user;
        public UsersControllerTests(DatabaseFixture fixture)
        {
            userCollection = fixture.DB.GetCollection<User>("users");
            serviceProvider = Host.CreateDefaultBuilder() .ConfigureServices((_, services) => services.AddMediatR(typeof(QuestTechTest.Startup)).AddSingleton<ILiteCollection<User>>(userCollection).AddTransient<UsersController>()).Build().Services;
            usersController = serviceProvider.GetService<UsersController>();
            user = new User() { Id = 1, Name = "Leanne Graham", Username = "Bret", Email = "Sincere@april.biz", Phone = "1-770-736-8031 x56442", Website = "hildegard.org" };
            user.Address = new Address() { Street = "Kulas Light", Suite = "Apt. 556", City = "Gwenborough", Zipcode = "92998-3874" };
            user.Address.Geo = new Geo() { Lat = "-37.3159", Lng = "81.1496" };
            user.Company = new Company() { Name = "Romaguera-Crona", CatchPhrase= "Multi-layered client-server neural-net", Bs = "harness real-time e-markets" };
            
        }

        [Fact]
        public void ListTest()
        {
            userCollection.DeleteAll();
            userCollection.Insert(user);
            var okResult = usersController.ListUsers().Result;
            Assert.True(okResult.Count() == 1);
            Assert.True(okResult.Single().Id == user.Id);
        }

        [Fact]
        public void GetTest()
        {
            userCollection.DeleteAll();
            userCollection.Insert(user);
            var okResult = usersController.GetUser(1) as OkObjectResult;
            Assert.True(okResult != null);
            var userResult = okResult.Value as User;
            Assert.True(userResult != null);
            Assert.True(userResult.Id == user.Id);
        }

        [Fact]
        public void UpdateTest()
        {
            userCollection.DeleteAll();
            userCollection.Insert(user);

            var updatedName = "test name";
            user.Name = updatedName;
            user.Id = 0;

            var okResult = usersController.UpdateUser(1, user) as OkObjectResult;
            Assert.True(okResult != null);
            var updatedResult = okResult.Value as CrudResponse;
            Assert.True(updatedResult.CrudValue == CrudResponse.CrudValues.U);
            Assert.True(updatedResult.NumberOfRecordsEffected == 1);
            var updatedUser = userCollection.FindById(1);
            Assert.True(updatedUser.Name == updatedName);

            userCollection.DeleteAll();

            var notFoundResult = usersController.UpdateUser(1, user) as NotFoundObjectResult;
            Assert.True(notFoundResult != null);
            updatedResult = notFoundResult.Value as CrudResponse;
            Assert.True(updatedResult.NumberOfRecordsEffected == 0);

        }

        [Fact]
        public void DeleteTest()
        {
            userCollection.DeleteAll();
            userCollection.Insert(user);
            var okResult = usersController.DeleteUser(1) as OkObjectResult;
            Assert.True(okResult != null);
            var deleteResult = okResult.Value as CrudResponse;
            Assert.True(deleteResult != null);
            Assert.True(deleteResult.CrudValue == CrudResponse.CrudValues.D);
            Assert.True(deleteResult.NumberOfRecordsEffected == 1);
            Assert.True(userCollection.Count() == 0);

            var notFoundResult = usersController.DeleteUser(1) as NotFoundObjectResult;
            Assert.True(notFoundResult != null);
            deleteResult = notFoundResult.Value as CrudResponse;
            Assert.True(deleteResult.NumberOfRecordsEffected == 0);
        }

        [Fact]
        public void AddTest()
        {
            userCollection.DeleteAll();
            var createdAtActionResult = usersController.AddUser(user) as CreatedAtActionResult;
            Assert.True(createdAtActionResult != null);
            Assert.True(userCollection.Count() == 1);

            var errorObjectResult = usersController.AddUser(user) as ObjectResult;
            Assert.True(errorObjectResult.StatusCode == 500);
        }



    }
}
 