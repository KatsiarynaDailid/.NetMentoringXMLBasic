using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Parsers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Entities.SubEntities;
using System.Xml.Linq;

namespace LibrarySystem.Parsers
{
    public class PatentParser : EntityParser
    {
        public override IEntity ParseEntity(XElement element)
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
    }
}
