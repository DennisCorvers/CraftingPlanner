using System;
using System.Collections.Generic;
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

namespace CraftingPlanner.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for TabNavigator.xaml
    /// </summary>
    public partial class TabNavigator : UserControl
    {
        public static readonly DependencyProperty CanNavigateProperty;
        public static readonly DependencyProperty HeaderTextProperty;
        public static readonly DependencyProperty NavigateNextProperty;
        public static readonly DependencyProperty NavigatePreviousProperty;

        static TabNavigator()
        {
            CanNavigateProperty = DependencyProperty.Register(nameof(CanNavigate), typeof(bool), typeof(TabNavigator),
                new PropertyMetadata(false, OnCanNavigateValueChanged));
            HeaderTextProperty = DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(TabNavigator),
                new PropertyMetadata(string.Empty, OnHeaderTextValueChanged));
            NavigateNextProperty = DependencyProperty.Register(nameof(NavigateNext), typeof(ICommand), typeof(TabNavigator));
            NavigatePreviousProperty = DependencyProperty.Register(nameof(NavigatePrevious), typeof(ICommand), typeof(TabNavigator));
        }

        public bool CanNavigate
        {
            get { return (bool)GetValue(CanNavigateProperty); }
            set { SetValue(CanNavigateProperty, value); }
        }

        public string? HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public ICommand? NavigateNext
        {
            get { return (ICommand)GetValue(NavigateNextProperty); }
            set { SetValue(NavigateNextProperty, value); }
        }

        public ICommand? NavigatePrevious
        {
            get { return (ICommand)GetValue(NavigatePreviousProperty); }
            set { SetValue(NavigatePreviousProperty, value); }
        }

        public TabNavigator()
        {
            InitializeComponent();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            if (NavigatePrevious != null && NavigatePrevious.CanExecute(null))
            {
                NavigatePrevious.Execute(null);
            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (NavigateNext != null && NavigateNext.CanExecute(null))
            {
                NavigateNext.Execute(null);
            }
        }

        private void ToggleCanNavigate(bool state)
        {
            ButtonNavigateNext.IsEnabled = state;
            ButtonNavigatePrevious.IsEnabled = state;
        }

        private void UpdateHeaderText(string? value)
        {
            TextHeader.Text = value;
            TextHeaderShadow.Text = value;
        }

        private static void OnCanNavigateValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabNavigator @this)
            {
                @this.ToggleCanNavigate((bool)e.NewValue);
            }
        }

        private static void OnHeaderTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is TabNavigator @this)
            {
                @this.UpdateHeaderText(e.NewValue as string);
            }
        }
    }
}
