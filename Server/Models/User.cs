using System;

namespace Server.Models {
    public class User : IModel {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole : byte {
        Teacher = 0,
        Student = 1,
        Administrator = 2
    }
}