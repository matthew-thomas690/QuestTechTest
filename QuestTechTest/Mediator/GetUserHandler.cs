using LiteDB;
using MediatR;
using QuestTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestTechTest.Mediator
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, User>
    {
        private ILiteCollection<User> userCollection;

        public GetUserHandler(ILiteCollection<User> userCollection)
        {
            this.userCollection = userCollection;
        }

        Task<User> IRequestHandler<GetUserRequest, User>.Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(userCollection.FindById(request.Id));
        }
    }
}
