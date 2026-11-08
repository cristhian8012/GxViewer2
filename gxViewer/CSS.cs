using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer
{
    public static class CSS
    {
        public static string CSS_Style_Div_Table
        {
            get
            {
                return @"
.divTable{
  display: table;
  width: 100%;
}
.divTableRow
{
  display: table-row;
}
.divTableHeading
{
 background-color: #EEE;
 display: table-header - group;
}
.divTableCell, .divTableHead
{
 border: 0px solid #999999;
 display: table-cell;
 padding: 3px 10px;
 text-align: left;
 vertical-align: top;
}
.divTableHeading
{
  background-color: #EEE;
  display: table-header - group;
  font-weight: bold;
}
.divTableFoot
{
 background-color: #EEE;
 display: table-footer-group;
 font-weight: bold;
}
.divTableBody
{
  display: table-row-group;
}";
            }
        }

        public static string CSS_Style_Layout_Procedure
        {
            get
            {
                return CSS_Style_Div_Table + "\n" + @"
.PrintBlock {
	position:relative; border:1px; border-style: solid;
}

.NameBlock {
	font-size:10px; font-weight: bold; font-family:Arial; left:0px; display:block;min-width:70px;
}

.TextAndVar {
	position:absolute; display:block; overflow:hidden;
}";
            }
        }

    }
}
