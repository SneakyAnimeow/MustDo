using MustDo.Interfaces;

namespace MustDo.Config {
    public class AppConfig : XmlSerializable<AppConfig> {
        public ApiConfig ApiConfig { get; set; }
        public MssqlConfig MssqlConfig { get; set; }
    }
}
