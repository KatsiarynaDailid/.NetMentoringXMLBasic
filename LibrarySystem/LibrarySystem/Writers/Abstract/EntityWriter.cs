using LibrarySystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LibrarySystem.Writers.Abstract
{
    public abstract class EntityWriter
    {
        public abstract void Write(IEntity source, XmlWriter writer);
    }
}
