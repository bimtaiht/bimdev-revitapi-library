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

namespace Model.Application
{
    public class RibbonApp : IExternalApplication
    {
        private RibbonData ribbonData => RibbonData.Instance;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonData.Application = application;

            var tab = EntTabUtil.Get("BIMDev-MEP");
            var panel = tab.GetPanel("Pipe");
            panel.GetPushButton("Sprinkler Connect", "Model.RevitCommand.PipeSprinklerCommand", "Resource/Icon/sprinklerconnect.ico");

            tab.CreateTab();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
