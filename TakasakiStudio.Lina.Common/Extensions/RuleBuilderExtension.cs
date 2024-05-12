using System.Text.RegularExpressions;
using FluentValidation;

namespace TakasakiStudio.Lina.Common.Extensions;

public static partial class RuleBuilderExtension
{
    public static IRuleBuilder<T, string> ValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(BeAValidCpf);
    }
    
    public static IRuleBuilder<T, string> ValidCnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(BeAValidCnpj);
    }

    private static bool BeAValidCpf(string input)
    {
        var numbers = ToNumbers(input);
        if (numbers.Length != 11 || numbers.Distinct().Count() == 1)
            return false;

        var v1 = 0;
        var v2 = 0;

        for (var i = 0; i < 9; i++) {
            var number = numbers[8 - i];
            v1 += number * (9 - i % 10);
            v2 += number * (9 - (i + 1) % 10);
        }

        v1 %= 11;
        v1 %= 10;

        v2 += v1 * 9;
        v2 %= 11;
        v2 %= 10;
		
        return numbers[9] == v1 && numbers[10] == v2;
    }

    private static bool BeAValidCnpj(string input)
    {
        var numbers = ToNumbers(input);
        if (numbers.Length != 14)
            return false;
		
        var v1 = 5 * numbers[0] + 4 * numbers[1] + 3 * numbers[2] + 2 * numbers[3] +
                 9 * numbers[4] + 8 * numbers[5] + 7 * numbers[6] + 6 * numbers[7] +
                 5 * numbers[8] + 4 * numbers[9] + 3 * numbers[10] + 2 * numbers[11];
		
        var v2 = 6 * numbers[0] + 5 * numbers[1] + 4 * numbers[2] + 3 * numbers[3] +
                 2 * numbers[4] + 9 * numbers[5] + 8 * numbers[6] + 7 * numbers[7] +
                 6 * numbers[8] + 5 * numbers[9] + 4 * numbers[10] + 3 * numbers[11] +
                 2 * numbers[12];
		
        v1 = 11 - v1 % 11;
        if (v1 >= 10)
            v1 = 0;
	
        v2 = 11 - v2 % 11;
        if (v2 >= 10)
            v2 = 0;
		
        return numbers[12] == v1 && numbers[13] == v2;
    }
	
    private static int[] ToNumbers(string input)
    {
        var clean = NotNumberRegex().Replace(input, string.Empty);
        var numbers = clean.ToCharArray().Select(x => int.Parse(x.ToString()));
		
        return numbers.ToArray();
    }

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex NotNumberRegex();
}