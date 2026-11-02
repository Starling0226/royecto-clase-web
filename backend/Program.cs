using BackEnd.Data;
using BackEnd.Modules.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
  options.AddPolicy("FrontEnd", (policy) => {
    policy
    .WithOrigins("http://localhost:5173")
    .AllowAnyMethod()
    .AllowAnyHeader();
  });
});
builder.Services.AddControllers();
builder.Services.AddAuthModule();

builder.Services.AddDbContext<AppDbContext>((option) => {
  option.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnection"));
});

var app = builder.Build();

app.UseCors("FrontEnd");
app.MapControllers();
app.Run("http://localhost:3000");
