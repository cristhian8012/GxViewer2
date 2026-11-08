using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    public static class GXConstants
    {
        public static List<string> GetTags()
        {
            List<string> _listtags = new List<string>();
            System.Reflection.Assembly _asm = System.Reflection.Assembly.GetExecutingAssembly();
            Type[] _types =  _asm.GetTypes();

            foreach (Type t in _types)
            {
                _listtags.Add(t.Name);
            }

            return _listtags;

        }

    }
}
