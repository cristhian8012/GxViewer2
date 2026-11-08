using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace gxViewer.GxObject
{
    [Serializable]
    [DefaultPropertyAttribute("Info")]
    public class GXObj
    {
        private GxObject.Documentation _Documentation;
        private GxObject.Help _Help;
        private GxObject.ObjInfo _ObjInfo;
        private GxObject.Info _Info = new Info();

        private GxObject.Variable[] _variables;
        private GxObject.GXAtt[] _GXAtt;

        private Dictionary<string, GxObject.Property> _Props = new Dictionary<string, Property>();
        private Dictionary<string, GxObject.Variable> _Vars = new Dictionary<string, Variable>();
        private Dictionary<string, string> _IdVars = new Dictionary<string, string>();
        private Dictionary<string, GxObject.GXAtt> _GxAttrs = new Dictionary<string, GXAtt>();

        private IList<string> _linesCode;
        private IList<string> _linesrules;
        private IList<string> _linesCondition;

        private GxObject.Group[] _groups;
        private GxObject.Folder[] _folders;

        private string _Rules;
        private string _Subroutines;
        private string _LastUpdate;
        private string _Source;
        private string _Conditions;

        private string _strLayoutText="";
        private string _strLayoutVars = "";
        private string _strDocuemntationText="";
        //private string _strCodeStaticText = "";
        //private string _strRuleStaticText = "";

        private KMW _KMW;
        private Model _model = new Model();

        private string _XMLObj;

        [Category("Diseño")]
        [Description("Tipo de Objeto")]
        public virtual string Tipo
        {
            get { return this.ToString(); }
        }


        #region readonly Property by ProperyGrid
        [Category("Diseño")]
        public string Name
        {   
            get { return _Info.Name;  }
        }
        [Category("Diseño")]
        public string Description
        {
            get { return _Info.Description; }
        }
        [Category("Diseño")]
        public string Folder
        {
            get { return  _Info.Folder; }
        }
        
        [Category("Modelo")]
        public string Modelo
        {
            get { return _KMW.Path; }
            set { _KMW.Path = value; }
        }

        [Category("Modelo")]
        public string IdModelo
        {
            get { return _model.Id; }
        }

        [Category("Modelo")]
        public string NombreModelo
        {
            get { return _model.Name; }
        }

        [Category("Modelo")]
        public string TipoModelo
        {
            get { return _model.Type; }
        }

        [Category("Modelo")]
        public string Version
        {
            get { return String.Format("{0}.{1}.{2}", _KMW.MajorVersion,_KMW.MinorVersion, _KMW.MaxGxBuildSaved )  ; }
        }

        [Category("Varios")]
        [Description("Versión de Genexus desde donde fue exportado el XPZ")]
        [DisplayName("Versión de Genexus")]
        public string Version_Genexus
        {
            get { return (_model == null ? "" : _model.Version); }
        }

        #endregion

        public void AddLayoutText(string pText)
        {
            if (!String.IsNullOrEmpty(pText))
            {
                _strLayoutText += $"{pText} ";
            }
        }

        public void AddLayoutVars(string pText)
        {
            if (!String.IsNullOrEmpty(pText))
            {
                _strLayoutVars += $"&{pText.Replace("&","")} ";
            }
        }

        public void SetLinesCode(IList<string> pListString)
        {
            _linesCode = pListString;
        }
        public void SetLinesRule(IList<string> pListString)
        {
            _linesrules = pListString;
        }

        public void SetLinesCondition(IList<string> pListString)
        {
            _linesCondition = pListString;
        }

        public void setLayoutText(string pText)
        {
            _strLayoutText = pText;
        }

        public string getLayoutText()
        {
            return _strLayoutText;
        }

        public void setLayoutVars(string pText)
        {
            _strLayoutVars = pText;
        }

        public string getLayoutVars()
        {
            return _strLayoutVars.ToLower();
        }


        public IList<string> getLinesCode()
        {
            return _linesCode;
        }
        public IList<string> getLinesRule()
        {
            return _linesrules;
        }
        public IList<string> getLinesCondition()
        {
            return _linesCondition;
        }


        public void setDocumentationText(string pText)
        {
            _strDocuemntationText = pText;
        }

        public string getDocumentationText()
        {
            return _strDocuemntationText;
        }


        public string GetName()
        {
            string _p = "";
            switch (this.Tipo)
            {
                case "Procedure":
                case "WorkPanel":
                case "Report":
                    _p = this.Tipo.Substring(0, 1);
                    break;
                case "WebPanel":
                    _p = "H"; break;
            }
            return String.Format("{0}{1}", _p, this.Name);
        }

        public string GetTotalSource()
        {
            StringBuilder _strb = new StringBuilder();
            _strb.AppendLine("// RULES--");
            _strb.AppendLine(this.Rules);

            _strb.AppendLine("// SOURCE--");
            _strb.AppendLine(this.Source);

            _strb.AppendLine("// SUBROUTINES--");
            _strb.AppendLine(this.Subroutines);

            _strb.AppendLine("// CONDITIONS--");
            _strb.AppendLine(this.Conditions);

            return _strb.ToString().ToLower();
        }

        public Variable getVarById(string varId)
        {
            try
            {
                string _namevar = _IdVars[varId];
                Variable _var = _Vars[_namevar];
                return _var;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Variable getVarByName( string varName)
        {
            try
            {
                Variable _var = _Vars[varName];
                return _var;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [BrowsableAttribute(false)]
        public GxObject.Documentation Documentation
        {
            get { return _Documentation; }
            set { _Documentation=value; }
        }


        [BrowsableAttribute(false)]
        public GxObject.Info Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        [BrowsableAttribute(false)]
        public GxObject.Help Help
        {
            get { return _Help; }
            set { _Help = value; }
        }
        [BrowsableAttribute(false)]
        public GxObject.Variable[] Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }

        [BrowsableAttribute(false)]
        public GxObject.GXAtt[] GXAtts
        {
            get { return _GXAtt; }
            set { _GXAtt = value; }
        }

        [BrowsableAttribute(false)]
        public Dictionary<string, GxObject.Property> Propiedades
        {
            get { return _Props; }
            set { _Props = value; }
        }

        [BrowsableAttribute(false)]
        public Dictionary<string, GxObject.Variable> GXVariables
        {
            get { return _Vars; }
            set { _Vars = value; }
        }

        [BrowsableAttribute(false)]
        public Dictionary<string, string> IdVariables
        {
            get { return _IdVars; }
            set { _IdVars = value; }
        }

        [BrowsableAttribute(false)]
        public Dictionary<string, GxObject.GXAtt> GXAtributos
        {
            get { return _GxAttrs; }
            set { _GxAttrs = value; }
        }


        [BrowsableAttribute(false)]
        public GxObject.Group[] Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        [BrowsableAttribute(false)]
        public GxObject.Folder[] Folders
        {
            get { return _folders; }
            set { _folders = value; }
        }

        [BrowsableAttribute(false)]
        public GxObject.ObjInfo ObjInfo
        {
            get { return _ObjInfo; }
            set { _ObjInfo = value; }
        }

        public string CreationDate
        {
            get; set;
        }

        public string LastUpdate
        {
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
        }

        [BrowsableAttribute(false)]
        public string Rules
        {
            get { return _Rules; }
            set { _Rules = value; }
        }
        [BrowsableAttribute(false)]
        public string Subroutines
        {
            get { return _Subroutines; }
            set { _Subroutines = value; }
        }

        [BrowsableAttribute(false)]
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        [BrowsableAttribute(false)]
        public string Conditions
        {
            get { return _Conditions; }
            set { _Conditions  = value; }
        }

        [BrowsableAttribute(false)]
        public KMW KMW
        {
            get { return _KMW; }
            set { _KMW = value; }
        }

        [BrowsableAttribute(false)]
        public Model Model
        {
            get { return _model; }
            set { _model = value; }
        }


        [BrowsableAttribute(false)]
        public bool IsXML { get; set; } = true;
        


        [BrowsableAttribute(false)]
        public string XMLObj
        {
            get { return _XMLObj; }
            set { _XMLObj = value; }
        }

       


    }
}
