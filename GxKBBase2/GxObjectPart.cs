using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxKBBase18
{
    public class GxObjectPart
    {
        public Guid EntityGuid { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int EntityVersionId { get; set; }
        public string Name { get; set; }
        public byte[] RawData { get; set; }

        public bool HasBinaryData =>
            RawData != null && RawData.Length > 0;
    }
}
