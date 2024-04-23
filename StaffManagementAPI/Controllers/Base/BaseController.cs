using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StaffManagementAPI.Responses;

namespace StaffManagementAPI.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly T _baseService;
        public BaseController(T baseService)
        {
            _baseService = baseService;
        }
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            var response = new Response<object>()
            {
                Data = value
            };
            return base.Ok(response);
        }
        public override FileStreamResult File(Stream fileStream, string contentType, string fileDownloadName)
        {
            return base.File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileDownloadName}_${DateTime.Now:yyyyMMddhhmmss}");
        }
    }
}