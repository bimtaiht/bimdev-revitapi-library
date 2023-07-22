using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Model.RevitCommand;

namespace Model.Application
{
    public class RibbonApp : IExternalApplication
    {
        private RibbonData ribbonData => RibbonData.Instance;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonData.Application = application;

            //(new RevitCommand.Command()).CreateRibbon();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
