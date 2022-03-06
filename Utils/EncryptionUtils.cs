using System.Security.Cryptography;
using System.Text;

namespace MustDo.Utils {
    public class EncryptionUtils {
        public static string ToMd5(string input) {
            var output = new StringBuilder();

            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            foreach (var b in hashBytes)
                output.Append(b.ToString("X2"));

            return output.ToString();
        }

        public static string ToSaltedMd5(string input, string salt) => ToMd5(input + salt);

        public static string GenerateRandomSecureString(bool specialChars = true, int length = 8) {
            var output = new StringBuilder();
            var characters = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890" + (specialChars ? @"-=[];'\,./_+{}:'""|<>?!@#$%^&*()~`" : "");

            for (var i = 0; i < length; i++)
                output.Append(characters[RandomNumberGenerator.GetInt32(characters.Length)]);

            return output.ToString();
        }
    }
}
