using Microsoft.EntityFrameworkCore;
using RadiostationWeb.Models;


namespace RadiostationWeb.Data
{
    public partial class RadiostationWebDbContext : DbContext
    {
        public RadiostationWebDbContext()
        {
        }

        public RadiostationWebDbContext(DbContextOptions<RadiostationWebDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Broadcast> Broadcasts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<HomePageImage> HomePageImages { get; set; }
        

    }
}
