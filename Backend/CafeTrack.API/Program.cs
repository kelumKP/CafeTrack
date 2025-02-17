using CafeTrack.Application;

var builder = WebApplication.CreateBuilder(args);

// Register Application layer dependencies
builder.Services.AddApplication(builder.Configuration);

// Register DB connectionstring dependencies
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Optional: Seed data (this could also be part of Infrastructure)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<CafeTrack.Infrastructure.Data.AppDbContext>();
    dbContext.Database.EnsureCreated();
    // Assuming SeedData.Initialize is in the Infrastructure layer
    CafeTrack.Infrastructure.Data.SeedData.Initialize(services);
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
