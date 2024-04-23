namespace StaffManagementAPI;

public class AppConfig
{
    private readonly IConfiguration _configuration;
    public AppConfig()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(basePath: AppContext.BaseDirectory).AddJsonFile("appsettings.json");
        _configuration = builder.Build();
    }
    public IConfiguration GetConfiguration()
    {
        return _configuration;
    }
}
