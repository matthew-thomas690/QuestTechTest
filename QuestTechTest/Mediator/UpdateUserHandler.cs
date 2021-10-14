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
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, CrudResponse>
    {
        private ILiteCollection<User> userCollection;

        public UpdateUserHandler(ILiteCollection<User> userCollection)
        {
            this.userCollection = userCollection;
        }

        public Task<CrudResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var crudResponse = new CrudResponse() { CrudValue = CrudResponse.CrudValues.U, Id = request.User.Id };

            try
            {
                crudResponse.NumberOfRecordsEffected = userCollection.Update(request.User) ? 1 : 0;
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
