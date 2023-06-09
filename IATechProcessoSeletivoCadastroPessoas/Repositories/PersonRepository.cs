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
            PersonModel person = await _dbContext.People.Include(person => person.Phones).FirstOrDefaultAsync(person => person.Id == id);

            if(person == null)
            {
                throw new Exception($"Person not found!");
            }
            return person;
        }
        public async Task<PersonModel> CreatePerson(PersonModel person)
        {
            Guid personId = Guid.NewGuid();
            person.Id = personId;

            foreach (var phone in person.Phones)
            {
                phone.PersonId = personId;
            }

            await _dbContext.People.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<PersonModel> UpdatePerson(PersonModel person, Guid id)
        {
            var db_person = await GetById(id);
            if(db_person == null)
            {
                throw new Exception($"The person with id {id} was not found!");
            }

            foreach (var phone in person.Phones)
            {
                phone.PersonId = db_person.Id;
            }

            var existingPhoneIds = db_person.Phones.Select(p => p.Id).ToList();
            var updatedPhoneIds = person.Phones.Select(p => p.Id).ToList();

            var phonesToRemove = db_person.Phones
                .Where(p => !updatedPhoneIds.Contains(p.Id))
                .ToList();

            foreach (var phoneToRemove in phonesToRemove)
            {
                db_person.Phones.Remove(phoneToRemove);
                _dbContext.Phone.Remove(phoneToRemove);
            }

            foreach (var updatedPhone in person.Phones)
            {
                var existingPhone = db_person.Phones.FirstOrDefault(p => p.Id == updatedPhone.Id);
                if (existingPhone != null)
                {
                    existingPhone.Number = updatedPhone.Number;
                }
                else
                {
                    updatedPhone.PersonId = db_person.Id;
                    db_person.Phones.Add(updatedPhone);
                    _dbContext.Phone.Add(updatedPhone);
                }
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

        public async Task<List<PersonModel>> GetPeopleByName(string name)
        {
            List<PersonModel> people = await _dbContext.People.Where(person => person.Name.Contains(name)).Include(person => person.Phones).ToListAsync();
            if(people == null)
            {
                throw new Exception($"There are no person Matching this name");
            }

            return people;
        }
    }
}
