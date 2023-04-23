using FlightSearchApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Infrastructure
{
    public class CodeListRepository : GenericSoftDeleteRepository<CodeList>, ICodeListRepository
    {
        public CodeListRepository() : base()
        {
        }
        public IEnumerable<CodeList> ReadAllActiveOrdered()
        {
            return QueryDbsetActive()
                .OrderBy(s => s.Entity)
                .AsEnumerable();
        }
        public IEnumerable<CodeList> ReadAllForEntity(CodesEnum entity)
        {
            return QueryDbsetActive()
                .Where(s => s.Entity == entity.ToString())
                .OrderBy(s => s.Code)
                .AsEnumerable();
        }
    }
}
