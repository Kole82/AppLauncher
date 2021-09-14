using AppLauncher.Models.Xml;
using System;
using System.Collections.Generic;

namespace AppLauncher.Models.Interfaces
{
    public interface IPage
    {
        Guid Id { get; set; }
        string Name { get; set; }
        List<Shortcut> Shortcuts { get; set; }
    }
}
