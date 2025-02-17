namespace CafeTrack.Core.Entities
{
    public class EmployeeCafe
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } // Navigation property
        public Guid CafeId { get; set; }
        public Cafe Cafe { get; set; } // Navigation property
    }
}
