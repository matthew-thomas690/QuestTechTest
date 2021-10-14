using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestTechTest.Mediator
{
    public class DeleteUserRequest : IRequest<CrudResponse>
    {
        public int Id { get; set; }
    }
}
