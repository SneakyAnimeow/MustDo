using MustDo.Interfaces;

namespace MustDo.Config {
    public class MssqlConfig : XmlSerializable<MssqlConfig> {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UseDefaultPort { get; set; }
    }
}
