using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services.BusinessLogic;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        ILogicContaCorrente logicContaCorrente;

        public ContaCorrenteController(ILogicContaCorrente logicContaCorrente)
        {
            this.logicContaCorrente = logicContaCorrente;
        }

        [HttpPost]
        public async Task<ActionResult<ResultResponse>> Post([FromBody] MovimentoRequest request)
        {
            ResultResponse result = await logicContaCorrente.RegistrarMovimento(request);

            if (!result.HouveErro)
            {
                return Ok($"IdMovimento gerado: {result.MensagemRetorno}");
            }
            else
            {
                return BadRequest(result.MensagemRetorno);
            }
        }

    }
}
