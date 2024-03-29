﻿using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Utility;

namespace Model.Entity
{
    public class EntPushButton
    {
        private RibbonData ribbonData => RibbonData.Instance;
        private IOData ioData => IOData.Instance;

        private string? _id;
        public string id
        {
            get => this._id ??= this.Name!;
            set => this._id = value;
        }

        public string? Name { get; set; }

        public EntPanel? EntPanel { get; set; }

        public UIControlledApplication Application => EntPanel!.Application;

        private string? text;
        public string? Text
        {
            get => text ??= Name;
            set=>text = value;
        }

        public string? ToolTip { get; set; }

        public string? IconName { get; set; }

        private string? iconPath;
        public string IconPath => iconPath ??= Path.Combine(ioData.IconDirectoryPath, IconName);

        private string? assemblyName;
        public string AssemblyName
        {
            get => this.assemblyName ??= ribbonData.AddinFilePath;
            set => this.assemblyName = value;
        }

        public string? CommandName { get; set; }

        private string? className;
        public string ClassName => className ??= CommandName!;

        private ImageSource? largeImage;
        public ImageSource? LargeImage => largeImage ??= this.GetLargeImage();

        private PushButton? pushButton;
        public PushButton PushButton => pushButton ??= this.GetPushButton();

        public Action? OnSetEnabled { get; set; }

        private bool? enabled;
        public bool Enabled
        {
            get => this.enabled ??= true;
            set
            {
                this.enabled = value;
                this.OnSetEnabled?.Invoke();
            }
        }

        private List<PushButton_UseType>? useTypes;
        public List<PushButton_UseType> UseTypes
        {
            get => this.useTypes ??= new List<PushButton_UseType> { PushButton_UseType.Common };
            set => this.useTypes = value;
        }
    }
}
