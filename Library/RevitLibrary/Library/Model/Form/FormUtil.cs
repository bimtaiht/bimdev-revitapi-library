using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Utility
{
    public static class FormUtil
    {
        private static FormData formData => FormData.Instance;

        private static void Dispose_onFormClosed(object sender, EventArgs e)
        {
            try
            {
                var data = formData.Form!.DataContext;
                var dataType = data.GetType();
                var instanceField = dataType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                instanceField.SetValue(null, null);
            }
            catch
            {

            }

            RevitDataUtil.Dispose();
        }

        public static void Show(this System.Windows.Window form, bool isDialog)
        {
            formData.Form = form;
            formData.IsFormVisible = true;
            formData.IsDialog = isDialog;

            if (isDialog)
            {
                form.ShowDialog();
            }
            else
            {
                if (!formData.IsDisposeOnClosed)
                {
                    formData.IsDisposeOnClosed = true;
                    form.Closed += Dispose_onFormClosed;
                }

                form.Show();
            }
        }

        public static void Do(this System.Windows.Window form, Action action, bool isHaveTransaction = false)
        {
            if (formData.IsDialog)
            {
                form.Hide();

                action();

                form.ShowDialog();
            }
            else
            {
                action();
            }
        }
    }
}
