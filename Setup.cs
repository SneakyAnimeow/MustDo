using MustDo.Config;
using MustDo.Extensions;

namespace MustDo {
    public class Setup {
        private static string _configFileName = "app_config.xml";

        public static AppConfig AppConfig = new() {
            ApiConfig = new() {
                Port = 14420,
                IsDevelopment = false,
                Salt = Utils.EncryptionUtils.GenerateRandomSecureString(length: 16)
            },
            MssqlConfig = new() {
                Server = "localhost",
                Port = "1433",
                Database = "MustDo",
                UseDefaultPort = true,
                IntegratedSecurity = true,
                User = "sa",
                Password = "password",
            }
        };

        public static void OnStart() {
            if (!File.Exists(_configFileName)) {
                using var writer = AppConfig.GetXmlWriter(_configFileName);
                AppConfig.XmlSerializer.Serialize(writer, AppConfig);
                Console.WriteLine("App started for the first time!\n" +
                    "Change properties in file app_config.xml and start application again.\n" +
                    "Press any key to close the program. . .");
                Console.Read();
                Environment.Exit(0);
            }
            using var reader = AppConfig.GetXmlReader(_configFileName);
            AppConfig = AppConfig.XmlSerializer.Deserialize(reader).To<AppConfig>();
        }
    }
}
