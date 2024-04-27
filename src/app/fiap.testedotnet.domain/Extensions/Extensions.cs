using System.Security.Cryptography;
using System.Text;

namespace fiap.testedotnet.domain.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string s)
        {
            if(string.IsNullOrEmpty(s)) return true;
            if(string.IsNullOrWhiteSpace(s)) return true;
            return false;
        }
        public static bool IsPasswordValid(this string password)
        {
            // Verificar se a senha tem pelo menos 8 caracteres
            if (password.Length < 8)
                return false;

            // Verificar se a senha contém pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
        public static string ToSHA256Hash(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
