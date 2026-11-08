using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Block
    {
        private CodeBlock _codblock;
        public CodeBlock CodeBlock
        {
            get { return _codblock; }
            set { _codblock = value; }
        }
        public CodeBlock CodeBlockVer8
        {
            get { return _codblock; }
            set { _codblock = value; }
        }

        private PrintBlock _PrintBlock;
        public PrintBlock PrintBlock
        {
            get { return _PrintBlock; }
            set { _PrintBlock = value; }
        }

        public PrintBlock PrintBlockVer8
        {
            get { return _PrintBlock; }
            set { _PrintBlock = value; }
        }


        private GxObject.Text[] _texts;
        public GxObject.Text[] Texts 
        {
            get { return _texts; }
            set { _texts = value; }
        }

        private GxObject.Edit[] _Edits;
        public GxObject.Edit[] Edits
        {
            get { return _Edits; }
            set { _Edits = value; }
        }

        private Frame _Frame;
        public Frame Frame
        {
            get { return _Frame; }
            set { _Frame = value; }
        }

        private Button[] _buttons;
        public Button[] Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        private SubFile[] _subfiles;
        public SubFile[] Subfiles
        {
            get { return _subfiles; }
            set { _subfiles = value; }
        }
	

    }
}
