using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using LibrarySystem.Parsers.Abstract;
using LibrarySystem.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LibrarySystem.Parsers
{
    public class BookParser : EntityParser
    {
        /// <summary>
        /// TODO:
        /// - Move validation logic to a different class
        /// </summary>
        public override IEntity ParseEntity(XElement element)
        {
            BookValidation(element);
            Book book = new Book();
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

        private void BookValidation(XElement element)
        {
            BookSchema schema = new BookSchema();
            XmlSchemaSet set = new XmlSchemaSet();

            set.Add(schema.GetBookSchema());
            set.Compile();

            XmlSchemaObject schemaObject = set.GlobalElements.Values.OfType<XmlSchemaObject>().First();
            element.Validate(schemaObject, set, new ValidationEventHandler(ValidationCallback));
        }

        /// <summary>
        /// TODO:
        /// - Use log instead on Console.WriteLine
        /// </summary>
        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.WriteLine("ERROR: ");

            Console.WriteLine(args.Message);

            throw new XmlValidationException(args.Message);
        }
    }
}
