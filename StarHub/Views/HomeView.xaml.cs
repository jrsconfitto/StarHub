﻿using ReactiveUI;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeView : UserControl, IViewFor<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();

            // Bind UI
            this.Bind(ViewModel, vm => vm.UserName, view => view.UserName.Content);

            // Bind commands
            this.BindCommand(ViewModel, vm => vm.LogIn);
        }

        public HomeViewModel ViewModel {
            get { return (HomeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(HomeViewModel), typeof(HomeView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (HomeViewModel)value; }
        }
    }
}
