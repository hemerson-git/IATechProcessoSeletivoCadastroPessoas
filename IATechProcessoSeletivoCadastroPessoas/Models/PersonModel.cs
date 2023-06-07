using System.Collections.Generic;
using System.Numerics;

namespace IATechProcessoSeletivoCadastroPessoas.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set;}
        public DateTime Birth { get; set; }
        public ICollection<PhoneModel> Phones { get; set; } = new List<PhoneModel>();
    }
}
