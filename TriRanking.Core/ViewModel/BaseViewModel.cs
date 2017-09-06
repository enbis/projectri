using System;
using System.Threading.Tasks;
using Nostromo.Interfaces;
using ReactiveUI;

namespace TriRanking.Core
{
    public class BaseViewModel : ReactiveObject, IPageViewModel
    {
        public string Title { get; set; }
        public IPageNavigator Navigator { get; set; }

        public virtual async Task Appearing()
        {
        }

        public async Task Disappearing()
        {
        }

        public virtual async void Load()
        {
        }

        bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.RaiseAndSetIfChanged(ref isBusy, value);}
        }
    }
}
