using Akavache;
using Octokit;
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
using System.Reactive.Linq;
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
            LoadCredentials().Subscribe(creds =>
                {
                    Connection conn = new Connection(new ProductHeaderValue("StarHub"));
                    if (creds != null)
                    {
                        conn.Credentials = creds;
                    }
                    else
                    {
                        // For now, look for a token in the environment while i figure out how to get logins working
                        var authToken = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");
                        conn.Credentials = new Credentials(authToken);
                    }

                    GHClient = new GitHubClient(conn);

                    // Instantiate my view models
                    HomeVM = new HomeViewModel(this, GHClient);
                    Router.Navigate.Execute(HomeVM);
                });

            //LogInVM = new LogInViewModel(this, GHClient);

            //BlobCache.UserAccount.GetObjectAsync<string>("UserName").Subscribe(user =>
            //    {

            //    });

            // Make sure routing is set up
            //SetRoutingHome();
            //SetRoutingLogIn();
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
        }

        private void SetRoutingHome()
        {
            //// Route to the log in page if it's clicked
            //HomeVM.LogIn.Subscribe(_ =>
            //{
            //    Router.Navigate.Execute(LogInVM);
            //});
        }

        //private void SetRoutingLogIn()
        //{
        //    LogInVM.Submit.Subscribe(_ => Router.NavigateBack.Execute(null));
        //}

        public IObservable<Credentials> LoadCredentials()
        {
            return Observable.Zip(
                BlobCache.UserAccount.GetObjectAsync<string>("Username")
                .Catch(new string[] { "Friend" }.ToObservable()),
                BlobCache.UserAccount.GetObjectAsync<string>("Token")
                .Catch(new string[] { "Token" }.ToObservable()),
                (username, token) =>
                {
                    if (username != "Friend" && token != "Token")
                    {
                        return new Credentials(username, token);
                    }
                    return null;
                });
        }
    }
}
