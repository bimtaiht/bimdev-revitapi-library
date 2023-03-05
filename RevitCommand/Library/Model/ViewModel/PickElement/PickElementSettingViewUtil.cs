using Model.Entity;
using Model.ViewModel;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static class PickElementSettingViewUtil
    {
        private static RevitData revitData => RevitData.Instance;

        public static PickElementSettingView GetPickElementSettingView(this PickElementSetting pes)
        {
            var pesv = new PickElementSettingView
            {
                PickElementSetting = pes
            };

            pes.OnDisciplineTypeChanged = (v) =>
            {
                if (!pes.VM2M)
                {
                    pesv.UpdateFromModel(() => pesv.DisciplineType = v);
                }
            };

            pes.OnCountChanged = (v) =>
            {
                if (!pes.VM2M)
                {
                    pesv.UpdateFromModel(() => pesv.Count = v);
                }
            };

            return pesv;
        }

        //
        // Property
        //

        public static bool GetPickByEntireProject(this PickElementSettingView pickElementSettingView)
        {
            return pickElementSettingView.PickElementType == PickElementType.EntireProject;
        }

        public static bool GetPickByView(this PickElementSettingView pickElementSettingView)
        {
            return pickElementSettingView.PickElementType == PickElementType.EntireView;
        }

        public static bool GetPickElementsInProject(this PickElementSettingView pickElementSettingView)
        {
            return pickElementSettingView.PickElementType == PickElementType.PickElementsInProject;
        }

        public static Func<System.Windows.Visibility> GetVisibility_Render(this PickElementSettingView pickElementSettingView)
        {
            return () => Visibility.Visible;
        }

        public static void Refresh_Visibility(this PickElementSettingView q)
        {
            q.Visibility = q.Visibility_Render();
        }

        public static Func<System.Windows.Visibility> GetPickElementsInProject_Visibility_Render(this PickElementSettingView pickElementSettingView)
        {
            return () => Visibility.Visible;
        }

        public static void Refresh_PickElementsInProject_Visibility(this PickElementSettingView q)
        {
            q.PickElementsInProject_Visibility = q.PickElementsInProject_Visibility_Render();
        }

        public static System.Windows.Visibility GetPickElementsVisibility(this PickElementSettingView pickElementSettingView)
        {
            return pickElementSettingView.PickElementType == PickElementType.PickElementsInProject ?
                 System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        //
        // Method
        //

        public static void SetPickByEntireProject(this PickElementSettingView pickElementSettingView, bool value)
        {
            if (value)
            {
                pickElementSettingView.PickElementType = PickElementType.EntireProject;
                pickElementSettingView.PickElementsInProject = false;
                pickElementSettingView.PickByView = false;
            }
        }

        public static void SetPickByView(this PickElementSettingView pickElementSettingView, bool value)
        {
            if (value)
            {
                pickElementSettingView.PickElementType = PickElementType.EntireView;
                pickElementSettingView.PickElementsInProject = false;
                pickElementSettingView.PickByEntireProject = false;
            }
        }

        public static void SetPickElementsInProject(this PickElementSettingView pickElementSettingView, bool value)
        {
            if (value)
            {
                pickElementSettingView.PickElementType = PickElementType.PickElementsInProject;
                pickElementSettingView.PickByView = false;
                pickElementSettingView.PickByEntireProject = false;
            }
        }

        public static void PickElements(this PickElementSettingView pickElementSettingView)
        {
            Action action = () => pickElementSettingView.ActionWithButtonGroup(pickElementSettingView.PickElements_Func);
            pickElementSettingView.BaseFormData!.RaiseExternalEvent(action);
        }

        public static void PickElements_Func(this PickElementSettingView pickElementSettingView)
        {
            var pes = pickElementSettingView.PickElementSetting;
            var sel = revitData.Selection;

            try
            {
                pes.PickedElements = sel.PickElements();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
        }
    }
}
