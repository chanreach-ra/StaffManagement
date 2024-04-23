using StaffManagementAPI.DTOs;
using StaffManagementAPI.Models;

namespace StaffManagementAPI.Services
{
    public interface IStaffService
    {
        public Task<List<GetStaffDTO>> GetStaffAsync(SearchStaffDTO search);
        public Task<GetStaffDTO> GetStaffByIdAsync(int id);
        public Task<GetStaffDTO> PostStaffAsync(StaffDTO staff);
        public Task<GetStaffDTO> PutStaffAsync(int id, StaffDTO staff);
        public Task<object> DeleteStaffAsync(int id);
    }
}