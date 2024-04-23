using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StaffManagementAPI.Data;
using StaffManagementAPI.DTOs;
using StaffManagementAPI.Exceptions;
using StaffManagementAPI.Models;
using StaffManagementAPI.Services.Base;

namespace StaffManagementAPI.Services
{
    public class StaffService : BaseService, IStaffService
    {
        public StaffService(AppDbContext appDbContext, IMapper mapper) : base(appDbContext, mapper)
        {
        }

        public async Task<object> DeleteStaffAsync(int id)
        {
            var staffs = await _appDbContext.Staff
           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id)
           ?? throw new CustomException(HttpStatusCode.NotFound, "Staff was not found");

            staffs.IsDeleted = true;
            staffs.ModifiedBy = "*** SYSTEM ***";
            staffs.ModifiedDateTime = DateTime.Now;

            _appDbContext.Update(staffs);
            await _appDbContext.SaveChangesAsync();

            return null;
        }

        public async Task<List<GetStaffDTO>> GetStaffAsync(SearchStaffDTO search)
        {
            var staffs = await _appDbContext.Staff
            .Where(x =>
                x.IsDeleted == false
                && (search.StaffID.IsNullOrEmpty() || x.StaffID == search.StaffID)
                && (search.Gender == 0 || (int)x.Gender == search.Gender)
                && (search.FromDate == null || x.Birthday.Date == search.FromDate.Value.Date)
                && (search.ToDate == null || x.Birthday.Date == search.ToDate.Value.Date)
            )
            .OrderByDescending(x => x.StaffID)
            .ToListAsync();
            return _mapper.Map<List<GetStaffDTO>>(staffs);
        }

        public async Task<GetStaffDTO> GetStaffByIdAsync(int id)
        {
            var staffs = await _appDbContext.Staff
           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id)
           ?? throw new CustomException(HttpStatusCode.NotFound, "Staff was not found");

            return _mapper.Map<GetStaffDTO>(staffs);
        }

        public async Task<GetStaffDTO> PostStaffAsync(StaffDTO staff)
        {
            if (!staff.IsValid(out string message))
                throw new CustomException(HttpStatusCode.NotAcceptable, message);

            var existed = await _appDbContext.Staff
            .Where(e => e.IsDeleted == false && e.StaffID == staff.StaffID)
            .AnyAsync();

            if (existed)
                throw new CustomException(HttpStatusCode.BadRequest, $"Staff ID '{staff.StaffID}' is already existed");

            var staffs = _mapper.Map<Staff>(staff);
            _appDbContext.Add(staffs);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<GetStaffDTO>(staffs);
        }

        public async Task<GetStaffDTO> PutStaffAsync(int id, StaffDTO staff)
        {
            if (!staff.IsValid(out string message))
                throw new CustomException(HttpStatusCode.NotAcceptable, message);

            var staffs = await _appDbContext.Staff
           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id)
           ?? throw new CustomException(HttpStatusCode.NotFound, "Staff was not found");

            var st = _mapper.Map<StaffDTO, Staff>(staff, staffs);
            st.StaffID = staffs.StaffID;
            st.ModifiedBy = "*** SYSTEM ***";
            st.ModifiedDateTime = DateTime.Now;

            _appDbContext.Update(st);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<GetStaffDTO>(st);
        }

        
    }
}