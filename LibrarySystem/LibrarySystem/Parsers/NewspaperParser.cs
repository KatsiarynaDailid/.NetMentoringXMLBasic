using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Parsers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibrarySystem.Parsers
{
    public class NewspaperParser : EntityParser
    {
        public override IEntity ParseEntity(XElement element)
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
    }
}
