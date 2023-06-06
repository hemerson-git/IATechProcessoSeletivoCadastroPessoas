using Microsoft.EntityFrameworkCore;
using IATechProcessoSeletivoCadastroPessoas.Data.Map;
using IATechProcessoSeletivoCadastroPessoas.Models;

namespace IATechProcessoSeletivoCadastroPessoas.Data
{
    public class PeopleRegistrationDBContext : DbContext
    {
        public PeopleRegistrationDBContext(DbContextOptions<PeopleRegistrationDBContext> options)
            : base(options)
        {

        }

        public DbSet<PersonModel> People { get; set; }
        public DbSet<PhoneModel> Phone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new PhoneMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}