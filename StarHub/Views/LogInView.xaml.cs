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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogInView : UserControl, IViewFor<LogInViewModel>
    {
        public LogInView()
        {
            InitializeComponent();

            // Bind the UserName to the label. i'll be shocked if this all works.
            this.Bind(ViewModel, vm => vm.UserName, view => view.UserName.Text);

            // Bind the Submit command to the button on the form
            this.BindCommand(ViewModel, vm => vm.Submit);
        }

        public LogInViewModel ViewModel
        {
            get { return (LogInViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LogInViewModel), typeof(LogInView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LogInViewModel)value; }
        }
    }
}
