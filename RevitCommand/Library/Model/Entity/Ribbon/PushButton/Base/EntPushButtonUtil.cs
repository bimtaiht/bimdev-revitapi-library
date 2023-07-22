using Autodesk.Revit.UI;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utility
{
    public static class EntPushButtonUtil
    {
        public static EntPushButton GetPushButton(this EntPanel entPanel, PushButtonCofig config)
        {
            var name = config.Name;

            var entPushButtons = entPanel.EntPushButtons;
            var entPushButton = entPushButtons.SingleOrDefault(x => x.Name == name);
            if (entPushButton == null)
            {
                entPushButton = new EntPushButton
                {
                    EntPanel = entPanel,
                    Name = name,
                    CommandName = config.CommandName,
                    ToolTip = config.ToolTip,
                    IconName = config.IconName
                };
                entPushButtons.Add(entPushButton);
            }
            return entPushButton;
        }

        public static EntPushButton GetPushButton(this EntPanel entPanel, string name, string commandName, string iconName)
        {
            return GetPushButton(entPanel, new PushButtonCofig
            {
                Name = name,
                CommandName = commandName,
                IconName = iconName
            });
        }

        #region Property
        public static ImageSource? GetLargeImage(this EntPushButton entPushButton)
        {
            if (entPushButton.IconName == null)
            {
                return null;
            }

            var bitmapImage = new BitmapImage(new Uri(entPushButton.IconPath));
            return bitmapImage;
        }

        public static PushButton GetPushButton(this EntPushButton entPushButton)
        {
            var ribbonPanel = entPushButton.EntPanel!.RibbonPanel;

            var pbd = new PushButtonData(entPushButton.Name, entPushButton.Text, entPushButton.AssemblyName, entPushButton.ClassName);
            pbd.ToolTip = entPushButton.ToolTip;

            var pb = ribbonPanel.AddItem(pbd) as PushButton;
            pb!.LargeImage = entPushButton.LargeImage;

            return pb;
        }
        #endregion
    }
}
