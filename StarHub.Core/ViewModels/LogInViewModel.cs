using Akavache;
using Octokit;
using ReactiveUI;
using System;
using System.Diagnostics;

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

        // Client
        private IGitHubClient GHClient;

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { this.RaiseAndSetIfChanged(ref _Password, value); }
        }

        public ReactiveCommand Submit;

        public LogInViewModel(IScreen host, IGitHubClient ghClient)
        {
            this.HostScreen = host;
            this.GHClient = ghClient;

            // Only allow a submit when the user name is valid
            var validStuffTyped = this.WhenAny(x => x.UserName, x => x.Password,
                (user, pass) => !string.IsNullOrWhiteSpace(user.Value) && !string.IsNullOrWhiteSpace(pass.Value));

            Submit = new ReactiveCommand(validStuffTyped);

            //todo: better make a cancel or else you'll get very mad!
            //Submit.Subscribe(_ =>
            //{
            //    GHClient.Authorization.Create();
            //    //try
            //    //{
            //    //    //if (user != null)
            //    //    //{
            //    //    // Insert the user into the cache
            //    //    BlobCache.UserAccount.InsertObject<User>("MyUser", user);
            //    //    //}
            //    //}
            //    //catch (AuthorizationException authEx)
            //    //{
            //    //    Debug.Print("CRAP!");
            //    //    Debug.Print(authEx.Message);
            //    //    Debug.Print(authEx.StackTrace);
            //    //}
            //});

            MessageBus.Current.RegisterMessageSource(Submit);
        }
    }
}
