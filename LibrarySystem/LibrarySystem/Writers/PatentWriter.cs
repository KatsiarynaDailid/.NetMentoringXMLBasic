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
    public class PatentWriter : EntityWriter
    {
        public override void Write(IEntity source, XmlWriter writer)
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
