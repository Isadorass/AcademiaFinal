using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
    class StandardValidation
    {
        internal class StandardValidations
        {
            public static string ValidationsDescricao(string descricao)
            {
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    return "A descrição deve ser informada.\r\n";
                }

                string tamanhoEhValido = StandardValidations.ValidationsComprimento(descricao, 3, 70, "Descrição");
                if (tamanhoEhValido != "")
                {
                    return tamanhoEhValido;
                }

                return StandardValidations.ValidationsCaracteres(descricao, "Descrição");
            }

            public static string ValidationsNome(string nome)
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    return "O nome deve ser informado.\r\n";
                }
                string tamanhoEhValido = StandardValidations.ValidationsComprimento(nome, 3, 70, "Nome");
                if (tamanhoEhValido != "")
                {
                    return tamanhoEhValido;
                }

                return StandardValidations.ValidationsCaracteres(nome, "Nome");
            }

            public static string ValidationsCpf(string cpf)
            {
                if (string.IsNullOrWhiteSpace(cpf))
                {
                    return "CPF deve ser informado.\r\n";
                }

                if (cpf.Length != 11)
                {
                    return "CPF deve conter 11 caracteres.\r\n";
                }

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;

                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                bool valido = cpf.EndsWith(digito);
                if (!valido)
                {
                    return "CPF inválido.\r\n";
                }

                return "";
            }

            public bool ValidationsRg(string rg)
            {
                rg = rg.Replace("-", "").Replace(".", "").Replace(",", "");

                if (rg.Length == 9)
                {
                    int[] n = new int[8];

                    n[0] = Convert.ToInt32(rg.Substring(0, 1));
                    n[1] = Convert.ToInt32(rg.Substring(1, 1));
                    n[2] = Convert.ToInt32(rg.Substring(2, 1));
                    n[3] = Convert.ToInt32(rg.Substring(3, 1));
                    n[4] = Convert.ToInt32(rg.Substring(4, 1));
                    n[5] = Convert.ToInt32(rg.Substring(5, 1));
                    n[6] = Convert.ToInt32(rg.Substring(6, 1));
                    n[7] = Convert.ToInt32(rg.Substring(7, 1));
                    n[8] = Convert.ToInt32(rg.Substring(8, 1));

                    n[0] *= 2;
                    n[1] *= 3;
                    n[2] *= 4;
                    n[3] *= 5;
                    n[4] *= 6;
                    n[5] *= 7;
                    n[6] *= 8;
                    n[7] *= 9;
                    n[8] *= 100;

                    int somaFinal = n[0] + n[1] + n[2] + n[3] + n[4] + n[5] + n[6] + n[7] + n[8];
                    if ((somaFinal % 11) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            public static string ValidationsEmail(string email, bool isRequired)
            {
                if (isRequired && string.IsNullOrWhiteSpace(email))
                {
                    return "Email deve ser informado.\r\n";
                }

                string erroEmail = ValidationsComprimento(email, 8, 100, "Email");
                if (erroEmail != "")
                {
                    return erroEmail;
                }

                string regex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                bool isValid = Regex.IsMatch(email, regex);
                if (!isValid)
                {
                    return "Email inválido.\r\n";
                }

                return "";
            }

            public static string ValidationsTelefone(string telefone)
            {
                if (string.IsNullOrWhiteSpace(telefone))
                {
                    return "Telefone deve ser informado.\r\n";
                }
                if (telefone.Length < 8 || telefone.Length > 13)
                {
                    return "Telefone deve conter entre 10 e 13 caracteres.\r\n";
                }

                //Arrumar Regex
                if (!Regex.IsMatch(telefone, @"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$"))
                {
                    return "Telefone inválido.\r\n";
                }

                return "";
            }

            public static string ValidationsComprimento(string value, int min, int max, string fieldName)
            {
                if (value.Length < min || value.Length > max)
                {
                    return string.Format("O campo {0} deve conter entre {1} e {2} caracteres.\r\n", fieldName, min, max);
                }

                return "";
            }

            public static string ValidationsCaracteres(string value, string fieldName)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    char letra = value[i];
                    if (!char.IsLetter(letra) && letra != ' ' && letra != '\'')
                    {
                        return string.Format("O campo {0} não pode conter caracteres inválidos", fieldName);
                    }
                }

                return "";
            }

            //Ver com o professor sobre 
            public static bool ValidationsDataNascimento(String data)
            {
                DateTime result;

                if (DateTime.TryParse(data, out result))
                {
                    return true;
                }
                return false;
            }

            public static string ValidationsBairro(string bairro)
            {
                if (string.IsNullOrWhiteSpace(bairro))
                {
                    return "Bairro deve ser informado.\r\n";
                }

                return ValidationsComprimento(bairro, 3, 100, "Bairro");
            }

            public static string ValidationsRua(string rua)
            {
                if (string.IsNullOrWhiteSpace(rua))
                {
                    return "Rua deve ser informada.\r\n";
                }

                return ValidationsComprimento(rua, 3, 100, "Rua");
            }

            public static string ValidationsComplementoNumero(string complemento, string numeroCasa)
            {
                if (string.IsNullOrWhiteSpace(numeroCasa) && string.IsNullOrWhiteSpace(complemento))
                {
                    return "Complemento obrigatório caso não tenha numero da casa.\r\n";
                }
                if (complemento?.Length > 30)
                {
                    return "Complemento não pode conter mais de 30 caracteres.\r\n";
                }
                if (numeroCasa?.Length > 10)
                {
                    return "Número da casa não pode conter mais de 6 caracteres.\r\n";
                }

                return "";
            }

            public static string ValidationsGenero(string genero)
            {
                if (string.IsNullOrWhiteSpace(genero))
                {
                    return "Gênero deve ser informado.\r\n";
                }

                string[] generos = { "Feminino", "Masculino", "Indeterminado" };
                if (!generos.Contains(genero))
                {
                    return "Gênero inválido.\r\n";
                }

                return "";
            }

            public static string ValidationsModalidades(string modalidade)
            {
                if (string.IsNullOrWhiteSpace(modalidade))
                {
                    return "A modalidade deve ser informada.\r\n";
                }
                string tamanhoEhValido = StandardValidations.ValidationsComprimento(modalidade, 3, 70, "Nome");
                if (tamanhoEhValido != "")
                {
                    return tamanhoEhValido;
                }

                return StandardValidations.ValidationsModalidades(modalidade);
            }
        }




    }
}
