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
    public class AddUserHandler : IRequestHandler<AddUserRequest, CrudResponse>
    {
        private ILiteCollection<User> userCollection;

        public AddUserHandler(ILiteCollection<User> userCollection)
        {
            this.userCollection = userCollection;
        }

        public Task<CrudResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var crudResponse = new CrudResponse() { CrudValue = CrudResponse.CrudValues.C, Id = request.User.Id };
                
            try
            {
                crudResponse.NumberOfRecordsEffected = userCollection.Insert(request.User);
            }
            catch
            {
                crudResponse.CrudValue = CrudResponse.CrudValues.Error;
                crudResponse.NumberOfRecordsEffected = 0;
            }

            return Task.FromResult(crudResponse);
        }
    }
}
