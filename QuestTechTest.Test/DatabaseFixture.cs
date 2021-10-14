using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestTechTest.Test
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            DB = new LiteDatabase(@"questdb.db");
        }

        public LiteDatabase DB { get; private set; }
    }
}
