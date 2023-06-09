using IATechProcessoSeletivoCadastroPessoas.Models;
using IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

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
    public async Task<ActionResult<List<PersonModel>>> GetAllPeople([FromQuery] string ?name)
    {
        if(!string.IsNullOrWhiteSpace(name))
        {
            List<PersonModel> matchedPeople = await _personRepository.GetPeopleByName(name);
            return Ok(matchedPeople);
        }

        List<PersonModel> people = await _personRepository.GetAll();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonModel>> GetPersonById(Guid id)
    {
        PersonModel person = await _personRepository.GetById(id);
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonModel>> CreatePerson(PersonModel person)
    {
        Guid personID = Guid.NewGuid();
        person.Id = personID;

        if (person.Phones == null)
        {
            person.Phones = new List<PhoneModel>();
        }

        PersonModel newPerson = await _personRepository.CreatePerson(person);
        return Ok(newPerson);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PersonModel>> UpdatePerson([FromBody] PersonModel person, Guid id)
    {
        try
        {
            PersonModel updatedPerson = await _personRepository.UpdatePerson(person, id);
            return Ok(updatedPerson);
        } catch(Exception ex)
        {
            return NotFound(ex);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonModel>> DeletePerson(Guid id)
    {
        bool hasDeleted = await _personRepository.DeletePerson(id);
        return Ok(hasDeleted);
    }
}

