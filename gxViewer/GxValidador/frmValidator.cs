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
    public partial class frmValidator : Form
    {
        private ValidatorGX _objValidator;

        public frmValidator( ValidatorGX _objVal  )
        {
            _objValidator = _objVal;
            InitializeComponent();
        }

        private void frmValidator_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("{0}Validator   |    {1} : {2} ({3}) ", _objValidator.Name , _objValidator.ObjetoGX.Tipo, _objValidator.ObjetoGX.Info.Name, _objValidator.ObjetoGX.Info.Description);
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


            GXHtmlRules.LoadVars(_objValidator.ObjetoGX);

            foreach (GXValRegla _rule in _objValidator.Reglas)
            {

                Type _tRule = typeof(GXHtmlRules); 
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