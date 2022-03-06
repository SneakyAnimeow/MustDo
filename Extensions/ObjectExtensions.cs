using Newtonsoft.Json;

namespace MustDo.Extensions {
    public static class ObjectExtensions {
        public static T To<T>(this object o) {
            return (T)o;
        }

        public static string ToJson(this object o) {
            return JsonConvert.SerializeObject(o);
        }
    }
}
