using IATechProcessoSeletivoCadastroPessoas.Data;
using IATechProcessoSeletivoCadastroPessoas.Models;
using IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace IATechProcessoSeletivoCadastroPessoas.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleRegistrationDBContext _dbContext;

        public PersonRepository(PeopleRegistrationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PersonModel>> GetAll()
        {
            return await _dbContext.People.Include(person => person.Phones).ToListAsync();
        }
        public async Task<PersonModel> GetById(Guid id)
        {
            PersonModel person = await _dbContext.People.FirstOrDefaultAsync(person => person.Id == id);
            if(person == null)
            {
                throw new Exception($"Person not found!");
            }
            return person;
        }
        public async Task<PersonModel> CreatePerson(PersonModel person)
        {
            await _dbContext.People.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<PersonModel> UpdatePerson(PersonModel person, Guid id)
        {
            PersonModel db_person = await GetById(id);
            if(db_person == null)
            {
                throw new Exception($"The person with id {id} was not found!");
            }

            db_person.Birth = person.Birth;
            db_person.Name = person.Name;
            db_person.CPF = person.CPF;

            _dbContext.Update(db_person);
            await _dbContext.SaveChangesAsync();

            return db_person;
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            PersonModel person = await GetById(id);

            if(person == null)
            {
                throw new Exception($"The Person with id {id} was not found!");
            }

            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
