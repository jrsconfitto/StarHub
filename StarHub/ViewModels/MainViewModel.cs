using Akavache;
using Octokit;
using Octokit.Reactive;
using ReactiveUI;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StarHub.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private GitHubClient ghClient;

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { this.RaiseAndSetIfChanged(ref _UserName, value); }
        }

        public MainViewModel()
        {
            // Pull the auth token from the enironment
            var authToken = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");
            IConnection conn = new Connection(new ProductHeaderValue("StarHub"))
            {
                Credentials = new Credentials(authToken)
            };

            // For now, do it here because i don't understand WPF, ReactiveUI, or Octokit! YAYZ!
            ghClient = new GitHubClient(conn);

            //ApiConnection apiConn = new ApiConnection(conn);
            UserName = "jugglingnutcase";
        }
    }
}
