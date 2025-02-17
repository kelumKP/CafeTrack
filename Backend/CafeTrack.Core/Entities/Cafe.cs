namespace CafeTrack.Core.Entities
{
    public class Cafe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
        public ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
    }
}
