using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;

namespace WebAtividadeEntrevista.Business
{
    public static class CpfValidate
    {
        public static CPFValidateReturn ValidateCpf (string cpf)
        {
            try
            {
                if (cpf.Length != 14)
                    return new CPFValidateReturn() { IsValid = false, Message = "Está faltando ou sobrando valores, verfique se prencheeu com . e -" };

                #region CalculaPrimeiroDigito
                int soma = 0;
                var i = 10;
                foreach ( string parte in cpf.Split('-').FirstOrDefault().Split('.'))
                {
                    foreach (char numero in parte)
                    {
                        soma += (int) Char.GetNumericValue(numero) * i;
                        i--;
                    }                    
                }
                int resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }
                
                string DigitosFinais = resto.ToString();
                #endregion
                #region CalculaUltimoDigito
                soma = 0;
                i = 11;
                foreach (string parte in cpf.Split('-').FirstOrDefault().Split('.'))
                {
                    foreach (char numero in parte)
                    {
                        soma += (int)Char.GetNumericValue(numero) * i;
                        i--;
                    }                    
                }
                soma += resto * i;
                
                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }
                
                DigitosFinais += resto.ToString();
                #endregion

                if (DigitosFinais != cpf.Split('-').LastOrDefault())
                    return new CPFValidateReturn() { IsValid = false, Message = "O CPF informado é inválido! Verique o número" };

                return new CPFValidateReturn() { IsValid = true, Message = ""};
            }
            catch (Exception e)
            {
                return new CPFValidateReturn() { IsValid = false, Message = e.Message };
            }
        }
    }

    public class CPFValidateReturn
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}