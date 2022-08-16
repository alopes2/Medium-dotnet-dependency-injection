using System.Reflection;
using DependencyInjection.Core;
using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddSingleton<ISingletonWithTransientService, SingletonWithTransientService>();


builder.Services.AddTransient<IService, MyService>();
builder.Services.AddTransient<IService, AnotherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/transient", (ITransientService service, ITransientService secondService) =>
{
    var first = service.Random;
    var second = secondService.Random;

    var result = new {
        FirstTransient = service.Random,
        SecondTransient = secondService.Random,
        IsEqual = first == second
    };

    return result;
})
.WithName("GetTransient");

app.MapGet("/scoped", (IScopedService service, IScopedService secondService) =>
{
    var first = service.Random;
    var second = secondService.Random;

    var result = new {
        FirstScoped = service.Random,
        SecondScoped = secondService.Random,
        IsEqual = first == second
    };

    return result;
})
.WithName("GetScoped");

app.MapGet("/singleton", (ISingletonService service) =>
{
    return service.Random;
})
.WithName("GetSingleton");

app.MapGet("/transient-singleton", (ISingletonWithTransientService service) =>
{
    return service.TransientServiceRandomNumber;
})
.WithName("GetSingletonWithTransient");

app.MapGet("/all", (
    ITransientService firstTransient,
    ITransientService secondTransient,
    IScopedService firstScoped,
    IScopedService secondScoped,
    ISingletonService singleton,
    ISingletonWithTransientService singletonWithTransientService) =>
{
    var result = new {
        FirstTransient = firstTransient.Random,
        SecondTransient = secondTransient.Random,
        FirstScoped = firstScoped.Random,
        SecondScoped = secondScoped.Random,
        Singleton = singleton.Random,
        SingletonWithTransient = singletonWithTransientService.TransientServiceRandomNumber
    };

    return result;
})
.WithName("GetAll");

app.MapGet("/multiple-injection", (IEnumerable<IService> services) =>
{
    return services.Select(s => s.GetName());
})
.WithName("GetMultipleInjection");

app.MapGet("/myservice-injection", (IEnumerable<IService> services) =>
{
    var myService = services.FirstOrDefault(s => Type.Equals(s.GetType(), typeof(MyService)));
    return myService.GetName();
})
.WithName("GetMyServiceInjection");

app.MapGet("/anotherservice-injection", (IEnumerable<IService> services) =>
{
    var myService = services.FirstOrDefault(s => Type.Equals(s.GetType(), typeof(AnotherService)));
    return myService.GetName();
})
.WithName("GetAnotherServiceInjection");



app.Run();