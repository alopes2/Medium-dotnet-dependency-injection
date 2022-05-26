using DependencyInjection.Core;

namespace DependencyInjection.Services;

public class SingletonWithTransientService : ISingletonWithTransientService
{
    private readonly ITransientService _transientService;

    public int TransientServiceRandomNumber { get => _transientService.Random; }

    public SingletonWithTransientService(ITransientService transientService)
    {
        _transientService = transientService;
    }
}