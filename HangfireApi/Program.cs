using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "workstation id=rifki123.mssql.somee.com;packet size=4096;user id=rifkisururi_SQLLogin_1;pwd=12344321;data source=rifki123.mssql.somee.com;persist security info=False;initial catalog=rifki123;TrustServerCertificate=True";

builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(connectionString);
});

builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard("/jobs");
app.Run();
