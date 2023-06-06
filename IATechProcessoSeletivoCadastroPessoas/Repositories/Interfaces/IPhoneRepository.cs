using IATechProcessoSeletivoCadastroPessoas.Models;

namespace IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces
{
    public interface IPhoneRepository
    {
        Task<List<PhoneModel>> GetAll();
        Task<PhoneModel> GetById(Guid Id);
        Task<PhoneModel> CreatePhone(PhoneModel phone);
        Task<PhoneModel> UpdatePhone(PhoneModel phone, Guid Id);
        Task<bool> DeletePhone(Guid Id);
    }
}
