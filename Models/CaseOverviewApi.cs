using System;
using System.Collections.Generic;
using System.Linq;


namespace J2BIOverseasOps.Models
{
    public class CaseOverviewApi
    {
        public class Identifier
        {
            public string Id { get; set; }
        }

        public class GetCaseOverviewResponse
        {
            public Identifier CaseId { get; set; }
            public CaseOverview CaseOverview { get; set; }
        }

        public class CaseOverview
        {
            public Identifier CaseId { get; set; }
            public string CaseReference { get; set; }
            public Identifier StateMode { get; set; }
            public string Notes { get; set; }
            public List<Identifier> AffectedSubjects { get; set; }
            public string BookingReference { get; set; }
            public List<Subject> Subjects { get; set; }
            public List<CaseOverviewCategoryModel> Categories { get; set; }
            public string InitialSummary { get; set; }
            public IEnumerable<string> Destinations { get; set; }
            public CaseOverviewReportedFromModel ReportedFrom { get; set; }
            public List<Identifier> Attachments { get; set; }
            public List<UserAssignee> Assignees { get; set; }
            public Identifier Id { get; set; }
        }

        public class UserAssignee
        {
            public Identifier Id { get; set; }
            public bool IsAutoAssigned { get; set; }
        }

        public class CaseOverviewCategoryModel
        {
            public Identifier CategoryId { get; set; }
            public string Name { get; set; }
            public bool IsParent => Children?.Any() ?? false;
            public CaseOverviewCategoryModel[] Children { get; set; }
        }

        public class CaseOverviewReportedFromModel
        {
            public ReportedByType ReportedByType { get; set; }
            public Identifier ReportedById { get; set; }
            public string ReportedByName { get; set; }
            public string ReportedByDepartmentName { get; set; }
            public string ReportedByOtherDescription { get; set; }
            public string OtherDepartmentName { get; set; }
        }

        public enum ReportedByType
        {
            Invalid,
            Customer,
            Colleague,
            Other,
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

        public class NoteModel
        {
            public Identifier NoteId { get; set; }
            public DateTime CreatedOn { get; set; }
            public string Note { get; set; }
        }



        /*
public class CaseNoteModel
{
    public Identifier CaseId { get; set; }
    public NoteModel Note { get; set; }
    public CaseNoteModel(Identifier caseId)
    {
        Id = caseId;
        CaseId = caseId;
    }
    public Identifier Id { get; set; }
}
*/

        //##################################################################################
        /*
        public class Identifier
        {
            public string Id { get; set; }
        }
        public class GetCaseOverviewResponse
        {
            public Identifier CaseId { get; set; }
            public CaseOverview CaseOverview { get; set; }
        }
        public class CaseOverview
        {
            public Identifier CaseReference { get; set; }
            public Identifier StateMode { get; set; }
            public List<Identifier> AffectedSubjects { get; set; }
            public Identifier ReportedBy { get; set; }
            public Identifier ReportedByDepartment { get; set; }
            public string ReportedByTypeName { get; set; }
            public string BookingReference { get; set; }
            public List<Subject> Subjects { get; set; }
            public List<Category> Categories { get; set; }
            public string InitialSummary { get; set; }
            public string Notes { get; set; }
            public IEnumerable<string> Destinations { get; set; }
            public IEnumerable<string> Properties { get; set; }
        }
        public class Subject
        {
            public Identifier SubjectId { get; set; }
            public string Reference { get; set; }
            public string Name { get; set; }
        }
        public class Category
        {
            public Identifier CategoryId { get; set; }
            public string Name { get; set; }
        }
//##################################################################################


        public class Identifier
        {
            public string Id { get; set; }
        }

        public class GetCaseOverviewResponse
        {
            public Identifier CaseId { get; set; }
            public CaseOverview CaseOverview { get; set; }
        }

        public class CaseOverview
        {
            public Identifier CaseId { get; set; }
            public Identifier CaseReference { get; set; }
            public Identifier StateMode { get; set; }
            public CaseNoteModel Notes { get; set; }
            public List<Identifier> AffectedSubjects { get; set; }
            public Identifier ReportedBy { get; set; }
            public Identifier ReportedByDepartment { get; set; }
            public ReportedByType ReportedByType { get; set; }
            public string ReportedByTypeName => ReportedByType.ToString();
            public string ReportedByOtherDescription { get; set; }
            public string BookingReference { get; set; }
            public List<SubjectModel> Subjects { get; set; }
            public List<CaseOverviewCategoryModel> Categories { get; set; }
            //public List<SituationActionModel> Actions { get; set; }
            public string InitialSummary { get; set; }
            public IEnumerable<string> Destinations { get; set; }
        }

        public class SubjectModel
        {
            public string Name { get; set; }
            public SubjectType Type { get; set; }
            public BookingModel RelatedBooking { get; set; }
        }
        
        public enum SubjectType
        {
            Destination = 1,
            Resort = 2,
            Property = 3,
            Customer = 4,
            Booking = 5,
        }

        public class BookingModel
        {
            public string BookingRef { get; }
            public BookingModel(Identifier id, string bookingRef)
            {
                Id = id;
                BookingRef = bookingRef;
            }
            public Identifier Id { get; set; }
        }

        public class CaseOverviewCategoryModel
        {
            public Identifier CategoryId { get; set; }
            public string Name { get; set; }
            public bool IsParent => Children?.Any() ?? false;
            public CaseOverviewCategoryModel[] Children { get; set; }
        }

        public enum ReportedByType
        {
            Invalid = 0,
            Customer,
            Colleague,
            Other
        }

        public class CaseNoteModel
        {
            public Identifier CaseId { get; set; }
            public NoteModel Note { get; set; }
            public CaseNoteModel(Identifier caseId)
            {
                Id = caseId;
                CaseId = caseId;
            }
            public Identifier Id { get; set; }
        }
        public class NoteModel
        {
            public Identifier NoteId { get; set; }
            public DateTime CreatedOn { get; set; }
            public string Note { get; set; }
        }

        */
    }

}


