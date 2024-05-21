using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;

namespace Utility
{
    public static class FormUtil
    {
        private static FormData formData => FormData.Instance;
        private static IOData ioData => IOData.Instance;

        private static void DisposeData()
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
                // ignore
            }

            RevitDataUtil.Dispose();
        }

        private static void OnFormClosed(object sender, EventArgs e)
        {
            DisposeData();
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
                    form.Closed += OnFormClosed;
                }

                form.Show();
            }
        }

        public static void Do(this System.Windows.Window form, Action action)
        {
            if (formData.IsDialog)
            {
                form.Hide();

                action();

                form.ShowDialog();
            }
            else
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (formData.IsPublish)
                    {
                        MessageBox.Show("Lỗi xảy ra", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var mess = $"{ex.GetType().Name}\n{ex.Message}\n{ex.StackTrace}";
                        try
                        {
                            File_Util.WriteTxtFileAndOpen(ioData.ErrorFilePath, mess);
                        }
                        catch
                        {
                            // ignore
                        }
                    }

                    form.Close();
                }
            }
        }
    }
}
