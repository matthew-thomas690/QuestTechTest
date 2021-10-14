using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestTechTest.Mediator
{
    public class CrudResponse
    {
        public enum CrudValues { Undefined, C, R, U, D, Error }

        public CrudValues CrudValue { get; set; }
        public int NumberOfRecordsEffected { get; set; }

        public int Id { get; set; }
    }
}
