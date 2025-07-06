using Application.Common.Interface;
using Application.Injection;
using Infrastructure.Injection;
using Infrastructure.JWT;
using Infrastructure.Presistence;
using Linkload.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTransient<IToken, TokenService>();
builder.Services.AddSingleton<ICurrentUser,CurrenUserService>();
builder.Services.AddDbContext<MainDbContext>(e => e.UseNpgsql(builder.Configuration.GetConnectionString("MainDb")));
builder.Services.AddSwaggerGen(q => {
    q.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });
    q.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }

        },
        new string[] { }
        }
    });
    });
var key = "54258dbe-8235-4g2a-w2hg-60q102134541";
var tokenOptions = new TokenOptions("ISS", "AUD", key);
builder.Services.AddSingleton(tokenOptions);
var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    db.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
