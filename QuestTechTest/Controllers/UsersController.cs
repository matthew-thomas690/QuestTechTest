using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTechTest.Mediator;
using QuestTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestTechTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<User>> ListUsers()
        {
            var listUsersRequest = new ListUsersRequest();
            return mediator.Send(listUsersRequest);
        }

        [HttpGet("/{id}")]
        public IActionResult GetUser(int id)
        {
            var getUserRequest = new GetUserRequest() {  Id = id };
            var getUserResponse = mediator.Send(getUserRequest);

            if(getUserResponse.Result == null)
            {
                return NotFound(id);
            }

            return Ok(getUserResponse.Result);
        }

        [HttpPost()]
        public IActionResult AddUser(User user)
        {
            var addUserRequest = new AddUserRequest() { User = user };
            var addUserResponse = mediator.Send(addUserRequest);

            if (addUserResponse.Result.CrudValue == CrudResponse.CrudValues.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, addUserResponse.Result);
            }

            return CreatedAtAction("Get", "Users", new { id = user.Id}, user);

        }

        [HttpPut("/{id}")]
        public IActionResult UpdateUser([FromRoute]int id, [FromBody]User user)
        {
            user.Id = id;
            var updateUserRequest = new UpdateUserRequest() { User = user };
            var updateUserResponse = mediator.Send<CrudResponse>(updateUserRequest);

            if (updateUserResponse.Result.CrudValue == CrudResponse.CrudValues.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, updateUserResponse.Result);
            }
            else if (updateUserResponse.Result.NumberOfRecordsEffected == 0)
            {
                return NotFound(updateUserResponse.Result);
            }

            return Ok(updateUserResponse.Result);

        }


        [HttpDelete("/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleteUserRequest = new DeleteUserRequest() { Id = id };
            var deleteUserResponse = mediator.Send<CrudResponse>(deleteUserRequest);

            if (deleteUserResponse.Result.CrudValue == CrudResponse.CrudValues.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, deleteUserResponse.Result);
            }
            else if (deleteUserResponse.Result.NumberOfRecordsEffected == 0)
            {
                return NotFound(deleteUserResponse.Result);
            }

            return Ok(deleteUserResponse.Result);

        }
    }
}
