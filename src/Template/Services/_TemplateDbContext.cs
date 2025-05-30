using Template.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Template.Services.Shared;

namespace Template.Services
{
    public class TemplateDbContext : DbContext
    {
        public TemplateDbContext()
        {
        }

        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
        {
            DataGenerator.InitializeUsers(this);
            DataGenerator.InitializeAbsenceEvents(this);
            DataGenerator.InitializeTimesheets(this);
            DataGenerator.InitializeTeamMembers(this);
            DataGenerator.InitializeTeams(this);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AbsenceEvent> AbsenceEvents { get; set; } // Nome al plurale consigliato
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
