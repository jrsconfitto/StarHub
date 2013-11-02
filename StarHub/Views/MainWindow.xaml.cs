using ReactiveUI;

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

namespace StarHub.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //, IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            //// Set the ViewModel
            //ViewModel = new MainViewModel();

            //this.OneWayBind(ViewModel, x => x.UserName);
        }

        //// The ViewModel itself must be a property that the window can bind against
        //public MainViewModel ViewModel
        //{
        //    get { return (MainViewModel)GetValue(ViewModelProperty); }
        //    set { SetValue(ViewModelProperty, value); }
        //}
        //public static readonly DependencyProperty ViewModelProperty =
        //   DependencyProperty.Register("ViewModel", typeof(MainViewModel), typeof(MainWindow));

        //object IViewFor.ViewModel
        //{
        //    get { return ViewModel; }
        //    set { ViewModel = (MainViewModel)value; }
        //}
    }

}