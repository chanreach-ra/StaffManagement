namespace StaffManagementAPI.DTOs
{
    public class SearchStaffDTO
    {
        public string StaffID { get; set; }
        public int Gender { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
        
    }
}