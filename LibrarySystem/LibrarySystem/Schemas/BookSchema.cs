using System.Xml;
using System.Xml.Schema;

namespace LibrarySystem.Schemas
{
    /// <summary>
    ///  TODO:
    ///  - Implement such schema for all of the entities
    ///  - Create separate fabric for validation managing
    /// </summary>
    public class BookSchema
    {
        public XmlSchema GetBookSchema()
        {
            // <FirstName>: Not required
            XmlSchemaElement firstNameElement = new XmlSchemaElement();
            firstNameElement.Name = "FirstName";
            firstNameElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            firstNameElement.MinOccurs = 0;

            // <LastName>: Required
            XmlSchemaElement lastNameElement = new XmlSchemaElement();
            lastNameElement.Name = "LastName";
            lastNameElement.MinOccurs = 1;
            lastNameElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            // <Author>: Required
            XmlSchemaElement authorElement = new XmlSchemaElement();
            authorElement.Name = "Author";
            XmlSchemaComplexType authorType = new XmlSchemaComplexType();
            XmlSchemaSequence authorSequence = new XmlSchemaSequence();
            authorSequence.Items.Add(firstNameElement);
            authorSequence.Items.Add(lastNameElement);
            authorType.Particle = authorSequence;
            authorElement.SchemaType = authorType;

            // <Name>: Required
            XmlSchemaElement nameElement = new XmlSchemaElement();
            nameElement.Name = "Name";
            nameElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            nameElement.MinOccurs = 1;

            // <PlaceOfPublishing>: Not required
            XmlSchemaElement placeOfPublishingElement = new XmlSchemaElement();
            placeOfPublishingElement.Name = "PlaceOfPublishing";
            placeOfPublishingElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            placeOfPublishingElement.MinOccurs = 0;

            // <PublishingHouse>: Required
            XmlSchemaElement publishingHouseElement = new XmlSchemaElement();
            publishingHouseElement.Name = "PublishingHouse";
            publishingHouseElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            publishingHouseElement.MinOccurs = 1;

            // <YearOfPublishing>: Required
            XmlSchemaElement yearOfPublishingElement = new XmlSchemaElement();
            yearOfPublishingElement.Name = "YearOfPublishing";
            yearOfPublishingElement.SchemaTypeName = new XmlQualifiedName("positiveInteger", "http://www.w3.org/2001/XMLSchema");
            yearOfPublishingElement.MinOccurs = 1;

            // <CountOfPages>: Required
            XmlSchemaElement countOfPagesElement = new XmlSchemaElement();
            countOfPagesElement.Name = "CountOfPages";
            countOfPagesElement.SchemaTypeName = new XmlQualifiedName("positiveInteger", "http://www.w3.org/2001/XMLSchema");
            countOfPagesElement.MinOccurs = 1;

            // <Notes>: Not required
            XmlSchemaElement notesElement = new XmlSchemaElement();
            notesElement.Name = "Notes";
            notesElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            notesElement.MinOccurs = 0;

            // <ISBN>: Required
            XmlSchemaElement ISBNElement = new XmlSchemaElement();
            ISBNElement.Name = "ISBN";
            ISBNElement.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            ISBNElement.MinOccurs = 1;
    
            XmlSchemaComplexType bookType = new XmlSchemaComplexType();
            XmlSchemaSequence bookSequence = new XmlSchemaSequence();
            bookSequence.Items.Add(nameElement);
            bookSequence.Items.Add(authorElement);           
            bookSequence.Items.Add(placeOfPublishingElement);
            bookSequence.Items.Add(publishingHouseElement);
            bookSequence.Items.Add(yearOfPublishingElement);
            bookSequence.Items.Add(countOfPagesElement);
            bookSequence.Items.Add(notesElement);
            bookSequence.Items.Add(ISBNElement);
            bookType.Particle = bookSequence;

            XmlSchemaElement book = new XmlSchemaElement();
            book.Name = "Book";
            book.SchemaType = bookType;
            
            XmlSchema bookSchema = new XmlSchema();
            bookSchema.Items.Add(book);

            return bookSchema;
        }

    }
}
