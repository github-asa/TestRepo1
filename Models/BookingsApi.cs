using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace J2BIOverseasOps.Models
{
    public class BookingsApi
    {
        public class BookingsRoot
        {
            public int TotalMatches { get; set; }

            public bool RestrictedByPermittedDestinations { get; set; }

            public Booking[] Bookings { get; set; }

            public object Page { get; set; }
        }

        public class Customer
        {
            public Id Id { get; set; }

            public string Name { get; set; }

            public DateTimeOffset DateOfBirth { get; set; }
        }

        public class Booking
        {
            public Id Id { get; set; }

            public string BookingRef { get; set; }

            public string LeadCustomerName { get; set; }

            public string Outbound { get; set; }

            public string Destination { get; set; }

            public string Resort { get; set; }

            public string Property { get; set; }

            public bool HasCreatedCases { get; set; }

            public Customer[] Customers { get; set; }
        }
    }

        public partial class Id
        {
            public Guid IdId { get; set; }
        }
    
}