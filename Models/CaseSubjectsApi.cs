using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2BIOverseasOps.Models
{
    public class CaseSubjectsApi
    {
        public class Identifier
        {
            public string Id { get; set; }
        }

        public class GetCaseSubjectsResponse
        {
            public Identifier CaseId { get; set; }
            public List<Subject> Subjects { get; set; }
        }

        public class Subject
        {
            public Identifier Id { get; set; }
            public string Name { get; set; }
            public SubjectType Type { get; set; }
            public Identifier RelatedBookingId { get; set; }
            public string RelatedBookingRef { get; set; }
        }

        public enum SubjectType
        {
            Destination = 1,
            Resort = 2,
            Property = 3,
            Customer = 4,
            Booking = 5,
        }

    }
}

