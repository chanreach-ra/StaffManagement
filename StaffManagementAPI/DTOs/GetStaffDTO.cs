using StaffManagementAPI.Extensions;
using StaffManagementAPI.Models;

namespace StaffManagementAPI.DTOs
{
    public class GetStaffDTO
    {
        public int Id { get; set; }
        public string StaffID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public GenderType Gender { get; set; }
        public string Genders
        {
            get
            {
                return Gender.ToEnumString();
            }
        }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }


    }
}