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
        private IUsersClient UsersClient;

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        public ReactiveCommand Submit;

        public LogInViewModel(IScreen host, IUsersClient usersClient)
        {
            this.HostScreen = host;
            this.UsersClient = usersClient;

            // Only allow a submit when the user name is valid
            var validUserName = this.WhenAny(x => x.UserName, x => !string.IsNullOrWhiteSpace(x.Value));
            Submit = new ReactiveCommand(validUserName);

            //todo: better make a cancel or else you'll get very mad!
            Submit.Subscribe(_ =>
            {
                try
                {
                    // See if the user exists
                    var user = this.UsersClient.Get(UserName).Result;

                    if (user != null)
                    {
                        // Insert the user into the cache
                        BlobCache.UserAccount.InsertObject<User>("MyUser", user);
                    }
                }
                catch (AuthorizationException authEx)
                {
                    Debug.Print("CRAP!");
                    Debug.Print(authEx.Message);
                    Debug.Print(authEx.StackTrace);
                }
            });

            MessageBus.Current.RegisterMessageSource(Submit);
        }
    }
}
