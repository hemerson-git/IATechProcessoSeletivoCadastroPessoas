using IATechProcessoSeletivoCadastroPessoas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IATechProcessoSeletivoCadastroPessoas.Data.Map
{
    public class PhoneMap : IEntityTypeConfiguration<PhoneModel> 
    {
        public void Configure(EntityTypeBuilder<PhoneModel> builder)
        {
            builder.HasKey(phone => phone.Id);
            builder.Property(phone => phone.Number).IsRequired();
        }
    }
}
