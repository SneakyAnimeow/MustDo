using MustDo.Utils;
using System.Security.Cryptography;

namespace MustDo.Data {
    public class Data {
        public static Dictionary<string, string> SessionIds = new();

        public static string GenerateSessionId() {
            return EncryptionUtils.ToMd5(DateTime.Now.ToString() + RandomNumberGenerator.GetInt32(0, 420));
        }
    }
}
