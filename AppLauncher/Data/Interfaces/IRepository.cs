using AppLauncher.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace AppLauncher.Data.Interfaces
{
    public interface IRepository
    {
        void Add(int pageIndex);
        void Update(IPage page);
        IEnumerable<IPage> GetAll();

        event EventHandler<PageEventArgs> PageAdded;
        event EventHandler<PageEventArgs> PageUpdated;
    }
}
