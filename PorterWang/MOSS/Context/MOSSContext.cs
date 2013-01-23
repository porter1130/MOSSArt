using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace MOSSArt
{
    public class MOSSContext
    {
        private static MOSSContext _context = null;

        public static MOSSContext Current
        {
            get { return MOSSContext._context; }
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
    }
}
