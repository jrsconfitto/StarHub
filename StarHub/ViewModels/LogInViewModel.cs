using Octokit;
using ReactiveUI;
using System;

namespace StarHub.ViewModels
{
    public class LogInViewModel : ReactiveObject, IRoutableViewModel
    {
        // Routing stuff
        public IScreen HostScreen { get; protected set; }
        public string UrlPathSegment
        {
            get { return "login"; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        public ReactiveCommand Submit;

        public LogInViewModel(IScreen host)
        {
            this.HostScreen = host;

            // Only allow a submit when the user name is valid
            var validUserName = this.WhenAny(x => x.UserName, x => !string.IsNullOrWhiteSpace(x.Value));
            Submit = new ReactiveCommand(validUserName);
        }
    }
}
