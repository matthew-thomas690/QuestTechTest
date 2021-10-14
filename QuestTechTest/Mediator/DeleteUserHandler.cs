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

    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, CrudResponse>
    {
        private ILiteCollection<User> userCollection;

        public DeleteUserHandler(ILiteCollection<User> userCollection)
        {
            this.userCollection = userCollection;
        }

        public Task<CrudResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var crudResponse = new CrudResponse() { CrudValue = CrudResponse.CrudValues.D, Id = request.Id };

            try
            {
                crudResponse.NumberOfRecordsEffected = userCollection.Delete(request.Id) ? 1 : 0;
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
