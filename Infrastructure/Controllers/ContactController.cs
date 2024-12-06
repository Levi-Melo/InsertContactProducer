using Core.Entities;
using Core.Inputs;
using Infrastructure.Gateways.Queue;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController(ILogger<ContactController> logger, BaseQueue<Contato> queue) : ControllerBase
    {

        private readonly ILogger<ContactController> _logger = logger;
        private readonly BaseQueue<Contato> _queue = queue;

        [HttpPost(Name = "contatos")]
        public IActionResult Get([FromBody] ContatoInputCadastrar input)
        {
            try
            {
                this._queue.Publish(new Contato()
                {
                    ContatoNome = input.ContatoNome,
                    ContatoTelefone = input.ContatoTelefone,
                    ContatoEmail = input.ContatoEmail
                });
                return Ok();

            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
    }
}
