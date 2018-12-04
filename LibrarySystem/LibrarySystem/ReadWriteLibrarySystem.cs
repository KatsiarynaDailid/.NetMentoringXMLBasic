using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Parsers;
using LibrarySystem.Parsers.Abstract;
using LibrarySystem.Writers;
using LibrarySystem.Writers.Abstract;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem
{
    public class ReadWriteLibrarySystem
    {
        private WriterCreator writerCreator;
        private ParserCreator parserCreator;

        public ReadWriteLibrarySystem()
        {
            writerCreator = new WriterCreator();
            parserCreator = new ParserCreator();
        }

        public void WriteTo(IEnumerable<IEntity> source, TextWriter output, bool useIndentFormatSetting = false)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();

            if (useIndentFormatSetting)
            {
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.IndentChars = "\t";
                xmlWriterSettings.NewLineOnAttributes = true;
            }

            using (XmlWriter writer = XmlWriter.Create(output, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("library");

                EntityWriter entityWriter;
                foreach (var entity in source)
                {
                    // :)
                    entityWriter = writerCreator.CreateEntityWriter(entity);
                    entityWriter.Write(entity, writer);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public IEnumerable<IEntity> ReadFrom(TextReader input)
        {
            var listOfEntities = new List<IEntity>();
            using (XmlReader reader = XmlReader.Create(input))
            {
                reader.ReadStartElement("library");
                EntityParser entityParser;
                do
                {
                    while (reader.NodeType == XmlNodeType.Element)
                    {
                        XElement currentElement = XNode.ReadFrom(reader) as XElement;
                        var name = currentElement.Name.LocalName;
                        entityParser = parserCreator.CreateEntityParser(name);
                        listOfEntities.Add(entityParser.ParseEntity(currentElement));
                    }
                } while (reader.Read());
            }
            return listOfEntities;
        }
    }
}
