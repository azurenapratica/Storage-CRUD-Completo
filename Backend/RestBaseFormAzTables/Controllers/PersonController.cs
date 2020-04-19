using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestBaseFormAzTables.ApiModels;
using RestBaseFormAzTables.Core.Entities;
using RestBaseFormAzTables.Core.Interfaces;

namespace RestBaseFormAzTables.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPerson _personRepository;

        public PersonController(IPerson personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet("{rowKey}")]
        public async Task<Person> Get(string rowKey)
        {
            return await _personRepository.GetById(rowKey);
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _personRepository.Get();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PersonDTO person)
        {
            if (person is null) { return NoContent(); }
            var pictureUrl = await _personRepository.UploadBlobFileAsync(person.Picture);
            var personItem = new Person()
            {
                Name = person.Name,
                Email = person.Email,
                Picture = pictureUrl
            };
            await _personRepository.Add(personItem);
            return Ok();
        }

        [HttpPut("{rowKey}")]
        public async Task<IActionResult> Put(string rowKey, [FromForm] PersonDTO person)
        {
            if (person is null || rowKey == string.Empty) { return NoContent(); }
            var personToUpdate = await _personRepository.GetById(rowKey);
            if (personToUpdate is null) { return NotFound(); }
            else
            {
                var pictureUrl = await _personRepository.UploadBlobFileAsync(person.Picture);
                personToUpdate.Name = person.Name;
                personToUpdate.Email = person.Email;
                personToUpdate.Picture = (pictureUrl == null || pictureUrl == string.Empty) ? personToUpdate.Picture : pictureUrl;
                await _personRepository.Edit(personToUpdate);
            }
            return Ok(personToUpdate);
        }

        [HttpDelete("{rowKey}")]
        public async Task<IActionResult> Delete(string rowKey)
        {
            if (rowKey == string.Empty) { return NoContent(); }
            var personToDelete = await _personRepository.GetById(rowKey);
            if (personToDelete is null) { return NotFound(); }
            else
            {
                await _personRepository.Delete(personToDelete);
            }
            return Ok();
        }
    }
}
