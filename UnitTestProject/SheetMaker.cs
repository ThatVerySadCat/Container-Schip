using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

using Container_Schip;

namespace UnitTestProject
{
    public class SheetMaker
    {        /// <summary>

        /// Generates an Excel sheet and displays it within Excel based on the given ship.
        /// </summary>
        /// <param name="ship">The ship on which to base the sheet.</param>
        public static void ShipToExcelSheet(Ship ship)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();
            oXL.Visible = true;

            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            for (int x = 0; x < ship.Width; x++)
            {
                oSheet.Cells[3, x + 4] = x.ToString();
            }
            for (int y = 0; y < ship.Length; y++)
            {
                oSheet.Cells[y + 4, 3] = y.ToString();
            }

            int halfLength = ship.Length / 2;
            int halfWidth = ship.Width / 2;
            foreach (ContainerStack stack in ship.iContainerStacks)
            {
                oSheet.Cells[1, 2] = "Bottom: " + ship.GetBottomSideWeight();
                oSheet.Cells[2, 1] = "Left: " + ship.GetLeftSideWeight();
                oSheet.Cells[3, 2] = "Top: " + ship.GetTopSideWeight();
                oSheet.Cells[2, 3] = "Right: " + ship.GetRightSideWeight();

                string cellString = "";
                foreach (Container container in stack.iContainers)
                {
                    cellString += container.Type.ToString() + " - (" + container.Weight + ") | ";
                }

                oSheet.Cells[stack.Y + 4, stack.X + 4] = cellString;
            }
        }
    }
}
