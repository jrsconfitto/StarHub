using ReactiveUI;
using StarHub.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarHub.ViewModels
{
    public class HomeViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; protected set; }
        public string UrlPathSegment
        {
            get { return "home"; }
        }

        private const string AnonymousUser = "not logged in!";
        private string _UserName = AnonymousUser;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        public ReactiveCommand LogIn;

        public HomeViewModel(IScreen host)
        {
            HostScreen = host;

            // Log in is necessary if the anonymous user text is the current user name
            var logInNecessary = this.WhenAny(x => x.UserName, x => x.Value == AnonymousUser);
            LogIn = new ReactiveCommand(logInNecessary);
        }
    }
}
