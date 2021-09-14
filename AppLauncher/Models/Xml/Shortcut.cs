using AppLauncher.Models.Interfaces;
using System;
using System.Xml.Serialization;

namespace AppLauncher.Models.Xml
{
    [Serializable]
    public class Shortcut : IShortcut
    {
        #region IShortcut Members

        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string Path { get; set; }

        [XmlElement]
        public byte[] Icon { get; set; }

        #endregion
    }
}
