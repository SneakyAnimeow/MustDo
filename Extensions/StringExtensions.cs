using MustDo.Utils;
using Newtonsoft.Json;

namespace MustDo.Extensions {
    public static class StringExtensions {
        public static string ToSaltedMd5(this string s, string salt) {
            return EncryptionUtils.ToSaltedMd5(s, salt);
        }

        public static string ToMd5(this string s) {
            return EncryptionUtils.ToMd5(s);
        }

        public static T FromJson<T>(this string s) {
            return JsonConvert.DeserializeObject<T>(s) ?? (T)Activator.CreateInstance<T>();
        }
    }
}
