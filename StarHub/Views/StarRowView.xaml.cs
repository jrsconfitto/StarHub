using StarHub.ViewModels;
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
    /// View bindings, yo!
    /// </summary>
    public partial class StarRowView : UserControl, IViewFor<StarRowViewModel>
    {
        public StarRowView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.Owner, view => view.Owner.Content);
            this.OneWayBind(ViewModel, vm => vm.RepoName, view => view.RepoName.Content);
            this.BindCommand(ViewModel, vm => vm.VisitHomepage);
        }

        public StarRowViewModel ViewModel {
            get { return (StarRowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(StarRowViewModel), typeof(StarRowView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (StarRowViewModel)value; }
        }
    }
}
