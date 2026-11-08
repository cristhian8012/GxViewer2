using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Report : GXObj
    {

        private Layout _Layout;
        public Layout Layout
        {
            get { return _Layout; }
            set
            {
                //Console.WriteLine(this.Tipo + "==>" + this.Info.Name);
                _Layout = value;
                if (_Layout != null)
                {
                    if (_Layout.Blocks != null)
                    {
                        StringBuilder _strb = new StringBuilder();
                        int _IdBlock = 0;
                        string _strNameCtrl = String.Empty;
                        foreach (GxObject.Block _b in _Layout.Blocks)
                        {
                            if (_b.CodeBlock != null)
                            {
                                _strb.Append(_b.CodeBlock.Source);
                            }
                            if (_b.PrintBlock != null)
                            {
                                if (_b.PrintBlock.CtrlName == null)
                                {
                                    if (_IdBlock == 0)
                                    {
                                        _strNameCtrl = String.Format("P_{0}", this.Name);
                                    }
                                    else
                                    {
                                        _strNameCtrl = String.Format("P_{0}{1}", this.Name, _IdBlock);
                                    }

                                    _b.PrintBlock.CtrlName = _strNameCtrl;

                                    if (_b.PrintBlock.CommandLine == null)
                                    {
                                        _b.PrintBlock.CommandLine = String.Format("print {0}", _b.PrintBlock.CtrlName);
                                    }

                                    _IdBlock++;
                                }
                                else
                                {
                                    if (_b.PrintBlock.CommandLine == null)
                                    {
                                        _b.PrintBlock.CommandLine = String.Format("print {0}", _b.PrintBlock.CtrlName);
                                    }
                                }


                                _strb.AppendLine(_b.PrintBlock.CommandLine.Replace("[Unreferenced]", String.Empty));
                            }
                        }
                        base.Source = _strb.ToString();
                    }
                }
            }
        }

        public override string Tipo
        {
            get
            {
                return "Report";
            }
        }


    }
}
