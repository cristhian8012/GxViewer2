using System;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Variable
    {
        private string _id;
        private string _Name = "";
        private string _Title;
        private string _Type = "numeric";
        private int _Length;
        private int _Decimals;
        private string _Picture;
        private string _BasedOn;
        private int _Rows = 0;
        private int _Columns = 0;
        private Property _property;
        private string _UserDefType;
        private string _DefaultValue;
        private object _CurrentValue;
        private string _Direction = "InOut";

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title= value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string UserDefType
        {
            get { return _UserDefType; }
            set { _UserDefType = value; }
        }

        public string DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }

        public object CurrentValue
        {
            get { return _CurrentValue; }
            set { _CurrentValue = value; }
        }

        public int Length
        {
            get 
            {
                if (this._Length <= 0 && this._Type.ToLower() == "numeric")
                {
                    if (!String.IsNullOrEmpty(_Picture))
                    {
                        this._Length = this._Picture.Length;
                    }
                }
                else
                {
                    if (this._Length <= 0 && this._Type.ToLower() == "character")
                    {
                        this._Length = 10;
                    }
                 }

                return _Length; 
            }
            set { _Length = value; }
        }
        public int Decimals
        {
            get { return _Decimals; }
            set { _Decimals = value; }
        }

        public int Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }
        public int Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        public Property Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public string BasedOn
        {
            get { return _BasedOn; }
            set { _BasedOn = value; }
        }

        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        public int Decimales 
        {
            get
            {
                int _x = 0;
                if (this._Decimals <= 0 && this._Type=="numeric")
                {
                    if (!String.IsNullOrEmpty(_Picture))
                    {
                        int _index =  this._Picture.IndexOf( ".");
                        if (_index > 0)
                        {
                            string _strDecs = this._Picture.Substring(_index + 1);
                            _x = _strDecs.Length;
                        }
                    }
                }
                else
                {
                    _x = this._Decimals;
                }


                return _x;
            }
        }

        /// <summary>
        /// Obtiene nombre incluyendo &
        /// </summary>
        public string Name2
        {
            get { return String.Format("&{0}", this._Name.ToLower()); }
        }


        /// <summary>
        /// Obtiene nombre
        /// </summary>
        public string NombreVar
        {
            get 
            {
                string _retVarName = _Name;
                if ( _Rows > 0)
                {
                    if (_Columns > 0)
                    {
                        _retVarName = _retVarName + "(" + _Rows.ToString() + ","+ _Columns.ToString() + ")";
                    }
                    else
                    {
                        _retVarName = _retVarName + "(" + _Rows.ToString() + ")";
                    }
                }
                return _retVarName;
            }
        }

        public string TipoVar
        {
            get
            {
                string _retName = "";
                if (this.Decimales > 0)
                {
                    _retName = _retName + String.Format("{0}({1}.{2})", this.Type.Substring(0, 1).ToUpper(), this.Length, this.Decimales);
                }
                else
                {
                    _retName = _retName + String.Format("{0}({1})", this.Type.Substring(0, 1).ToUpper(), this.Length);
                }
                return _retName;
            }
        }


        public static Variable CreateVarFromRow(System.Data.DataRow prow)
        {
            Variable _var = new Variable();
            if (prow != null)
            {
                foreach (System.Data.DataColumn c in prow.Table.Columns)
                {
                    object _val = prow[c.ColumnName];
                    if (_val != null && _val != DBNull.Value)
                    {
                        System.Reflection.PropertyInfo _prop = _var.GetType().GetProperty(c.ColumnName);
                        if (_prop != null)
                        {
                            if (_prop.CanWrite)
                            {
                                _prop.SetValue(_var, _val, null);
                            }
                        }
                    }
                }
            }
            return _var;
        }

        public string getFixedStringName()
        {
            string _retName = this.NombreVar.PadRight(25);
            if (this.Decimales > 0)
            {
                _retName = _retName + String.Format("{0}({1}.{2})", this.Type.Substring(0, 1).ToUpper(), this.Length, this.Decimales).PadRight(15);
            }
            else
            {
                _retName = _retName + String.Format("{0}({1})", this.Type.Substring(0, 1).ToUpper(), this.Length).PadRight(15);
            }
            _retName = _retName + (this.Picture == null ? "" : this.Picture ).PadLeft(20);
            if (this.Length <= 0)
            {
                if (!String.IsNullOrEmpty( this.BasedOn))
                {
                    _retName = _retName + "  " + this.BasedOn;
                }
            }
            return _retName;
        }

        public string getToolTipVar()
        {

            string _retname = $"Titulo: {this.Title}\nTipo: {this.Type}({this.Length})";

            if (this.Decimales > 0)
            {
                _retname = $"Titulo: {this.Title}\nTipo: {this.Type}({this.Length},{this.Decimales})";
            }

            if (this.Picture != null)
            {
                _retname += $"\nPicture: {this.Picture}";
            }

            if (this.BasedOn != null)
            {
                _retname += $"\nBasado en: {this.BasedOn}";
            }

            return _retname;
        }

        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }

    }
}
