using System.Data;

namespace MOSSArt
{
    public class DataTableWrapper
    {
        private DataTable _dt = new DataTable();

        public DataTableWrapper(DataTable dt)
        {
            _dt = dt;
        }

        public DataTable GetTable()
        {
            return _dt;
        }
    }
}
