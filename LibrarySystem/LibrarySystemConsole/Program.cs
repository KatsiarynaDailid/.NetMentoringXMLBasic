using LibrarySystem;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemConsole
{
    public class Program
    {
        /// <summary>
        /// TODO:
        /// - Move pathes to config file
        /// - XmlValidationException handling (try/catch)
        /// </summary>
        static void Main(string[] args)
        {
            var pathToXmlToReadFrom = @"D:\1.xml";
            var pathToXmlToWriteIn = @"D:\2.xml";
            List<IEntity> entities = new List<IEntity>();

            ReadWriteLibrarySystem librarySystem = new ReadWriteLibrarySystem();
            using (StreamReader sr = new StreamReader(pathToXmlToReadFrom))
            {
                entities = librarySystem.ReadFrom(sr).ToList();
            }

            using (StreamWriter sr = new StreamWriter(pathToXmlToWriteIn))
            {
                librarySystem.WriteTo(entities, sr, true);
            }
        }
    }
}
