using AutoMapper;
using StaffManagementAPI.DTOs;
using StaffManagementAPI.Models;

namespace StaffManagementAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StaffDTO, Staff>();
            CreateMap<Staff, GetStaffDTO>();
        }
    }
}