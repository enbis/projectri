using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nostromo;
using Nostromo.Interfaces;
using TriRanking.Core;
using Xamarin.Forms;

namespace TriRanking
{
    public partial class App : Application, IPageNavigator
    {
        NavigationPage mainPage;

        public App()
        {
            InitializeComponent();

            var initializer = NostromoApp
                .Setup()
                .UseAutofacContainer()
                .UseGenericPlatformViewActivator()
                .RegisterViewsInAssemblyOf<App>()
                .RegisterDependenciesInAssemblyOf<App>();

            initializer.Init();

			//var vm = Build.ViewModel(() => new RankingAtletiViewModel());
            var vm = Build.ViewModel(() => new HomeViewModel());

			var firstView = Build.View<Page>(vm);
			firstView.BindingContext = vm;
			vm.Navigator = this;
			ReplaceMainPage(firstView);
        }

		void ReplaceMainPage(Page page)
		{
			if (mainPage != null)
				mainPage = null;

			mainPage = new NavigationPage(page)
			{
				BarBackgroundColor = Xamarin.Forms.Color.FromHex("3F3F3F"),
				BarTextColor = Xamarin.Forms.Color.White
			};


			MainPage = mainPage;
		}

        public Task BackAsync(NavigatorSettings settings)
        {
            throw new NotImplementedException();
        }

        public async Task NavigateToAsync(IPageViewModel viewmodel, NavigatorSettings settings)
        {
			Device.BeginInvokeOnMainThread(async () =>
			{
				viewmodel.Navigator = this;

				var view = Build.View<Page>(viewmodel);
				view.BindingContext = viewmodel;

				if (settings.IsModal)
				{
					await mainPage.Navigation.PushModalAsync(view, settings.UsePlatformAnimation);
				}
				else
				{
					await mainPage.PushAsync(view, settings.UsePlatformAnimation);
				}
			});
        }
    }
}
