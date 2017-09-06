using System;
using System.Collections.Generic;
using Nostromo;
using ReactiveUI;
using TriRanking.Core;
using Xamarin.Forms;

namespace TriRanking
{
    [ViewFor(typeof(RankingAtletiViewModel))]
    public partial class RankingAtletiPage : ViewPage<RankingAtletiViewModel>
    {
        public RankingAtletiPage()
        {
            InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Appearing();
            this.WhenAnyValue(v => v.ricerca.Text).Subscribe(async (x) => await ViewModel.RefreshLine(x));
        }

    }
}
