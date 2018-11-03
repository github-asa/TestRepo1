using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenQA.Selenium.Html5;

namespace J2BIOverseasOps.Models
{
    public class DataFeedEmployees
    {
        [JsonProperty("Employees")]
        public Employees Employees { get; set; }
    }

    public class Employees
    {
        [JsonProperty("@xmlns:xsi")]
        public Uri XmlnsXsi { get; set; }

        [JsonProperty("@xmlns:xsd")]
        public Uri XmlnsXsd { get; set; }

        [JsonProperty("Employee")]
        public List<Employee> Employee { get; set; }
    }

    public class Employee
    {
        [JsonProperty("@EmployeeId")]
       // [JsonConverter(typeof(ParseStringConverter))]
        public int EmployeeId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Rank")]
        public string Rank { get; set; }

        [JsonProperty("Department")]
        public string Department { get; set; }

        [JsonProperty("EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("StartDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("ContinuousServiceStartDate")]
        public DateTimeOffset ContinuousServiceStartDate { get; set; }

        [JsonProperty("JobRecords")]
        public JobRecords JobRecords { get; set; }
    }

    public class JobRecords
    {
        [JsonProperty("Job")]
        public Job Job { get; set; }
    }

    public class Job
    {
        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("JobStartDate")]
        public DateTimeOffset JobStartDate { get; set; }
    }
    //    public class DataFeedEmployees
    //    {
    //        public Employees Employees { get; set; }
    //    }
    //    public  class Employees
    //    {
    //        public Employee[] Employee { get; set; }
    //    }
    //
    //    public class Employee
    //    {
    //        public int EmployeeId { get; set; }
    //        public string UserName { get; set; }
    //        public string FirstName { get; set; }
    //        public string LastName { get; set; }
    //        public string Rank { get; set; }
    //        public string Department { get; set; }
    //        public string EmailAddress { get; set; }
    //        public string PhoneNumber { get; set; }
    //        public DateTimeOffset StartDate { get; set; }
    //        public DateTimeOffset ContinuousServiceStartDate { get; set; }
    //        public string JobRecords { get; set; }
    //    }

}

