using System;

namespace AppLauncher.Models.Interfaces
{
    public interface IShortcut
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        byte[] Icon { get; set; }
    }
}
