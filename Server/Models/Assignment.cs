using System;

namespace Server.Models {
    public class Assignment : IModel {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TimeSpan? DueTime { get; set; }
        public double MaxPoints { get; set; }
        public double Grade { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public long CourseId { get; set; }
        public Course Course { get; set; }
    }
    public enum AssignmentType : byte {
        text = 0,
        file = 1,
    }
}