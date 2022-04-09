using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddSingleton<SingletonWithTransientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/transient", (TransientService service, TransientService secondService) =>
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

app.MapGet("/scoped", (ScopedService service, ScopedService secondService) =>
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

app.MapGet("/singleton", (SingletonService service) =>
{
    return service.Random;
})
.WithName("GetSingleton");

app.MapGet("/transient-singleton", (SingletonWithTransientService service) =>
{
    return service.TransientServiceRandomNumber;
})
.WithName("GetSingletonWithTransient");

app.MapGet("/all", (
    TransientService firstTransient,
    TransientService secondTransient,
    ScopedService firstScoped,
    ScopedService secondScoped,
    SingletonService singleton,
    SingletonWithTransientService singletonWithTransientService) =>
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

app.Run();