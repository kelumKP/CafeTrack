using AutoMapper;
using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;

namespace CafeTrack.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map Employee to EmployeeDto
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DaysWorked, opt => opt.MapFrom(src => CalculateDaysWorked(src.StartDate)))
                .ForMember(dest => dest.CafeId, opt => opt.MapFrom(src => src.EmployeeCafes.FirstOrDefault().CafeId.ToString()));
        }

        private static int CalculateDaysWorked(DateTime? startDate)
        {
            if (!startDate.HasValue)
                return 0;

            return (DateTime.UtcNow - startDate.Value).Days;
        }
    }
}
