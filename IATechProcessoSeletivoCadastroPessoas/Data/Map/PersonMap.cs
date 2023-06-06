using Microsoft.EntityFrameworkCore;
using IATechProcessoSeletivoCadastroPessoas.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IATechProcessoSeletivoCadastroPessoas.Data.Map
{
    public class PersonMap : IEntityTypeConfiguration<PersonModel>
    {
        public void Configure(EntityTypeBuilder<PersonModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(person => person.Name).IsRequired().HasMaxLength(255);
            builder.Property(person => person.CPF).IsRequired().HasMaxLength(11);
            builder.Property(person => person.Birth).IsRequired();

            builder.HasMany(person => person.Phones)
            .WithOne(phone => phone.Person)
            .HasForeignKey(phone => phone.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
