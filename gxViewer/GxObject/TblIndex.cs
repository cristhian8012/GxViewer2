using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class TblIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string UniqueDuplicate { get; set; }
        public List<IdxAttri> IdxAttri { get; set; }
    }
}
