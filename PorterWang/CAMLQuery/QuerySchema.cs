using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOSSArt.CAMLQuery
{

    public abstract class QuerySchema
    {
        private QuerySchema()
        {
        }

        public abstract string SchemaName { get; }

        public class Today : QuerySchema
        {
            private int _offsetDays;
            public const string SCHEMA_NAME = "{Today}";
            private const string Today_format = "<Today OffsetDays=\"{0}\" />";

            public Today()
            {
                this._offsetDays = 0;
            }

            public Today(int days)
            {
                this._offsetDays = 0;
                this.AddDays(days);
            }

            public void AddDays(int days)
            {
                this._offsetDays += days;
            }

            public static QuerySchema.Today operator +(QuerySchema.Today q, int iValue)
            {
                q.AddDays(iValue);
                return q;
            }

            public static QuerySchema.Today operator -(QuerySchema.Today q, int iValue)
            {
                q.AddDays(-iValue);
                return q;
            }

            public override string ToString()
            {
                if (this._offsetDays != 0)
                {
                    return string.Format("<Today OffsetDays=\"{0}\" />", this._offsetDays);
                }
                return "<Today/>";
            }

            public int OffsetDays
            {
                get
                {
                    return this._offsetDays;
                }
                set
                {
                    this._offsetDays = value;
                }
            }

            public override string SchemaName
            {
                get
                {
                    return "{Today}";
                }
            }
        }

        public class UserID : QuerySchema
        {
            public const string SCHEMA_NAME = "{Me}";

            public override string ToString()
            {
                return "<UserID/>";
            }

            public override string SchemaName
            {
                get
                {
                    return "{Me}";
                }
            }
        }
    }
}
