using Akavache;
using Octokit;
using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private IStarredClient SClient;

        private string UnknownUser = "Friend";
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        public ReactiveList<StarRowViewModel> Stars;

        public ReactiveCommand LogIn;

        public HomeViewModel(IScreen host, IGitHubClient GHClient)
        {
            HostScreen = host;

            // Default to the normal (read: "tacky") user name
            if (GHClient.Connection.Credentials.Login == null)
            {
                UserName = UnknownUser;
            }

            //try
            //{
            //    var creds = GHClient.Connection.Credentials;
            //    Debug.Print("Current user: {0}", creds.Login.ToString());

            //    if (GHClient.Connection.Credentials.Login != null)
            //    {
            //        UserName = GHClient.Connection.Credentials.Login;
            //    }
            //    else
            //    {
            //        UserName = UnknownUser;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.Message);
            //    UserName = UnknownUser;
            //}

            var loggedIn = this.WhenAny(x => x.UserName, x => x.Value == UnknownUser);

            // Log in is necessary if the anonymous user text is the current user name
            LogIn = new ReactiveCommand(loggedIn);

            SClient = GHClient.Activity.Starring;
            var starredRepos = SClient.GetAllForCurrent().Result
                .Select(star => new StarRowViewModel(star)).ToArray();

            Stars = new ReactiveList<StarRowViewModel>(starredRepos);

            //todo: once i get this working we can use actual log ins
            //IObservable<User> user = BlobCache.UserAccount.GetOrCreateObject<User>("MyUser", () =>
            //{
            //    return null;
            //});

            //user.Subscribe(u =>
            // {
            //     if (u == null)
            //     {
            //         UserName = "Anonymous";
            //     }
            //     else
            //     {
            //         UserName = u.Login;
            //     }
            // });
        }
    }
}
