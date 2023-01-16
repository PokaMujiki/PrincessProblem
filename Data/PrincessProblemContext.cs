using Microsoft.EntityFrameworkCore;
using PrincessProblem;
using System.Configuration;

namespace PrincessProblemData;

public class PrincessProblemContext : DbContext
{
    public DbSet<Attempt> Attempts { get; set; }
    public DbSet<Contender> Contenders { get; set; }
    
    public PrincessProblemContext(DbContextOptions<PrincessProblemContext> options)
        : base(options)
    {
    }

    public PrincessProblemContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = ConfigurationManager.AppSettings;
        optionsBuilder.UseNpgsql(
            $"Server={config["Server"]};" +
            $"Database={config["Database"]};" +
            $"User Id={config["User"]};" +
            $"Password={config["Password"]}"
        );
    }
    
}