using System;

namespace Server.Models {
    public class UserCourses : IModel {
        public long Id { get; set; }
        public long CourseID { get; set; }
        public long UserID { get; set; }
        public int CreditHours { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Course Course {get;set;}
    }
}