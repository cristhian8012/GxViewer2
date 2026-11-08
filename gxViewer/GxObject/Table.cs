using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Table
    {
        public List<string> Key { get; set; }
        public Info Info { get; set; }
        public int Id { get; set; }
        public Documentation Documentation { get; set; }
        public List<TblIndex> TblIndex { get; set; }
    }
}
