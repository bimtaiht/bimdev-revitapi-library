using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Model.UserControl
{
    /// <summary>
    /// Interaction logic for PM_ProjectUC.xaml
    /// </summary>
    public partial class ComboBoxUC : System.Windows.Controls.UserControl
    {
        public ComboBoxUC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(ComboBoxUC), new FrameworkPropertyMetadata());

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ComboBoxUC), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboBoxUC), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(ComboBoxUC), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public object Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
          DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(ComboBoxUC), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public object DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public static readonly DependencyProperty AddItemProperty =
            DependencyProperty.Register("AddItem",typeof(ICommand),typeof(ComboBoxUC),new UIPropertyMetadata(null));

        public ICommand AddItem
        {
            get { return (ICommand)GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); }
        }

        public static readonly DependencyProperty DeleteItemProperty =
            DependencyProperty.Register("DeleteItem", typeof(ICommand), typeof(ComboBoxUC), new UIPropertyMetadata(null));

        public ICommand DeleteItem
        {
            get { return (ICommand)GetValue(DeleteItemProperty); }
            set { SetValue(DeleteItemProperty, value); }
        }

        public static readonly DependencyProperty AddEnableProperty =
           DependencyProperty.Register("AddEnable", typeof(bool), typeof(ComboBoxUC),
               new FrameworkPropertyMetadata(true) { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public bool AddEnable
        {
            get => (bool)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty AddVisibilityProperty =
            DependencyProperty.Register("AddVisibility", typeof(Visibility), typeof(ComboBoxUC), 
                new FrameworkPropertyMetadata(Visibility.Collapsed) { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public Visibility AddVisibility
        {
            get => (Visibility)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty DeleteEnableProperty =
           DependencyProperty.Register("DeleteEnable", typeof(bool), typeof(ComboBoxUC),
               new FrameworkPropertyMetadata(true) { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public bool DeleteEnable
        {
            get => (bool)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty DeleteVisibilityProperty =
            DependencyProperty.Register("DeleteVisibility", typeof(Visibility), typeof(ComboBoxUC), 
                new FrameworkPropertyMetadata(Visibility.Visible) { BindsTwoWayByDefault = true });

        [Browsable(true)]
        public Visibility DeleteVisibility
        {
            get => (Visibility)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
    }
}
