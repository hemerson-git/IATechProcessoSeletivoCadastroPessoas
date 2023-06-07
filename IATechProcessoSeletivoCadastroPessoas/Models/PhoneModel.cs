using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IATechProcessoSeletivoCadastroPessoas.Models
{
    public class PhoneModel
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public Guid? PersonId { get; set; }
    }
}