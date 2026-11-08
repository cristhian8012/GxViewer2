using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace gxViewer.GxValidador
{
    public partial class frmAS400Validator : Form
    {
        private ValidatorAS400 _objValidator;

        public frmAS400Validator(ValidatorAS400 _objVal  )
        {
            _objValidator = _objVal;
            InitializeComponent();
        }

        private void frmAS400Validator_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("{0}  |    {1} : {2}  ", _objValidator.Name , _objValidator.ObjetoAS.GetNameType(), _objValidator.ObjetoAS.Name);
            this.ShowResult();
        }

        private void ShowResult()
        {

            string _strHeader = @"<html>
                                    <head>
                                        <style>
                                        .TableVariablesNoUsada {font-family:Arial, Helvetica, sans-serif ; font-size:12px; border-right:1px solid #000000; border-top:1px solid #000000;   }
                                        .TableVariablesTitulo { font-family:Arial, Helvetica, sans-serif ; font-size:14px; font-weight:bold; color:#FF0000; padding-bottom:20px; }
                                        .TableVariablesSubTitulo { font-family:Arial, Helvetica, sans-serif ; font-size:12px; font-weight:bold; color:#0000FF; padding-bottom:10px; }
                                        .FilaVariablesNoUsada {  color:#666666; }
                                        .CeldaVariablesNoUsada { width:138px; padding-right: 5px; padding-bottom:3px; border-bottom:1px solid #000000; border-left:1px solid #000000; }
                                        .CabeceraOrtografia { font-family:Arial, Helvetica, sans-serif ; padding-right: 5px; padding-bottom:3px; border-bottom:1px solid #000000; border-left:1px solid #000000; background-color: eeeeee; color:#5d5e53; }
                                        .CeldaOrtografia { padding-right: 5px; padding-bottom:3px; border-bottom:1px solid #000000; border-left:1px solid #000000; background-color: white; }
                                        </style>
                                    </head>
                                    <body>";
            StringBuilder _srtHTML = new StringBuilder(_strHeader);


            AS400HtmlRules.LoadVars(_objValidator.ObjetoAS);

            foreach (GXValRegla _rule in _objValidator.Reglas)
            {

                Type _tRule = typeof(AS400HtmlRules); 
                MethodInfo _tRuleFunc = _tRule.GetMethod(_rule.Metodo);
                if (_tRuleFunc != null)
                {
                    object[] _parms = { _rule };
                    _tRuleFunc.Invoke(this, _parms);
                }

                _srtHTML.AppendLine( _rule._OutPutHTML  );
            }
            _srtHTML.AppendLine("</body></html>");

            wbResult.DocumentText = _srtHTML.ToString();


        }

    }
}