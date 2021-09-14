using AppLauncher.Data.Interfaces;
using AppLauncher.Models.Interfaces;
using AppLauncher.Models.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace AppLauncher.Data.Xml
{
    public class PageRepository : IRepository
    {
        #region Private Fields

        private readonly IList<Page> _pages;
        private readonly string _file;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageRepository(string file = "data.xml")
        {
            _file = file;

            //TODO refactor
            if (File.Exists(_file))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Page>));

                using (FileStream fileStream = new FileStream(_file, FileMode.Open))
                {
                    _pages = (List<Page>)formatter.Deserialize(fileStream);
                }
            }
            else
            {
                //TODO refactor
                _pages = new List<Page>();

                _pages.Add(new Page
                {
                    Id = Guid.NewGuid(),
                    Name = "New Page",
                    Shortcuts = new List<Shortcut>()
                });
            }
        }

        #endregion

        #region Public Events

        public event EventHandler<PageEventArgs> PageAdded;
        public event EventHandler<PageEventArgs> PageUpdated;

        #endregion

        #region IRepository Members

        public void Add(int pageIndex)
        {
            Page page = new Page
            {
                Id = Guid.NewGuid(),
                Name = "New Page",
                Shortcuts = new List<Shortcut>()
            };

            if (++pageIndex <= _pages.Count - 1)
            {
                _pages.Insert(pageIndex, page);
            }
            else
            {
                _pages.Add(page);
            }

            Save();

            PageAdded?.Invoke(this, new PageEventArgs(page));
        }

        public void Update(IPage page)
        {
            int index = _pages.IndexOf(_pages.Where(p => p.Id ==page.Id).First());
            _pages[index] = (Page)page;

            Save();

            PageUpdated?.Invoke(this, new PageEventArgs(page));
        }
        
        public IEnumerable<IPage> GetAll() => new List<IPage>(_pages);

        #endregion

        #region Private Helpers

        private void Save()
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Page>));

                using (FileStream fs = new FileStream(_file, FileMode.Create))
                {
                    formatter.Serialize(fs, _pages);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
