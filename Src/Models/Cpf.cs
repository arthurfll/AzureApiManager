using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Source.Models;

public class Cpf
{
    private string _texto = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 números.")]
    public string Texto
    {
        get => _texto;
        set
        {
            var cpfNumerico = Regex.Replace(value ?? string.Empty, @"\D", "");

            if (cpfNumerico.Length != 11)
                throw new ValidationException("O CPF deve conter exatamente 11 números.");

            if (cpfNumerico.Distinct().Count() == 1)
                throw new ValidationException("CPF inválido.");

            int[] digits = cpfNumerico.Select(c => c - '0').ToArray();

            int sum1 = 0;
            for (int i = 0; i < 9; i++)
                sum1 += digits[i] * (10 - i);
            int dv1 = (sum1 % 11 < 2) ? 0 : 11 - (sum1 % 11);
            if (digits[9] != dv1)
                throw new ValidationException("CPF inválido.");

            int sum2 = 0;
            for (int i = 0; i < 10; i++)
                sum2 += digits[i] * (11 - i);
            int dv2 = (sum2 % 11 < 2) ? 0 : 11 - (sum2 % 11);
            if (digits[10] != dv2)
                throw new ValidationException("CPF inválido.");

            _texto = cpfNumerico;
        }
    }
}
