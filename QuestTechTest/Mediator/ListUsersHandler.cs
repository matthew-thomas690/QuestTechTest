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
    public class ListUsersHandler : IRequestHandler<ListUsersRequest, IEnumerable<User>>
    {
        private ILiteCollection<User> userCollection;

        public ListUsersHandler(ILiteCollection<User> userCollection)
        {
            this.userCollection = userCollection;
        }

        public Task<IEnumerable<User>> Handle(ListUsersRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(userCollection.FindAll());
        }
    }
}
