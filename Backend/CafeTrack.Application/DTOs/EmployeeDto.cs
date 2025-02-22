namespace CafeTrack.Application.DTOs
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int DaysWorked { get; set; } 
        public string CafeId { get; set; } 
    }

}
