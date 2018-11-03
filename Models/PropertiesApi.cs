using System;
using System.Collections.Generic;

namespace J2BIOverseasOps.Models
{
    public class PropertiesApi
    {

        public class Destination
        {
            public int id { get; set; }
            public string iataCode { get; set; }
            public string airportName { get; set; }
        }

        public class Resort
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Property
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ResortId { get; set; }
        }


        public class Restaurant
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class Pool
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class Bar
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class Rooms
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class OtherAdvertisedFacility
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Commitments
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Secured { get; set; }
            public int Guaranteed { get; set; }
            public string Applicability { get; set; }
            public string Period { get; set; }
            public string PeriodDesc  { get; set; }
    }
    }
}
