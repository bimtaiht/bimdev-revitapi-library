using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Utility;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Model.Entity;

namespace Model.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public abstract class RevitCommand : IExternalCommand
    {
        protected RevitData revitData => RevitData.Instance;
        protected StructuralData structuralData => StructuralData.Instance;
        protected ArchitectData architectData => ArchitectData.Instance;
        protected MEPData mepData => MEPData.Instance;
        protected ViewData viewData => ViewData.Instance;
        protected WorksetData worksetData => WorksetData.Instance;
        protected IOData ioData => IOData.Instance;
        private FormData formData => FormData.Instance;

        protected UIApplication uiapp => revitData.UIApplication!;
        protected Autodesk.Revit.ApplicationServices.Application app => revitData.Application;
        protected UIDocument uidoc => revitData.UIDocument;
        protected Document doc => revitData.Document;
        protected Selection sel => revitData.Selection;

        protected virtual bool IsAutoDisposed => true;
        protected virtual bool HasExternalEvent => false;
        protected virtual bool IsExecute => true;
        protected virtual bool IsThrowWhenCatchException => false;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            revitData.UIApplication = commandData.Application;
            PreExecute();

            try
            {
                if (!IsExecute) return Result.Succeeded;
                Execute();
            }
            catch (Exception ex)
            {
                RevitDataUtil.Dispose();

                var mess = $"{ex.Message}\n{ex.StackTrace}";
                try
                {
                    File_Util.WriteTxtFileAndOpen(ioData.ErrorFilePath, mess);
                }
                catch
                {
                    // Xử lý để bắt lỗi trên máy người dùng, khi không thể viết file lỗi trên notepad
                    System.Windows.MessageBox.Show(mess, "Lỗi xảy ra!");
                }

                if (this.IsThrowWhenCatchException)
                {
                    throw;
                }
                else
                {
                    return Result.Succeeded;
                }
            }

            PostExecute();
            return Result.Succeeded;
        }

        protected virtual void PreExecute()
        {

        }

        protected virtual void PostExecute()
        {
            var isFormShow = formData.IsFormVisible && !formData.IsDialog;

            var hasExternalEvent = this.HasExternalEvent;
            if (!hasExternalEvent)
            {
                hasExternalEvent = isFormShow;
            }

            if (hasExternalEvent)
            {
                revitData.ExternalEvent = ExternalEvent.Create(revitData.ExternalEventHandler);
            }

            var isAutoDisposed = this.IsAutoDisposed;
            if (isAutoDisposed)
            {
                isAutoDisposed = !isFormShow;
            }

            if (isAutoDisposed)
            {
                RevitDataUtil.Dispose();
            }
        }

        public abstract void Execute();

        // Ribbon
        public virtual RibbonConfig? RibbonConfig { get; set; }

        public void CreateRibbon(bool? enabled = null)
        {
            var config = this.RibbonConfig!;
            var tab = EntTabUtil.Get(config.Tab!);
            var panel = tab.GetPanel(config.Panel!);

            panel.GetPushButton(new PushButtonConfig
            {
                id = config.id,
                Name = config.Name,
                CommandType = this.GetType(),
                IconName = config.IconPath,
                ToolTip = config.ToolTip,
                Enabled = enabled == null ? config.Enabled : enabled.Value,
                UseType = config.UseType
            }) ;

            tab.CreateTab();
        }
    }
}
