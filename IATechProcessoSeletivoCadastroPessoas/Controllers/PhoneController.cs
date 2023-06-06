using Microsoft.AspNetCore.Http;
using IATechProcessoSeletivoCadastroPessoas.Models;
using IATechProcessoSeletivoCadastroPessoas.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IATechProcessoSeletivoCadastroPessoas.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PhoneController : ControllerBase
{
    private readonly IPhoneRepository _phoneRepository;
    private readonly IPersonRepository _personRepository;

    public PhoneController(IPhoneRepository phoneRepository, IPersonRepository personRepository)
    {
        _phoneRepository = phoneRepository;
        _personRepository = personRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<PhoneModel>>> GetAll()
    {
        List<PhoneModel> phones = await _phoneRepository.GetAll();
        return Ok(phones);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhoneModel>> GetPhoneById(Guid id)
    {
        PhoneModel phone = await _phoneRepository.GetById(id);
        return Ok(phone);
    }

    [HttpPost("{personId}/phones")]
    public async Task<ActionResult<PhoneModel>> CreatePhone([FromBody] long number, Guid personId)
    {
        PersonModel person = await _personRepository.GetById(personId);
        PhoneModel phone = new PhoneModel();

        if(person == null)
        {
            throw new Exception($"Person no founded!");
        }

        phone.Number = number;
        phone.PersonId = personId;

        PhoneModel newPhone = await _phoneRepository.CreatePhone(phone);
        return Ok(newPhone);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PhoneModel>> UpdatePhone(PhoneModel phone, Guid id)
    {
        PhoneModel updatedPhone = await _phoneRepository.UpdatePhone(phone, id);
        return Ok(updatedPhone);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<PhoneModel>> DeletePhone(Guid id)
    {
        bool hasDeleted = await _phoneRepository.DeletePhone(id);
        return Ok(hasDeleted);
    }
}
