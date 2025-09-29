using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Source.Models;
using System.ComponentModel.DataAnnotations;
namespace Source.Functions
{
    public class CpfRequest
    {
        public string Cpf { get; set; } = string.Empty;
    }

    public class CpfValidatorFunction
    {
        private readonly ILogger<CpfValidatorFunction> _logger;

        public CpfValidatorFunction(ILogger<CpfValidatorFunction> logger)
        {
            _logger = logger;
        }

        [Function("CpfValidatorFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            // Lê o corpo da requisição
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            CpfRequest data;

            try
            {
                // Desserializa para a classe CpfRequest
                data = JsonSerializer.Deserialize<CpfRequest>(requestBody);

                if (data == null || string.IsNullOrWhiteSpace(data.Cpf))
                {
                    _logger.LogWarning("CPF não fornecido.");
                    return new BadRequestObjectResult("CPF não fornecido.");
                }


                Cpf cpf = new Cpf();
                cpf.Texto = data.Cpf;

                var context = new ValidationContext(cpf, null, null);
                var validationResults = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(cpf, context, validationResults, true);

                if (!isValid)
                {
                    // Retorna os erros encontrados
                    return new BadRequestObjectResult(validationResults.Select(v => v.ErrorMessage));
                }
                return new OkObjectResult($"CPF recebido: {cpf.Texto}");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao desserializar o JSON.");
                return new BadRequestObjectResult("JSON inválido.");
            }
        }
    }
}
