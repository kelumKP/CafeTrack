namespace CafeTrack.Core.Entities
{

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
    }
}
