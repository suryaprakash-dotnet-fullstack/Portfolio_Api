using Microsoft.EntityFrameworkCore;
using Portfolio.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ PostgreSQL with EF Core  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware  
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ✅ Health check at "/"
app.MapGet("/", () => "✅ Portfolio API is running!");

// ✅ Apply migrations and seed contact
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Contacts.Any())
    {
        db.Contacts.Add(new Portfolio.Models.Contact
        {
            Phone = "9524808329",
            Email = "rsuryaprakash713@gmail.com",
            Location = "Chennai",
            LinkedInUrl = "https://www.linkedin.com/in/suryaprakash-r"
        });

        db.SaveChanges();
    }
}

app.Run();
