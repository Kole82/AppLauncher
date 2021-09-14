using AppLauncher.Models.Interfaces;
using System;

namespace AppLauncher
{
    public class PageEventArgs : EventArgs
    {
        #region Public Properties

        public IPage Page { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageEventArgs(IPage page)
        {
            Page = page;
        }

        #endregion
    }
}
