using System;

namespace Server.Models {
    public class UserAssignment : IModel {
        public long Id { get; set; }
        public long AssignmentId { get; set; }
        public long UserId { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
        public string TextContent { get; set; }
        public double? Grade { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Assignment Assignment { get; set; }
        public User User { get; set; }
    }
}