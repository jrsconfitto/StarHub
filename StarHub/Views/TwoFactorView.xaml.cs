using ReactiveUI;
using StarHub.ViewModels;
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
    /// Interaction logic for TwoFactorView.xaml
    /// </summary>
    public partial class TwoFactorView : UserControl, IViewFor<TwoFactorViewModel>
    {
        public TwoFactorView()
        {
            InitializeComponent();

            this.Bind(ViewModel, vm => vm.Code, view => view.TwoFactorCode.Text);
            this.BindCommand(ViewModel, vm => vm.Submit);
        }

        public TwoFactorViewModel ViewModel
        {
            get { return (TwoFactorViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(TwoFactorViewModel), typeof(TwoFactorView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TwoFactorViewModel)value; }
        }
    }
}
