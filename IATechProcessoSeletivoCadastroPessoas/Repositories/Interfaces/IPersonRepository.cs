using IATechProcessoSeletivoCadastroPessoas.Models;

namespace IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<PersonModel>> GetAll();
        Task<List<PersonModel>> GetPeopleByName(string name);
        Task<PersonModel> GetById(Guid id);
        Task<PersonModel> CreatePerson(PersonModel person);
        Task<PersonModel> UpdatePerson(PersonModel person, Guid id);
        Task<bool> DeletePerson(Guid id);
    }
}
