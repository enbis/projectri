using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Nostromo;
using Nostromo.Navigation;
using ReactiveUI;

namespace TriRanking.Core
{
    public class HomeViewModel : BaseViewModel
    {

        public List<string> PickerAnno;
        public List<string> PickerSesso;
        public List<string> PickerTipoGara;
        public List<string> PickerCategoria;

		public ICommand Procedi { get; set; }

		//int selectedAnno;
		//public int SelectedAnno
		//{
		//	get { return selectedAnno; }
		//	set
		//	{
		//		this.RaiseAndSetIfChanged(ref selectedAnno, value);
		//	}
		//}

		//int selectedSesso;
		//public int SelectedSesso
		//{
		//	get { return selectedSesso; }
		//	set
		//	{
		//		this.RaiseAndSetIfChanged(ref selectedSesso, value);
		//	}
		//}

		//int selectedTipoGara;
		//public int SelectedTipoGara
		//{
		//	get { return selectedTipoGara; }
		//	set
		//	{
		//		this.RaiseAndSetIfChanged(ref selectedTipoGara, value);
		//	}
		//}

        string selectedItemAnno;
		public string SelectedItemAnno
		{
			get { return selectedItemAnno; }
			set
			{
				this.RaiseAndSetIfChanged(ref selectedItemAnno, value);
			}
		}

		string selectedItemSesso;
		public string SelectedItemSesso
		{
			get { return selectedItemSesso; }
			set
			{
				this.RaiseAndSetIfChanged(ref selectedItemSesso, value);
			}
		}

		string selectedItemTipoGara;
		public string SelectedItemTipoGara
		{
			get { return selectedItemTipoGara; }
			set
			{
				this.RaiseAndSetIfChanged(ref selectedItemTipoGara, value);
			}
		}

		string selectedItemCategoria;
		public string SelectedItemCategoria
		{
			get { return selectedItemCategoria; }
			set
			{
				this.RaiseAndSetIfChanged(ref selectedItemCategoria, value);
			}
		}

		public HomeViewModel()
        {
            Procedi = ReactiveCommand.CreateFromTask((arg) => ProcediRicerca());
        }

        async Task ProcediRicerca()
        {
            if ( CheckFields())
            {
                await Navigator.PushAsync(Build.ViewModel(() => new RankingAtletiViewModel(SelectedItemAnno, SelectedItemSesso, SelectedItemTipoGara, selectedItemCategoria)), true);
			}
            else
            {
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("Selezionare tutti i campi per procedre", "ATTENZIONE", "Ok");
            }
        }

        private bool CheckFields()
        {
            return ((SelectedItemAnno != "-") && (SelectedItemSesso != "-") && (SelectedItemTipoGara != "-") && (SelectedItemCategoria != "-"));
        }

        public override void Load()
        {
            base.Load();

            //         PickerAnno = Enumerable.Range(0, 4).Select(i => DateTime.Now.AddYears(i - 4)).OrderByDescending(x => x).Select(date => date.ToString("yyyy")).ToList();
            //PickerAnno.Insert(0, "-");

            PickerAnno = new List<string>() {"-", "2017", "2016", "2015", "2014" };
			PickerSesso = new List<string>() {"-", "Maschile", "Femminile" };
            PickerTipoGara = new List<string>() {"-", "Sprint", "Olimpico" };
            PickerCategoria = new List<string>() { "-", "YB", "JU", "S1", "S2", "S3", "S4", "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8" };
        }

        public async override Task Appearing()
        {
            //SelectedAnno = 0;
            //SelectedSesso = 0;
            //SelectedTipoGara = 0;
            base.Appearing();
        }

    }
}
