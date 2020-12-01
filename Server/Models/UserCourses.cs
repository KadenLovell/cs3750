using System;

namespace Server.Models {
    public class UserCourses : IModel {
        public long Id { get; set; }
        public string CourseID { get; set; }
        public long UserID { get; set; }
        public int CreditHours { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}