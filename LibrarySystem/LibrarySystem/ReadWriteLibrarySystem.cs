using LibrarySystem.Actions;
using LibrarySystem.Actions.Interfaces;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem
{
    public class ReadWriteLibrarySystem
    {
        private BookActions bookActions;
        private NewspaperActions newspaperActions;
        private PatentActions patentActions;

        public ReadWriteLibrarySystem()
        {
            bookActions = new BookActions();
            newspaperActions = new NewspaperActions();
            patentActions = new PatentActions();
        }

        public void WriteTo(IEnumerable<IEntity> source, TextWriter output)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                // Can be used in order to structurize xml but Unit tests became more complicated to write.
                // Disabled just to save time.

                /*Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true*/
            };

            using (XmlWriter writer = XmlWriter.Create(output, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("library");

                IEntityActions currentEntityActions;

                foreach (var entity in source)
                {
                    // I realize that it can be done in one line but i couldn't figure out how :(
                    if (entity.GetType() == typeof(Book))
                    {
                        currentEntityActions = bookActions;
                    }
                    else if (entity.GetType() == typeof(Newspaper))
                    {
                        currentEntityActions = newspaperActions;
                    }
                    else if (entity.GetType() == typeof(Patent))
                    {
                        currentEntityActions = patentActions;
                    }
                    else
                    {
                        throw new UnexpectedTypeException($"{entity.GetType()} unexpected type.");
                    }
                    currentEntityActions.Write(entity, writer);
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

                while (reader.NodeType == XmlNodeType.Element)
                {
                    XElement currentElement = XNode.ReadFrom(reader) as XElement;
                    var name = currentElement.Name.LocalName;
                    IEntityActions currentEntityActions;
                    switch (name)
                    {
                        case "Book":
                            {
                                currentEntityActions = bookActions;
                                break;
                            }
                        case "Newspaper":
                            {
                                currentEntityActions = newspaperActions;
                                break;
                            }
                        case "Patent":
                            {
                                currentEntityActions = patentActions;
                                break;
                            }
                        default:
                            {
                                throw new UnexpectedTypeException($"{name} unexpected type name.");
                            }
                    }
                    listOfEntities.Add(currentEntityActions.ParseEntity(currentElement));
                }
            }
            return listOfEntities;
        }

    }
}
