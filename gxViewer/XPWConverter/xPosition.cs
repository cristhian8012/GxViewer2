using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer.XPWConverter
{
    public class xPosition
    {
        public int initOpenBracket { get; set; }
        public int endOpenBracket { get; set; }

        public int initCloseBracket { get; set; }
        public int endCloseBracket { get; set; }

        public string ContentText { get; set; }
        public int LengthTxt { get; set; }

        public static void UpdateEndPostions(ref List<xPosition> pList, int pStart, int pIncrement)
        {
            for (int i = pStart; i < pList.Count; i++)
            {
                if (i == pStart)
                {
                    pList[i].initCloseBracket += pIncrement;
                    pList[i].endCloseBracket += pIncrement;
                }
                else
                {
                    pList[i].initOpenBracket += pIncrement;
                    pList[i].endOpenBracket += pIncrement;
                    pList[i].initCloseBracket += pIncrement;
                    pList[i].endCloseBracket += pIncrement;
                }
                
            }

        }

    }
}
