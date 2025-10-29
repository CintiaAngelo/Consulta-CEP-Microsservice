using System.Text.Json;
using CepServiceApp.Models;
using CepServiceApp.Repositories;

namespace CepServiceApp.Services
{
    public class CepService : ICepService
    {
        private readonly ICepRepository _cepRepository;
        private readonly HttpClient _httpClient;

        public CepService(ICepRepository cepRepository)
        {
            _cepRepository = cepRepository;
            
            // Configurar HttpClient manualmente para ignorar problemas SSL
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            
            _httpClient = new HttpClient(handler);
        }

        public async Task<Cep> ConsultarCepAsync(string cep)
        {
            // Validar e formatar CEP
            var cepLimpo = ValidarEFormatarCep(cep);

            // Verificar se já existe no banco
            var cepExistente = await _cepRepository.GetCepByCodeAsync(cepLimpo);
            if (cepExistente != null)
            {
                return cepExistente;
            }

            // Consultar Via CEP
            var viaCepResponse = await ConsultarViaCepAsync(cepLimpo);

            // Converter para entidade CEP
            var novoCep = new Cep
            {
                CepCode = viaCepResponse.Cep.Replace("-", ""),
                Logradouro = viaCepResponse.Logradouro,
                Complemento = viaCepResponse.Complemento,
                Bairro = viaCepResponse.Bairro,
                Localidade = viaCepResponse.Localidade,
                Uf = viaCepResponse.Uf,
                Ibge = viaCepResponse.Ibge,
                Gia = viaCepResponse.Gia,
                Ddd = viaCepResponse.Ddd,
                Siafi = viaCepResponse.Siafi,
                DataConsulta = DateTime.Now
            };

            // Salvar no banco
            return await _cepRepository.AddCepAsync(novoCep);
        }

        public async Task<List<Cep>> GetAllCepsAsync()
        {
            return await _cepRepository.GetAllCepsAsync();
        }

        private async Task<ViaCepResponse> ConsultarViaCepAsync(string cep)
        {
            // Use HTTP (não HTTPS) para evitar problemas SSL
            var url = $"http://viacep.com.br/ws/{cep}/json/";
            
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (viaCepResponse == null || viaCepResponse.Erro)
                {
                    throw new ArgumentException("CEP não encontrado");
                }
                
                return viaCepResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao consultar Via CEP: {ex.Message}");
            }
        }

        private string ValidarEFormatarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP não pode ser vazio");

            // Remover formatação
            var cepLimpo = cep.Replace("-", "").Replace(" ", "");

            // Validar se tem 8 dígitos e é numérico
            if (cepLimpo.Length != 8 || !cepLimpo.All(char.IsDigit))
                throw new ArgumentException("CEP deve conter 8 dígitos numéricos");

            return cepLimpo;
        }
    }
}