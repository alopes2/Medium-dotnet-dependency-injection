using DependencyInjection.Core;

namespace DependencyInjection.Services;

public class AnotherService : IService
{
    public string GetName()
    {
        return "AnotherService";
    }
}
