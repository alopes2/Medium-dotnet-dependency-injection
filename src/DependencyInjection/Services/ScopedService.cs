using DependencyInjection.Core;

namespace DependencyInjection.Services;

public class ScopedService : BaseService, IScopedService
{
    public ScopedService()
        : base()
    { }
}