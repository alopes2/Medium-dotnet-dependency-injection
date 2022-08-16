using DependencyInjection.Core;

namespace DependencyInjection.Services;

public class MyService : IService
{
    public string GetName()
    {
        return "MyService";
    }
}
