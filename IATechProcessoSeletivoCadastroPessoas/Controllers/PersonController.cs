using IATechProcessoSeletivoCadastroPessoas.Models;
using IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace IATechProcessoSeletivoCadastroPessoas.Controllers;
    [ApiController]
    [Route("api/[controller]")]

public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    private readonly IPhoneRepository _phoneRepository;

    public PersonController(IPersonRepository personRepository, IPhoneRepository phoneRepository)
    {
        _personRepository = personRepository;
        _phoneRepository = phoneRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonModel>>> GetAllPeople()
    {
        List<PersonModel> people = await _personRepository.GetAll();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonModel>> GetPersonById(Guid id)
    {
        PersonModel person = await _personRepository.GetById(id);
        return Ok(person);
    }
    [HttpGet("{id}/phones")]
    public async Task<ActionResult<List<PhoneModel>>> GetPersonPhone(Guid id)
    {
        PersonModel person = await _personRepository.GetById(id);

        if(person.Phones == null) {
            return Ok();
        }

        List<PhoneModel> phones = person.Phones.ToList();
        return Ok(phones);
    }
    [HttpPost("{id}/phones")]
    public async Task<ActionResult<PhoneModel>> CreatePersonPhone([FromBody] JsonElement request, Guid id)
    {
        PersonModel person = await _personRepository.GetById(id);
        PhoneModel phone = new();
        long number = request.GetProperty("number").GetInt64();

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        phone.Number = number;
        phone.PersonId = person.Id;

        string json = JsonSerializer.Serialize(phone, options);
        PhoneModel newPhone = await _phoneRepository.CreatePhone(phone);

        return Ok(newPhone);
    }

    [HttpPost]
    public async Task<ActionResult<PersonModel>> CreatePerson(PersonModel person)
    {
        PersonModel newPerson = await _personRepository.CreatePerson(person);
        return Ok(newPerson);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PersonModel>> UpdatePerson([FromBody] PersonModel person, Guid id)
    {
        PersonModel updatedPerson = await _personRepository.UpdatePerson(person, id);
        return Ok(updatedPerson);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonModel>> DeletePerson(Guid id)
    {
        bool hasDeleted = await _personRepository.DeletePerson(id);
        return Ok(hasDeleted);
    }
}

