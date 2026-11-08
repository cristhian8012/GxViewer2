using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer
{
    public static class CSS_HTML
    {
        public static string Page_Layout_Format
        {
            get
            {
                string _formatHTML = @"
<html>
<head>
<style>
{0}    
</style>
</head>
<body>
{1}
</body>
<footer>
{2}
</footer>
</html>
";
                return _formatHTML;
            }

        }


        public static string Body_Table_Format
        {
            get
            {
                //                string _formatHTML = @"
                //	<div class='divTable'>
                //        <div class='divTableBody'>
                //            {0}
                //        </div>
                //    </div>
                //";
                string _formatHTML = @"
<Table>
    <Tbody>
        {0}
    </Tbody>
</Table>
";

                return _formatHTML;
            }

        }

        public static string Body_Table_Row_Format
        {
            get
            {
                //            string _formatHTML = @"
                //            <div class='divTableRow'>
                //                {0} 
                //</div>";
                string _formatHTML = @"
<tr>
    {0} 
</tr>";

                return _formatHTML;
            }

        }

        public static string Body_Table_Row_Col_Format
        {
            get
            {
                //string _formatHTML = @"
                //    <div class='divTableCell' style='{1}'>
                //        {0}
                //    </div>";

                string _formatHTML = @"
<td class='divTableCell' style='{1}'>
    {0}
</td>";
                return _formatHTML;
            }

        }

        public static string Block_Layout_HTML(string ptitle, int pWitdh, int pHeight,  string LayoutControls)
        {
            //get
            {
                //Body_Table_Row_Format
                string _Result = String.Empty;
                string _Title = String.Format("<div id='LabelBlock' class='NameBlock'>{0}</div>", ptitle);
                string _cellTitle = String.Format(Body_Table_Row_Col_Format, _Title, "width: 7%;");

                string _formatBlock = @"
						<div id='Block' class='PrintBlock' style='width:{0}px; height:{1}px;'>
                            {2}
                        </div> ";
                string _ConTentBock = String.Format(_formatBlock, pWitdh, pHeight, LayoutControls);

                string _cellContent = String.Format(Body_Table_Row_Col_Format, _ConTentBock, "width: 90%;");

                _Result = String.Format(Body_Table_Row_Format, String.Format("{0}\n{1}",_cellTitle, _cellContent) ) ;

                return _Result;
            }

        }


    }
}
