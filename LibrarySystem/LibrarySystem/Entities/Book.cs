using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class Book : IEntity
    {
        public string Name { get; set; }

        public List<Author> Authors { get; set; }

        public string PlaceOfPublishing { get; set; }

        public string PublishingHouse { get; set; }

        public int YearOfPublishing { get; set; }

        public int CountOfPages { get; set; }

        public string Notes { get; set; }

        public string ISBN { get; set; }
    }
}
