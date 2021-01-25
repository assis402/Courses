using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Courses.Models
{
    public class CustomValidationCPFAttribute : ValidationAttribute, IClientValidatable
    {
        public CustomValidationCPFAttribute()
        {
        }
        /// Validação server
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = ValidCPF(value.ToString());
            return valido;
        }
        /// Validação client
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "CustomValidationCPF"
            };
        }

        public static string RemoveNonNumeric(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        public static bool ValidCPF(string cpf)
        {
            cpf = RemoveNonNumeric(cpf);
            if (cpf.Length != 11)
            {
                return false;
            }

            bool repeated = true;

            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    repeated = false;
                    break;
                }
            }

            if (repeated || cpf == "12345678909")
            {
                return false;
            }

            int[] numbers = new int[11];

            for (int i = 0; i < cpf.Length; i++)
            {
                numbers[i] = int.Parse(cpf[i].ToString());
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += numbers[i] * (10 - i);
            }

            int remnant = (sum * 10) % 11;

            if (remnant == 10 && numbers[9] != 0)
            {
                return false;
            }

            else if (remnant != 10 && remnant != numbers[9])
            {
                return false;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numbers[i] * (11 - i);
            }

            remnant = (sum * 10) % 11;

            if (remnant == 10 && numbers[10] != 0)
            {
                return false;
            }

            else if (remnant != 10 && remnant != numbers[10])
            {
                return false;
            }


            return true;

        }


    }
}
