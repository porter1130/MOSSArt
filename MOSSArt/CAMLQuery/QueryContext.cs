using System.Collections.Generic;
using Microsoft.SharePoint;

namespace MOSSArt.CAMLQuery
{
    public class QueryContext
    {
        internal QueryContext() { }

        public SPList List{get;set;}

        public string ListName { get; set; }

        public IFieldRef[] ViewFields { get; set; }

        public uint RowLimit { get; set; }

        public ICAMLExpression Query { get; set; }

        public IDictionary<IFieldRef, bool> OrderByFields = new Dictionary<IFieldRef, bool>();

        public IFieldRef GroupByField { get; set; }
    }

    
}
