using LibrarySystem.Actions.Interfaces;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem.Actions
{
    class BookActions : IEntityActions
    {
        public IEntity ParseEntity(XElement element)
        {
            Book book = new Book();
            var nodes = element.Elements();

            book.Name = element.Element("Name").Value;
            book.Authors = element.Elements("Author").Select(x => new Author
            {
                FirstName = x.Element("FirstName").Value ?? "None",
                LastName = x.Element("LastName").Value         
            }).ToList();

            book.PlaceOfPublishing = element.Element("PlaceOfPublishing").Value ?? "None";
            book.PublishingHouse = element.Element("PublishingHouse").Value;
            book.YearOfPublishing = int.Parse(element.Element("YearOfPublishing").Value);
            book.CountOfPages = int.Parse(element.Element("CountOfPages").Value);
            book.Notes = element.Element("Notes").Value ?? "None";
            book.ISBN = element.Element("ISBN").Value;

            return book;
        }

        public void Write(IEntity source, XmlWriter writer)
        {
            Book sourceBook = source as Book;

            writer.WriteStartElement("Book");
            writer.WriteElementString("Name", sourceBook.Name);

            foreach(var author in sourceBook.Authors)
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
