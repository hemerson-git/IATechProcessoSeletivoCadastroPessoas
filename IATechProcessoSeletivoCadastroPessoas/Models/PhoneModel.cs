namespace IATechProcessoSeletivoCadastroPessoas.Models
{
    public class PhoneModel
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public Guid? PersonId { get; set; }
        public virtual PersonModel Person { get; set; }

    }
}