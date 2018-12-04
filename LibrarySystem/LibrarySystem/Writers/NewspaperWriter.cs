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
    public class NewspaperWriter : EntityWriter
    {
        public override void Write(IEntity source, XmlWriter writer)
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
