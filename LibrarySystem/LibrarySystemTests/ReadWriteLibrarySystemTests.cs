using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibrarySystem.Parsers;
using LibrarySystem;
using LibrarySystem.Entities;
using LibrarySystem.Entities.Interfaces;
using LibrarySystem.Entities.SubEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibrarySystemTests
{
    /// <summary>
    /// TODO:
    /// - Move all static test data to a separate file
    /// </summary>
    [TestClass]
    public class ReadWriteLibrarySystemTests
    {
        private static ReadWriteLibrarySystem librarySystem;

        [ClassInitialize]
        public static void ReadWriteLibrarySystemTestsInitialize(TestContext testContext)
        {
            librarySystem = new ReadWriteLibrarySystem();
        }

        [TestMethod]
        public void CheckReadFromXml()
        {
            var expectedBook = GetBookEntity();
            var expectedNewspaper = GetNewspaperEntity();
            var expectedPatent = GetPatentEntity();
            StringReader stringReader = new StringReader(GetXmlToRead());

            var actualResult = librarySystem.ReadFrom(stringReader);
            stringReader.Dispose();
            var actualBook = (Book)actualResult.ToList().Where(x => x.GetType() == typeof(Book)).FirstOrDefault();
            var actualNewspaper = (Newspaper)actualResult.ToList().Where(x => x.GetType() == typeof(Newspaper)).FirstOrDefault();
            var actualPatent = (Patent)actualResult.ToList().Where(x => x.GetType() == typeof(Patent)).FirstOrDefault();

            Assert.IsTrue(expectedBook.Name == actualBook.Name &&
                expectedBook.Authors.FirstOrDefault().FirstName == actualBook.Authors.FirstOrDefault().FirstName &&
                expectedBook.Authors.FirstOrDefault().LastName == actualBook.Authors.FirstOrDefault().LastName &&
                expectedBook.PlaceOfPublishing == actualBook.PlaceOfPublishing &&
                expectedBook.PublishingHouse == expectedBook.PublishingHouse &&
                expectedBook.YearOfPublishing == actualBook.YearOfPublishing &&
                expectedBook.Notes == actualBook.Notes &&
                expectedBook.CountOfPages == actualBook.CountOfPages &&
                expectedBook.ISBN == actualBook.ISBN,
                "Actual parsed book is different from expected.");

            Assert.IsTrue(expectedNewspaper.Name == actualNewspaper.Name &&
                expectedNewspaper.PlaceOfPublishing == actualNewspaper.PlaceOfPublishing &&
                expectedNewspaper.PublishingHouse == expectedNewspaper.PublishingHouse &&
                expectedNewspaper.YearOfPublishing == actualNewspaper.YearOfPublishing &&
                expectedNewspaper.CountOfPages == actualNewspaper.CountOfPages &&
                expectedNewspaper.Number == actualNewspaper.Number &&
                expectedNewspaper.Date == actualNewspaper.Date &&
                expectedNewspaper.Notes == actualNewspaper.Notes &&
                expectedNewspaper.ISBN == actualNewspaper.ISBN,
                "Actual parsed newspaper is different from expected.");

            Assert.IsTrue(expectedPatent.Name == actualPatent.Name &&
                expectedPatent.Authors.FirstOrDefault().FirstName == actualPatent.Authors.FirstOrDefault().FirstName &&
                expectedPatent.Authors.FirstOrDefault().LastName == actualPatent.Authors.FirstOrDefault().LastName &&
                expectedPatent.Country == actualPatent.Country &&
                expectedPatent.RegistrationNumber == actualPatent.RegistrationNumber &&
                expectedPatent.ApplicationDate == actualPatent.ApplicationDate &&
                expectedPatent.Notes == actualPatent.Notes &&
                expectedPatent.CountOfPages == actualPatent.CountOfPages &&
                expectedPatent.DateOfPublication == actualPatent.DateOfPublication,
                "Actual parsed patent is different from expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlValidationException))]
        public void CheckReadFromXml_XmlValidationException()
        {
            var expectedBook = "<?xml version=\"1.0\" encoding=\"utf-16\"?><library>" +
                GetBookWithoutNameXml() +
                "</library>";
            StringReader stringReader = new StringReader(expectedBook);
            librarySystem.ReadFrom(stringReader);
            stringReader.Dispose();
        }

        [TestMethod]
        public void CheckWriteBookEntity()
        {
            var expectedBook = "<?xml version=\"1.0\" encoding=\"utf-16\"?><library>" +
                   GetBookXml() +
                   "</library>";
            StringBuilder actualBook = new StringBuilder();
            StringWriter stringWriter = new StringWriter(actualBook);
            librarySystem.WriteTo(new List<IEntity> { GetBookEntity() }, stringWriter);

            Assert.AreEqual(expectedBook.Trim(), actualBook.ToString().Trim());
            stringWriter.Dispose();
        }

        [TestMethod]
        public void CheckWriteNewspaperEntity()
        {
            var expectedNewspaper = "<?xml version=\"1.0\" encoding=\"utf-16\"?><library>" +
                   GetNewspaperXml() +
                   "</library>";
            StringBuilder actualNewspaper = new StringBuilder();
            StringWriter stringWriter = new StringWriter(actualNewspaper);
            librarySystem.WriteTo(new List<IEntity> { GetNewspaperEntity() }, new StringWriter(actualNewspaper));

            Assert.AreEqual(expectedNewspaper.Trim(), actualNewspaper.ToString().Trim());
            stringWriter.Dispose();
        }

        [TestMethod]
        public void CheckWritePatentEntity()
        {
            var expectedPatent = "<?xml version=\"1.0\" encoding=\"utf-16\"?><library>" +
                   GetPatentXml() +
                   "</library>";
            StringBuilder actualPatent = new StringBuilder();
            StringWriter stringWriter = new StringWriter(actualPatent);
            librarySystem.WriteTo(new List<IEntity> { GetPatentEntity() }, new StringWriter(actualPatent));

            Assert.AreEqual(expectedPatent.Trim(), actualPatent.ToString().Trim());
            stringWriter.Dispose();
        }

        #region Private

        private Newspaper GetNewspaperEntity()
        {
            return new Newspaper()
            {
                Name = "The New York Times",
                PlaceOfPublishing = "New York City",
                PublishingHouse = "A.G. Sulzberger",
                YearOfPublishing = 1851,
                CountOfPages = 20,
                Notes = "Some notes",
                Number = 3333,
                Date = new DateTime(2018, 11, 28),
                ISBN = "0362-4331",
            };
        }

        private Book GetBookEntity()
        {
            return new Book()
            {
                Name = "It",
                Authors = new List<Author>
                {
                    new Author()
                    {
                        FirstName = "Stephen",
                        LastName = "King"
                    }
                },
                PlaceOfPublishing = "USA",
                PublishingHouse = "Scribner",
                YearOfPublishing = 2017,
                CountOfPages = 1184,
                Notes = "",
                ISBN = "1501175467",
            };
        }

        private Patent GetPatentEntity()
        {
            return new Patent()
            {
                Name = "Predicted Weather Display and Decision Support Interface for Flight Deck",
                Authors = new List<Author>
                {
                    new Author()
                    {
                        FirstName = "",
                        LastName = "Johnson"
                    },
                    new Author()
                    {
                        FirstName = "Dominic",
                        LastName = "Wong"
                    },
                    new Author()
                    {
                        FirstName = "Shu-Chieh",
                        LastName = "Wu"
                    },
                    new Author()
                    {
                        FirstName = "Robert",
                        LastName = "Koteskey"
                    }
                },
                Country = "USA",
                RegistrationNumber = "US-Patent-9,652,888",
                ApplicationDate = new DateTime(2017, 5, 16),
                DateOfPublication = new DateTime(2017, 6, 7),
                CountOfPages = 1184,
                Notes = "A system and method for providing visual depictions of a predictive weather" +
                " forecast for in-route vehicle trajectory planning. " +
                "The method includes displaying weather information on a graphical display," +
                " displaying vehicle position information on the graphical display.",
            };
        }

        private string GetBookXml()
        {
            return "<Book>" +
                "<Name>It</Name>" +
                "<Author>" +
                "<FirstName>Stephen</FirstName>" +
                "<LastName>King</LastName>" +
                "</Author>" +
                "<PlaceOfPublishing>USA</PlaceOfPublishing>" +
                "<PublishingHouse>Scribner</PublishingHouse>" +
                "<YearOfPublishing>2017</YearOfPublishing>" +
                "<CountOfPages>1184</CountOfPages>" +
                "<Notes />" +
                "<ISBN>1501175467</ISBN>" +
                "</Book>";
        }

        private string GetBookWithoutNameXml()
        {
            return "<Book>" +
                "<Author>" +
                "<FirstName>Stephen</FirstName>" +
                "<LastName>King</LastName>" +
                "</Author>" +
                "<PlaceOfPublishing>USA</PlaceOfPublishing>" +
                "<PublishingHouse>Scribner</PublishingHouse>" +
                "<YearOfPublishing>2017</YearOfPublishing>" +
                "<CountOfPages>1184</CountOfPages>" +
                "<Notes />" +
                "<ISBN>1501175467</ISBN>" +
                "</Book>";
        }

        private string GetNewspaperXml()
        {
            return "<Newspaper>" +
                "<Name>The New York Times</Name>" +
                "<PlaceOfPublishing>New York City</PlaceOfPublishing>" +
                "<PublishingHouse>A.G. Sulzberger</PublishingHouse>" +
                "<YearOfPublishing>1851</YearOfPublishing>" +
                "<CountOfPages>20</CountOfPages>" +
                "<Notes>Some notes</Notes>" +
                "<Number>3333</Number>" +
                "<Date>11-28-2018</Date>" +
                "<ISBN>0362-4331</ISBN>" +
                "</Newspaper>";
        }

        private string GetPatentXml()
        {
            return "<Patent>" +
                "<Name>Predicted Weather Display and Decision Support Interface for Flight Deck</Name>" +
                "<Author>" +
                "<FirstName />" +
                "<LastName>Johnson</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Dominic</FirstName>" +
                "<LastName>Wong</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Shu-Chieh</FirstName>" +
                "<LastName>Wu</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Robert</FirstName>" +
                "<LastName>Koteskey</LastName>" +
                "</Author>" +
                "<Country>USA</Country>" +
                "<RegistrationNumber>US-Patent-9,652,888</RegistrationNumber>" +
                "<ApplicationDate>05-16-2017</ApplicationDate>" +
                "<DateOfPublication>06-07-2017</DateOfPublication>" +
                "<Notes>A system and method for providing visual depictions of a predictive weather" +
                " forecast for in-route vehicle trajectory planning. " +
                "The method includes displaying weather information on a graphical display," +
                " displaying vehicle position information on the graphical display.</Notes>" +
                "<CountOfPages>1184</CountOfPages>" +
                "</Patent>";
        }

        private string GetXmlToRead()
        {
            return "<library>" +
                "<Book>" +
                "<Name>It</Name>" +
                "<Author>" +
                "<FirstName>Stephen</FirstName>" +
                "<LastName>King</LastName>" +
                "</Author>" +
                "<PlaceOfPublishing>USA</PlaceOfPublishing>" +
                "<PublishingHouse>Scribner</PublishingHouse>" +
                "<YearOfPublishing>2017</YearOfPublishing>" +
                "<CountOfPages>1184</CountOfPages>" +
                "<Notes></Notes>" +
                "<ISBN>1501175467</ISBN>" +
                "</Book>" +
                "<Newspaper>" +
                "<Name>The New York Times</Name>" +
                "<PlaceOfPublishing>New York City</PlaceOfPublishing>" +
                "<PublishingHouse>A.G. Sulzberger</PublishingHouse>" +
                "<YearOfPublishing>1851</YearOfPublishing>" +
                "<CountOfPages>20</CountOfPages>" +
                "<Notes>Some notes</Notes>" +
                "<Number>3333</Number>" +
                "<Date>11-28-2018</Date>" +
                "<ISBN>0362-4331</ISBN>" +
                "</Newspaper>" +
                "<Patent>" +
                "<Name>Predicted Weather Display and Decision Support Interface for Flight Deck</Name>" +
                "<Author>" +
                "<FirstName></FirstName>" +
                "<LastName>Johnson</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Dominic</FirstName>" +
                "<LastName>Wong</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Shu-Chieh</FirstName>" +
                "<LastName>Wu</LastName>" +
                "</Author>" +
                "<Author>" +
                "<FirstName>Robert</FirstName>" +
                "<LastName>Koteskey</LastName>" +
                "</Author>" +
                "<Country>USA</Country>" +
                "<RegistrationNumber>US-Patent-9,652,888</RegistrationNumber>" +
                "<ApplicationDate>05-16-2017</ApplicationDate>" +
                "<DateOfPublication>06-07-2017</DateOfPublication>" +
                "<Notes>A system and method for providing visual depictions of a predictive weather" +
                " forecast for in-route vehicle trajectory planning. " +
                "The method includes displaying weather information on a graphical display," +
                " displaying vehicle position information on the graphical display.</Notes>" +
                "<CountOfPages>1184</CountOfPages>" +
                "</Patent>" +
                "</library>";
        }

        #endregion


    }
}
