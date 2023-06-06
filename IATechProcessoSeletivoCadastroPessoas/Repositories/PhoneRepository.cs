using IATechProcessoSeletivoCadastroPessoas.Data;
using IATechProcessoSeletivoCadastroPessoas.Models;
using IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IATechProcessoSeletivoCadastroPessoas.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly PeopleRegistrationDBContext _dbContext;

        public PhoneRepository(PeopleRegistrationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PhoneModel>> GetAll()
        {
            return await _dbContext.Phone.ToListAsync();
        }
        public async Task<PhoneModel> GetById(Guid id)
        {
            PhoneModel phone = await _dbContext.Phone.FirstOrDefaultAsync(phone => phone.Id == id);

            if(phone == null)
            {
                throw new Exception($"Phone not founded!");
            }

            return phone;
        }
        public async Task<PhoneModel> CreatePhone(PhoneModel phone)
        {
            await _dbContext.Phone.AddAsync(phone);
            await _dbContext.SaveChangesAsync();
            return phone;
        }
        public async Task<PhoneModel> UpdatePhone(PhoneModel phone, Guid id)
        {
            PhoneModel dbPhone = await GetById(id);

            if(phone == null)
            {
                throw new Exception($"The Phone with Id {id} was not found!");
            }

            dbPhone.Number = phone.Number;
            dbPhone.PersonId = phone.PersonId;

            _dbContext.Phone.Update(dbPhone);
            await _dbContext.SaveChangesAsync();
            return dbPhone;
        }
        public async Task<bool> DeletePhone(Guid id)
        {
            PhoneModel phone = await GetById(id);
            if(phone == null)
            {
                throw new Exception($"The phone with id {id} was not found!");
            }

            _dbContext.Phone.Remove(phone);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
