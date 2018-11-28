using LibrarySystem.Entities.Interfaces;
using System.Xml;
using System.Xml.Linq;

namespace LibrarySystem.Actions.Interfaces
{
    public interface IEntityActions
    {
        IEntity ParseEntity(XElement element);

        void Write(IEntity source, XmlWriter writer);
    }
}
