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
        public static EntPushButton GetPushButton(this EntPanel entPanel, PushButtonConfig config)
        {
            var name = config.Name;

            var entPushButtons = entPanel.EntPushButtons;
            var entPushButton = entPushButtons.SingleOrDefault(x => x.Name == name);
            if (entPushButton == null)
            {
                entPushButton = new EntPushButton
                {
                    EntPanel = entPanel,
                    id = config.id,
                    Name = name,
                    AssemblyName = config.AssemblyName,
                    CommandName = config.CommandName,
                    ToolTip = config.ToolTip,
                    IconName = config.IconName,
                    Enabled = config.Enabled
                };

                if (config.UseType != null)
                {
                    entPushButton.UseTypes = new List<PushButton_UseType> { config.UseType.Value };
                }

                entPushButtons.Add(entPushButton);
            }

            return entPushButton;
        }

        public static EntPushButton GetPushButton(this EntPanel entPanel, string name, string commandName, string iconName)
        {
            return GetPushButton(entPanel, new PushButtonConfig
            {
                Name = name,
                CommandName = commandName,
                IconName = iconName
            });
        }

        // property
        public static ImageSource? GetLargeImage(this EntPushButton entPushButton)
        {
            if (entPushButton.IconName == null)
            {
                return null;
            }

            var bitmapImage = new BitmapImage(new Uri(entPushButton.IconPath));
            return bitmapImage;
        }

        public static PushButton GetPushButton(this EntPushButton q)
        {
            var ribbonPanel = q.EntPanel!.RibbonPanel;

            var pbd = new PushButtonData(q.Name, q.Text, q.AssemblyName, q.ClassName);
            pbd.ToolTip = q.ToolTip;

            var pb = (ribbonPanel.AddItem(pbd) as PushButton)!;
            pb.LargeImage = q.LargeImage;
            pb.Enabled = q.Enabled;

            q.OnSetEnabled = () => pb.Enabled = q.Enabled;

            return pb;
        }
    }
}
