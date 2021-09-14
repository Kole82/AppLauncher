using AppLauncher.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AppLauncher.Models.Xml
{
    [Serializable]
    public class Page : IPage
    {
        #region IPage Members

        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlArray]
        public List<Shortcut> Shortcuts { get; set; }

        #endregion
    }
}
