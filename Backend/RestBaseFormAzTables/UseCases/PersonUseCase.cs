using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using RestBaseFormAzTables.Core.Entities;
using RestBaseFormAzTables.Core.Interfaces;

namespace RestBaseFormAzTables.UseCases
{
    public class PersonUseCase : IPerson
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;
        private CloudTableClient tableClient;
        private CloudTable table;
        private readonly string key = "person";

        public PersonUseCase()
        {
            storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("DbConn"));
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(key);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(key);
        }

        public async Task<Person> GetById(string rowKey)
        {
            var person = TableOperation.Retrieve<Person>(key, rowKey);
            var result = await table.ExecuteAsync(person);
            return (Person)result.Result;
        }

        public async Task<IEnumerable<Person>> Get()
        {
            var tableQuery = new TableQuery<Person>();
            TableContinuationToken continuationToken = null;
            TableQuerySegment<Person> tableQueryResult;
            do
            {
                tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                continuationToken = tableQueryResult.ContinuationToken;
            } while (continuationToken != null);
            return tableQueryResult.Results;
        }

        public async Task Add(Person person)
        {
            person.PartitionKey = key;
            person.RowKey = Guid.NewGuid().ToString();
            var tableOperation = TableOperation.Insert(person);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task Edit(Person person)
        {
            var tableOperation = TableOperation.InsertOrReplace(person);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task Delete(Person person)
        {
            var data = new DynamicTableEntity(person.PartitionKey, person.RowKey);
            data.ETag = "*";
            var tableOperation = TableOperation.Delete(data);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task<string> UploadBlobFileAsync(IFormFile picture)
        {
            var url = string.Empty;
            if (picture != null && picture.Length > 0)
            {
                var blockBlob = container.GetBlockBlobReference(picture.FileName);
                using (var fileStream = picture.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }
                url = blockBlob.Uri.ToString();
            }
            return url;
        }
    }
}