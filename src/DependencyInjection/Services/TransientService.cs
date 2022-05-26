using DependencyInjection.Core;

namespace DependencyInjection.Services;

public class TransientService : BaseService, ITransientService
{
    public TransientService()
        : base()
    { }
}