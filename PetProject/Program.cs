using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using BLL.Interfaces;
using BLL.AutoMapper;
using BLL.Services;
using System.Text.Json.Serialization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ITorgDBContext, TorgDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOFWork>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITradingPointRepository, TradingPointRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IWorkRegionRepository, WorkRegionRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITradingPointService, TradingPointService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IWorkRegionService, WorkRegionService>();
builder.Services.AddTransient<IFtpService, FtpService>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://your-frontend-domain.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();




using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TorgDBContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
