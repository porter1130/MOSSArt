﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOSSArt.CAMLQuery
{
    public class QueryField : FieldRef<object>
    {
        public QueryField(string name) : base(name) { }
    }
}
