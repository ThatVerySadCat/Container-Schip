using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop;

namespace UnitTestProject
{
    public class SheetMaker
    {
        public static void CreateExcelSheet()
        {
            /*Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();
            oXL.Visible = true;

            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            for (int x = 0; x < width; x++)
            {
                oSheet.Cells[3, x + 4] = x.ToString();
            }
            for (int y = 0; y < length; y++)
            {
                oSheet.Cells[y + 4, 3] = y.ToString();
            }

            int halfLength = length / 2;
            int halfWidth = width / 2;
            foreach (ContainerStack stack in containerStacks)
            {
                oSheet.Cells[1, 2] = "Bottom: " + GetBottomSideWeight();
                oSheet.Cells[2, 1] = "Left: " + GetLeftSideWeight();
                oSheet.Cells[3, 2] = "Top: " + GetTopSideWeight();
                oSheet.Cells[2, 3] = "Right: " + GetRightSideWeight();

                string cellString = "";
                foreach (Container container in stack.iContainers)
                {
                    cellString += container.Type.ToString() + " - (" + container.Weight + ") | ";
                }

                oSheet.Cells[stack.Y + 4, stack.X + 4] = cellString;
            }*/
        }
    }
}
