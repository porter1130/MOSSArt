using Microsoft.SharePoint;
using System;

namespace MOSSArt
{
    public class MOSSContext : IDisposable
    {
        private static MOSSContext _context;

        public static MOSSContext Current
        {
            get { return _context; }
        }

        private SPWeb _web;

        public SPWeb Web
        {
            get { return _web; }
        }

        private SPSite _site;

        public SPSite Site
        {
            get { return _site; }
        }


        private MOSSContext(SPWeb web)
        {
            _web = web;
            _site = web.Site;
        }

        public static MOSSContext GetContext(SPWeb web)
        {
            if (_context == null)
            {
                _context = new MOSSContext(web);
            }

            return _context;
        }

        public void Dispose()
        {
            _context = null;
        }
    }
}
