using System;

namespace Server.Models {
    public class Course : IModel {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string CreditHours { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Capacity { get; set; }
    }
}