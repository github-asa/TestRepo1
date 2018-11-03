using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace J2BIOverseasOps.Models
{
    public class CaseAutoAssigneesApi
    {

        public class GetUsersAutoAssignedToCaseResponse
        {
            public Identifier CaseId { get; set; }
            public List<Identifier> AutoAssignedUsers { get; set; }
        }

        public class Identifier
        {
            public string Id { get; set; }
        }

    }
}
