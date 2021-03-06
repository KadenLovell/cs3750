using System;
using System.Collections.Generic;

namespace Server.Models {
    public class Course : IModel {
        public long Id { get; set; }
        public long InstructorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int CreditHours { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Capacity { get; set; }
        public List<Assignment> Assignments { get; } = new List<Assignment>();
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
    }
}