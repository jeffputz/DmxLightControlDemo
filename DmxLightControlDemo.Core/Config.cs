using Microsoft.Extensions.Configuration;

namespace DmxLightControlDemo.Core;

public interface IConfig
{
    string LocalIP { get; }
    string LocalSubnetMask { get; }
}

public class Config : IConfig
{
    private readonly IConfiguration _configuration;

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string LocalIP => _configuration["LocalIP"] ?? string.Empty;
    public string LocalSubnetMask => _configuration["LocalSubnetMask"] ?? string.Empty;
}