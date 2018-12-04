using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Writers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LibrarySystem.Writers
{
    public class BookWriter : EntityWriter
    {
        public override void Write(IEntity source, XmlWriter writer)
        {
            Book sourceBook = source as Book;

            writer.WriteStartElement("Book");
            writer.WriteElementString("Name", sourceBook.Name);

            foreach (var author in sourceBook.Authors)
            {
                writer.WriteStartElement("Author");
                writer.WriteElementString("FirstName", author.FirstName ?? "None");
                writer.WriteElementString("LastName", author.LastName);
                writer.WriteEndElement();
            }

            writer.WriteElementString("PlaceOfPublishing", sourceBook.PlaceOfPublishing ?? "None");
            writer.WriteElementString("PublishingHouse", sourceBook.PublishingHouse);
            writer.WriteElementString("YearOfPublishing", sourceBook.YearOfPublishing.ToString());
            writer.WriteElementString("CountOfPages", sourceBook.CountOfPages.ToString());
            writer.WriteElementString("Notes", sourceBook.Notes ?? "None");
            writer.WriteElementString("ISBN", sourceBook.ISBN);

            writer.WriteEndElement();
        }
    }
}
