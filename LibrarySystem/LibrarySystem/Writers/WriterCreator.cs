using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Exceptions;
using LibrarySystem.Writers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Writers
{
    public class WriterCreator
    {
        public EntityWriter CreateEntityWriter(IEntity entity)
        {
            if (entity.GetType() == typeof(Book))
            {
                return new BookWriter();
            }
            else if (entity.GetType() == typeof(Newspaper))
            {
                return new NewspaperWriter();
            }
            else if (entity.GetType() == typeof(Patent))
            {
                return new PatentWriter();
            }
            else
            {
                throw new UnexpectedTypeException($"{entity.GetType()} unexpected type.");
            }
        }
    }
}
