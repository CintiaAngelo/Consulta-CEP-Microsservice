using Microsoft.AspNetCore.Mvc;
using CepServiceApp.Models;
using CepServiceApp.Services;

namespace CepServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly ICepService _cepService;

        public CepController(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpPost]
        public async Task<ActionResult<Cep>> ConsultarCep([FromBody] ConsultaCepRequest request)
        {
            try
            {
                var cep = await _cepService.ConsultarCepAsync(request.Cep);
                return Ok(cep);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Cep>>> GetAllCeps()
        {
            try
            {
                var ceps = await _cepService.GetAllCepsAsync();
                return Ok(ceps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Erro interno: {ex.Message}" });
            }
        }
    }

    public class ConsultaCepRequest
    {
        public string Cep { get; set; } = string.Empty;
    }
}