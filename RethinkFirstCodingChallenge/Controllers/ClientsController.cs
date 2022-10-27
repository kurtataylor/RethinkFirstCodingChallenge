using Microsoft.AspNetCore.Mvc;
using RethinkFirstCodingChallenge.Data;
using RethinkFirstCodingChallenge.Data.Model;

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
        public async Task AddClients(Client client)
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
    }
}
