using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using RethinkFirstCodingChallenge.Data;
using RethinkFirstCodingChallenge.Data.Model;
using System.Globalization;

namespace RethinkFirstCodingChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly RethinkFirstContext _rfContext;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(RethinkFirstContext rfContext, ILogger<ClientsController> logger)
        {
            _rfContext = rfContext;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Client> GetActiveClients()
        {
            var data = _rfContext.Client.Where(c => c.Removed == null);

            return data;
        }

        [HttpPost]
        public async Task AddClients(string client)
        {

            await _rfContext.AddAsync(client);
            await _rfContext.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task EditClient(Client client)
        {
            var record = _rfContext.Client.FirstOrDefault(c => c.Id == client.Id);
            record = client;
            await _rfContext.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task DeleteClientById(int clientId)
        {
            var record = _rfContext.Client.FirstOrDefault(c => c.Id == clientId);
            if (record == null) return;
            record.Removed = DateTime.Now;
            await _rfContext.SaveChangesAsync();
        }

        //TODO: Fix async
        [HttpPost]
        public async Task ParseClientsFromCSV(string csv)
        {
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                var collectionToUpload = new List<Client>();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };
                    DateOnly fromDateValue;
                    collectionToUpload.Add(new Client
                    {
                        FirstName = fields[0],
                        LastName = fields[1],
                        Birthdate = DateOnly.TryParseExact(fields[2], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue) ? DateOnly.Parse(fields[2]) : null,
                        Gender = fields[3]
                    });
                }

                _rfContext.AddRangeAsync(collectionToUpload);
                _rfContext.SaveChangesAsync();
            }
        }

    }
}
