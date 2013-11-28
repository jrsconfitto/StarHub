using System;
using ReactiveUI;

namespace StarHub.ViewModels
{
    public class TwoFactorViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; protected set; }
        public string UrlPathSegment
        {
            get { return "twofactor"; }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { this.RaiseAndSetIfChanged(ref _Code, value); }
        }

        public ReactiveCommand Submit;

        public TwoFactorViewModel(IScreen host)
        {
            HostScreen = host;

            var codeHasBeenInput = this.WhenAny(x => x.Code, code => !string.IsNullOrWhiteSpace(code.Value));
            Submit = new ReactiveCommand(codeHasBeenInput);
        }
    }
}
