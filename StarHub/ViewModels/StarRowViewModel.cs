using Octokit;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StarHub.ViewModels
{
    public class StarRowViewModel : ReactiveObject
    {
        private string _Owner;
        public string Owner
        {
            get { return _Owner; }
            set { this.RaiseAndSetIfChanged(ref _Owner, value); }
        }

        private string _RepoName;
        public string RepoName
        {
            get { return _RepoName; }
            set { this.RaiseAndSetIfChanged(ref _RepoName, value); }
        }

        private string _Homepage;
        public string Homepage
        {
            get { return _Homepage; }
            set { this.RaiseAndSetIfChanged(ref _Homepage, value); }
        }

        public ReactiveCommand VisitHomepage;

        public StarRowViewModel(Repository starredRepo)
        {
            // Visit homepage command
            var hasHomepage = this.WhenAny(x => x.Homepage, x => !string.IsNullOrEmpty(x.Value));
            VisitHomepage = new ReactiveCommand(hasHomepage);

            // Initialize properties
            Owner = starredRepo.Owner.Login;
            RepoName = starredRepo.Name;
            Homepage = starredRepo.Homepage;

            // Launch the page in the browser
            VisitHomepage.Subscribe(_ =>
            {
                Process.Start(this.Homepage);
            });
        }
    }
}
