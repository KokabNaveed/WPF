using System.Data.Entity;
using SubsrciptionSystem.Models;

public class AppDbContext : DbContext
{
    public AppDbContext() : base("SubscriptionSystemDB") { }

    public DbSet<DomainEntity> Domains { get; set; }
    public DbSet<SoftwareEntity> Softwares { get; set; }

    public DbSet<EmailEntity> EmailUser { get; set; }
}
