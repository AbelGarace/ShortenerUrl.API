using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortenerUrl.Api.ApplicationCore.Dtos.Auth;
using ShortenerUrl.Api.Infrastructure.Data;
using ShortenerUrl.Api.Infrastructure.Identity;
using ShorternerUrl.Api.Configuration;
using ShorternerUrl.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<JwtSettings>(configuration.GetSection("JWT"));
var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuth(jwtSettings);

ConfigureSwagger.Configure(builder.Services);
ConfigureInfrastructureServices.Configure(builder.Services);
ConfigureCoreServices.Configure(builder.Services);
ConfigureWebServices.Configure(builder.Services);
ConfigureCoreAssemblers.Configure(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShortenerUrl.Api V1");
    });
}


app.UseHttpsRedirection();

app.UseAuth();

app.MapControllers();

app.Run();


async Task SeedData(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await ApplicationDbContextSeed.SeedAsync(dbContext, loggerFactory, userManager);
            await CreateUserRoles(services);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}

async Task CreateUserRoles(IServiceProvider svcProvider)
{
    string[] roleNames = { "User" };
    var RoleManager = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = svcProvider.GetRequiredService<UserManager<ApplicationUser>>();

    //Adding roleNames
    IdentityResult roleResult;
    foreach (var roleName in roleNames)
    {
        //creating the roles and seeding them to the database
        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}