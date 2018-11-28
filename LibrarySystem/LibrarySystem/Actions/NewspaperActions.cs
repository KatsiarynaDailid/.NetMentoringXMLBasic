using LibrarySystem.Actions.Interfaces;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using System;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem.Actions
{
    class NewspaperActions : IEntityActions
    {
        public IEntity ParseEntity(XElement element)
        {
            Newspaper newspaper = new Newspaper();
            var nodes = element.Elements();

            newspaper.Name = element.Element("Name").Value;
            newspaper.PlaceOfPublishing = element.Element("PlaceOfPublishing").Value ?? "None";
            newspaper.PublishingHouse = element.Element("PublishingHouse").Value;
            newspaper.YearOfPublishing = int.Parse(element.Element("YearOfPublishing").Value);
            newspaper.CountOfPages = int.Parse(element.Element("CountOfPages").Value);
            newspaper.Notes = element.Element("Notes").Value ?? "None";
            newspaper.Number = int.Parse(element.Element("Number").Value);
            newspaper.Date = DateTime.Parse(element.Element("Date").Value);
            newspaper.ISBN = element.Element("ISBN").Value;

            return newspaper;
        }

        public void Write(IEntity source, XmlWriter writer)
        {
            Newspaper sourceNewspaper = source as Newspaper;

            writer.WriteStartElement("Newspaper");

            writer.WriteElementString("Name", sourceNewspaper.Name);
            writer.WriteElementString("PlaceOfPublishing", sourceNewspaper.PlaceOfPublishing ?? "None");
            writer.WriteElementString("PublishingHouse", sourceNewspaper.PublishingHouse);
            writer.WriteElementString("YearOfPublishing", sourceNewspaper.YearOfPublishing.ToString());
            writer.WriteElementString("CountOfPages", sourceNewspaper.CountOfPages.ToString());
            writer.WriteElementString("Notes", sourceNewspaper.Notes ?? "None");
            writer.WriteElementString("Number", sourceNewspaper.Number.ToString());
            writer.WriteElementString("Date", sourceNewspaper.Date.ToString("MM-dd-yyyy"));
            writer.WriteElementString("ISBN", sourceNewspaper.ISBN);

            writer.WriteEndElement();
        }
    }
}
