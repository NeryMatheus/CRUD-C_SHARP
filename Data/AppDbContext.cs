using CRUD_C_SHARP.students.entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C_SHARP.data;

public class AppDbContext: DbContext
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();

    public DbSet<Student> Students { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }
}