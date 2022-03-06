using System.Xml;
using System.Xml.Serialization;

namespace MustDo.Interfaces {
    public abstract class XmlSerializable<T> {
        public static XmlSerializer XmlSerializer = new(typeof(T));

        public static XmlWriterSettings XmlWriterSettings = new() {
            Indent = true,
            IndentChars = "\t",
            CloseOutput = true,
        };

        public static XmlWriter GetXmlWriter(string fileName) => XmlWriter.Create(fileName, XmlWriterSettings);

        public static XmlReader GetXmlReader(string fileName) => XmlReader.Create(fileName);
    }
}
