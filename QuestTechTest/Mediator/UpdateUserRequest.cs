using MediatR;
using QuestTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestTechTest.Mediator
{
    public class UpdateUserRequest : IRequest<CrudResponse>
    {
        public User User { get; set; }
    }

}
