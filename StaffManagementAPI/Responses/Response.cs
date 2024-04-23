using System.Net;
using System.Text.Json;
using StaffManagementAPI.Extensions;
namespace StaffManagementAPI.Responses;

public class Response<T>
{
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    public string Status
    {
        get
        {
            return Code == HttpStatusCode.OK ? "SUCCESS" : Code.ToEnumString();
        }
        set
        {
            value = Status;
        }
    }
    public string Message { get; set; } = "The request was successful";
    public T Data { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}