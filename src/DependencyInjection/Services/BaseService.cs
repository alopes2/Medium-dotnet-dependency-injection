namespace DependencyInjection.Services;

public class BaseService
{
    private readonly int _randomNumber;

    public int Random { get => _randomNumber; }

    public BaseService()
    {
        _randomNumber = new Random().Next();
    }
}