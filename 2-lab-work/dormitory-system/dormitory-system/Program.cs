using dormitory_system.Models;
using dormitory_system.Repositories;
using dormitory_system.Repositories.Interfaces;
using dormitory_system.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

using IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<NpgsqlDataSource>(_ => NpgsqlDataSource.Create(configuration["ConnectionString"]!));
        
        services.AddTransient<IDormitoryRepository, DormitoryRepository>();
        services.AddTransient<IFacultyRepository, FacultyRepository>();
        services.AddTransient<IFacultyDormitoryRelationRepository, FacultyDormitoryRelationRepository>();
        services.AddTransient<IMaintenanceRepository, MaintenanceRepository>();
        services.AddTransient<IResidenceRepository, ResidenceRepository>();
        services.AddTransient<IRoomRepository, RoomRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();

        services.AddTransient<FacultyDormitoryRelationService>();
        services.AddTransient<ResidenceService>();
        services.AddTransient<InputService>();
        
        services.AddTransient<MainService>();

        
    })
    .Build();

MainService mainService = ActivatorUtilities.CreateInstance<MainService>(host.Services);
await mainService.Run();