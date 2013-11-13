using Akavache;
using Octokit;
using Octokit.Reactive;
using ReactiveUI;
using StarHub.ViewModels;
using StarHub.Views;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace StarHub.ViewModels
{
    /* COOLSTUFF: What is the AppBootstrapper?
     * 
     * The AppBootstrapper is like a ViewModel for the WPF Application class.
     * Since Application isn't very testable (just like Window / UserControl), 
     * we want to create a class we can test. Since our application only has
     * one "screen" (i.e. a place we present Routed Views), we can also use 
     * this as our IScreen.
     * 
     * An IScreen is a ViewModel that contains a Router - practically speaking,
     * it usually represents a Window (or the RootFrame of a WinRT app). We 
     * should technically create a MainWindowViewModel to represent the IScreen,
     * but there isn't much benefit to split those up unless you've got multiple
     * windows.
     * 
     * AppBootstrapper is a good place to implement a lot of the "global 
     * variable" type things in your application. It's also the place where
     * you should configure your IoC container. And finally, it's the place 
     * which decides which View to Navigate to when the application starts.
     */
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public IRoutingState Router { get; private set; }

        // ViewModels in use
        private HomeViewModel HomeVM;
        //private LogInViewModel LogInVM;

        private IConnection Conn;
        private IGitHubClient GHClient;

        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, IRoutingState testRouter = null)
        {
            // Initialize the defaults from the constructor
            // (i think they are there for unit test purposes), even though they aren't used now.
            Router = testRouter ?? new RoutingState();
            dependencyResolver = dependencyResolver ?? RxApp.MutableResolver;

            // Declare the application name for Akavache
            BlobCache.ApplicationName = "StarHub";

            // Bind 
            RegisterParts(dependencyResolver);

            // TODO: This is a good place to set up any other app 
            // startup tasks, like setting the logging level
            LogHost.Default.Level = LogLevel.Debug;

            // Connect to GitHub
            ConnectToGitHub();

            // Instantiate my view models
            HomeVM = new HomeViewModel(this, GHClient);
            //LogInVM = new LogInViewModel(this, GHClient.User);

            //BlobCache.UserAccount.GetObjectAsync<string>("UserName").Subscribe(user =>
            //    {
                    
            //    });

            //// Make sure routing is set up
            //SetRoutingHome();
            //SetRoutingLogIn();

            // Let's start out at the home page
            Router.Navigate.Execute(HomeVM);
        }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));
            dependencyResolver.Register(() => new HomeView(), typeof(IViewFor<HomeViewModel>));
            dependencyResolver.Register(() => new StarRowView(), typeof(IViewFor<StarRowViewModel>));
            //dependencyResolver.Register(() => new LogInView(), typeof(IViewFor<LogInViewModel>));
        }

        private void ConnectToGitHub()
        {
            // Pull the auth token from the enironment
            var authToken = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");
            Conn = new Connection(new ProductHeaderValue("StarHub"))
            {
                Credentials = new Credentials(authToken)
            };

            // For now, do it here because i don't understand WPF, ReactiveUI, or Octokit! YAYZ!
            GHClient = new GitHubClient(Conn);
        }

        //private void SetRoutingHome()
        //{
        //    //// Route to the log in page if it's clicked
        //    //HomeVM.LogIn.Subscribe(_ =>
        //    //{
        //    //    Router.Navigate.Execute(LogInVM);
        //    //});
        //}

        //private void SetRoutingLogIn()
        //{
        //    LogInVM.Submit.Subscribe(_ => Router.NavigateBack.Execute(null));
        //}
    }
}
