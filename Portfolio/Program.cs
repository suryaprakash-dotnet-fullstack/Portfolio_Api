using Microsoft.EntityFrameworkCore;
using Portfolio.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core with SQLite  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware  
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ✅ Add this to respond at "/"
app.MapGet("/", () => "✅ Portfolio API is running!");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Create DB if it doesn't exist

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
