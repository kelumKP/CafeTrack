namespace CafeTrack.Core.Entities
{

        public class Employee
        {
            public string Id { get; set; } // Format: UIXXXXXXX
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Gender { get; set; } // Enum: Male/Female
            public DateTime? StartDate { get; set; } // Add StartDate to track when the employee joined
            public ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
        }
}
