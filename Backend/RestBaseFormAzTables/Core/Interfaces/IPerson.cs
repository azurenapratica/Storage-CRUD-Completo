using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestBaseFormAzTables.Core.Entities;

namespace RestBaseFormAzTables.Core.Interfaces
{
    public interface IPerson
    {
        Task<Person> GetById(string rowKey);  
        Task<IEnumerable<Person>> Get();  
        Task Add(Person person);
        Task Edit(Person person);
        Task Delete(Person person);
        Task<string> UploadBlobFileAsync(IFormFile picture);
    }
}