using Model.ViewModel;
using SingleData;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Utility
{
    public static class NotifyClassUtil
    {
        #region Method

        public static void UpdateFromModel(this NotifyClass viewModel, Action action)
        {
            viewModel.M2VM = true;
            action();
            viewModel.M2VM = false;
        }

        public static void ActionWithButtonGroup(this NotifyClass viewModel, Action action)
        {
            var btnGroup = viewModel.BaseButtonGroup;
            btnGroup?.DisableAll();

            action();

            btnGroup?.EnableAll();
        }

        #endregion
    }
}