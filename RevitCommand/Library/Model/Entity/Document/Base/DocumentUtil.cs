using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using System.Windows;

namespace Utility
{
    public static partial class DocumentUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static ViewData viewData => ViewData.Instance;
        private static FormData formData => FormData.Instance;

        public static void SetCurrentViewAsWorkPlane(this Document doc)
        {
            var activeSP = viewData.ActiveSketchPlane;
            viewData.ActiveView.SketchPlane = activeSP;
        }

        public static string GetPathName(this Document q)
        {
            var document = q;

            string? pathName;
            try
            {
                var modelPath = document.GetWorksharingCentralModelPath();

                var centralServerPath = ModelPathUtils.ConvertModelPathToUserVisiblePath(modelPath);

                pathName = centralServerPath.ToString();
            }
            catch
            {
                pathName = document.PathName;
            }

            return pathName;
        }

        public static void DoTransaction(this Document q, string transactionName, Action action)
        {
            Action wrapperAction = () =>
            {
                using (var transaction = new Transaction(q, transactionName))
                {
                    transaction.Start();

                    action();

                    transaction.Commit();
                }
            };

            var isFormShow = formData.IsFormVisible && !formData.IsDialog;
            if (!isFormShow)
            {
                wrapperAction();
            }
            else
            {
                try
                {
                    revitData.ExternalEventHandler.Action = wrapperAction;
                    revitData.ExternalEvent!.Raise();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }
    }
}
