using MustDo.Interfaces;

namespace MustDo.Config {
    public class ApiConfig : XmlSerializable<ApiConfig> {
        public int Port { get; set; }
        public bool IsDevelopment { get; set; }
        public string Salt { get; set; }
    }
}
