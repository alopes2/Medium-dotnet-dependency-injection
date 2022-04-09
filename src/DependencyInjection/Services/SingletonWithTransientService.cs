namespace DependencyInjection.Services;

public class SingletonWithTransientService
{
    private readonly TransientService _transientService;

    public int TransientServiceRandomNumber { get => _transientService.Random; }

    public SingletonWithTransientService(TransientService transientService)
    {
        _transientService = transientService;
    }
}