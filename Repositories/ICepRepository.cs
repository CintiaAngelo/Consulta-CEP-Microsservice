using CepServiceApp.Models;

namespace CepServiceApp.Repositories
{
    public interface ICepRepository
    {
        Task<Cep> AddCepAsync(Cep cep);
        Task<List<Cep>> GetAllCepsAsync();
        Task<Cep?> GetCepByCodeAsync(string cep);
    }
}