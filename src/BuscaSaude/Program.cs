using BuscaSaude.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DatabaseContext>(options => {
        options.UseNpgsql(builder.Configuration["ConnectionStrings:LocalDatabase"]);
    });
}
else
{
    builder.Services.AddDbContext<DatabaseContext>(options => {
        var connectionUri = new Uri(Environment.GetEnvironmentVariable("DATABASE_URL"));
        var credentials = connectionUri.UserInfo.Split(':');

        var connStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Username=credentials[0],
            Password=credentials[1],
            Host=connectionUri.Host,
            Port=connectionUri.Port,
            Database=connectionUri.LocalPath.TrimStart('/'),
            SslMode=SslMode.Prefer,
            TrustServerCertificate=true
        };
        options.UseNpgsql(connStringBuilder.ConnectionString);
    });
}

builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
