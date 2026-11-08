using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Procedure : GXObj
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

                            if (!this.Version_Genexus.Contains("18"))
                            {
                                base.Source = _strb.ToString();
                            }
                        }
                    }
                }
        }

        [Category("Options")]
        [Description("Report Output")]
        public string ReportOutput 
        {
            get 
            { 
                string _ret = String.Empty;
                if (this.Propiedades.ContainsKey("OUTPUT"))
	                {
                        switch (this.Propiedades["OUTPUT"].Value)
                        {
                            case "PRN": _ret = "Only to Printer"; break;
                            case "FIL": _ret = "Only To File"; break;
                            case "SCR": _ret = "Only To Screen"; break;
                            case "ASK": _ret = "Ask User"; break;

                            default:
                                _ret = "Ask User";
                                break;
                        }
	                }
                return _ret;
            } 
        }



        [Category("Options")]
        [Description("Confirmation")]
        public string Confirmation
        {
            get
            {
                string _ret = String.Empty;
                if (this.Propiedades.ContainsKey("TRNCFM") || this.Propiedades.ContainsKey("ConfirmTransaction"))
                {
                    string _keyProp = (this.Propiedades.ContainsKey("TRNCFM") ? "TRNCFM" : "ConfirmTransaction");

                    switch (this.Propiedades[_keyProp].Value.ToUpper())
                    {
                        case "NONE": _ret = "Never prompt"; break;
                        case "YES": _ret = "Always prompt"; break;
                        case "UMPV": _ret = "Use model's property value"; break;

                        default:
                            _ret = "Use model's property value";
                            break;
                    }
                }
                else
                {
                    if (this.Propiedades.ContainsKey("CONFIRM"))
                    {
                        _ret =  Propiedades["CONFIRM"].Rule;
                    }
                }
                return _ret;
            }
        }


        [Category("Transaction Integrity")]
        [Description("Commit on Exit")]
        public string CommitOnExit
        {
            get
            {
                string _ret = "Yes";
                if (this.Propiedades.ContainsKey("TRNEND"))
                {
                    switch (this.Propiedades["TRNEND"].Value.ToUpper())
                    {
                        case "NO": _ret = "No"; break;
                        case "YES": _ret = "Yes"; break;

                        default:
                            _ret = "Yes";
                            break;
                    }
                }
                return _ret;
            }
        }

        [Category("Transaction Integrity")]
        [Description("Commitment")]
        public string Commitment
        {
            get
            {
                string _ret = "Enabled";
                if (this.Propiedades.ContainsKey("TRNCMT"))
                {
                    switch (this.Propiedades["TRNCMT"].Value.ToUpper())
                    {
                        case "NO": _ret = "Disabled"; break;
                        case "YES": _ret = "Enabled"; break;

                        default:
                            _ret = "Enabled";
                            break;
                    }
                }
                return _ret;
            }
        }

 


        public override string Tipo
        {
            get
            {
                return "Procedure";
            }
        }

    }
}
