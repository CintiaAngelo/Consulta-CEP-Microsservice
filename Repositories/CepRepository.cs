using Microsoft.EntityFrameworkCore;
using CepServiceApp.Models;
using CepServiceApp.Data;

namespace CepServiceApp.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly AppDbContext _context;

        public CepRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cep> AddCepAsync(Cep cep)
        {
            _context.Ceps.Add(cep);
            await _context.SaveChangesAsync();
            return cep;
        }

        public async Task<List<Cep>> GetAllCepsAsync()
        {
            return await _context.Ceps
                .OrderByDescending(c => c.DataConsulta)
                .ToListAsync();
        }

        public async Task<Cep?> GetCepByCodeAsync(string cep)
        {
            return await _context.Ceps
                .FirstOrDefaultAsync(c => c.CepCode == cep); // MUDAR DE CepCode PARA Cep
        }
    }
}