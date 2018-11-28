using LibrarySystem.Actions.Interfaces;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem.Actions
{
    class PatentActions : IEntityActions
    {
        public IEntity ParseEntity(XElement element)
        {
            Patent patent = new Patent();
            var nodes = element.Elements();

            patent.Name = element.Element("Name").Value;
            patent.Authors = element.Elements("Author").Select(x => new Author
            {
                FirstName = x.Element("FirstName").Value ?? "None",
                LastName = x.Element("LastName").Value
            }).ToList();

            patent.Country = element.Element("Country").Value ?? "None";
            patent.RegistrationNumber = element.Element("RegistrationNumber").Value;
            patent.ApplicationDate = DateTime.Parse(element.Element("ApplicationDate").Value);
            patent.DateOfPublication = DateTime.Parse(element.Element("DateOfPublication").Value);
            patent.Notes = element.Element("Notes").Value ?? "None";
            patent.CountOfPages = int.Parse(element.Element("CountOfPages").Value);

            return patent;
        }

        public void Write(IEntity source, XmlWriter writer)
        {
            Patent sourcePatent = source as Patent;

            writer.WriteStartElement("Patent");
            writer.WriteElementString("Name", sourcePatent.Name);

            foreach (var author in sourcePatent.Authors)
            {
                writer.WriteStartElement("Author");
                writer.WriteElementString("FirstName", author.FirstName ?? "None");
                writer.WriteElementString("LastName", author.LastName);
                writer.WriteEndElement();
            }

            writer.WriteElementString("Country", sourcePatent.Country ?? "None");
            writer.WriteElementString("RegistrationNumber", sourcePatent.RegistrationNumber);
            writer.WriteElementString("ApplicationDate", sourcePatent.ApplicationDate.ToString("MM-dd-yyyy"));
            writer.WriteElementString("DateOfPublication", sourcePatent.DateOfPublication.ToString("MM-dd-yyyy"));
            writer.WriteElementString("Notes", sourcePatent.Notes ?? "None");
            writer.WriteElementString("CountOfPages", sourcePatent.CountOfPages.ToString());

            writer.WriteEndElement();
        }
    }
}
