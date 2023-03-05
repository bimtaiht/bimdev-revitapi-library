using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public static class ButtonGroupUtil
    {
        #region Method

        public static void DisableAll(this ButtonGroup buttonGroup)
        {
            buttonGroup.HandleButtons?.Invoke(false);
        }

        public static void EnableAll(this ButtonGroup buttonGroup)
        {
            buttonGroup.HandleButtons?.Invoke(true);
        }

        #endregion
    }
}
