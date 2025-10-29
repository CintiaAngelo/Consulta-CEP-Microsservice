using CepServiceApp.Models;

namespace CepServiceApp.Services
{
    public interface ICepService
    {
        Task<Cep> ConsultarCepAsync(string cep);
        Task<List<Cep>> GetAllCepsAsync();
    }
}