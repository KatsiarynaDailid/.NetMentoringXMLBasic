using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class Patent : IEntity
    {
        public string Name { get; set; }

        public List<Author> Authors { get; set; }

        public string Country { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime ApplicationDate { get; set; }

        public DateTime DateOfPublication { get; set; }

        public string Notes { get; set; }

        public int CountOfPages { get; set; }
    }
}
