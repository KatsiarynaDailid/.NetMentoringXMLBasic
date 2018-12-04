using LibrarySystem.Exceptions;
using LibrarySystem.Parsers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Parsers
{
    public class ParserCreator
    {
        /// <summary>
        /// TODO:
        /// - Use IoC approach in order to avoid constant creation of a new objects
        /// </summary>
        public EntityParser CreateEntityParser(string entityName)
        {
            switch (entityName)
            {
                case "Book":
                    {
                        return new BookParser();
                    }
                case "Newspaper":
                    {
                        return new NewspaperParser();
                    }
                case "Patent":
                    {
                        return new PatentParser();
                    }
                default:
                    {
                        throw new UnexpectedTypeException($"{entityName} unexpected type name.");
                    }
            }
        }
    }
}
