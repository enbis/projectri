using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MvvmHelpers;
using Nostromo.Interfaces;
using ReactiveUI;
using TriRanking.Models;

namespace TriRanking.Core
{
    public class RankingAtletiViewModel : BaseViewModel
    {
        //const string url = "http://risultati.fitri.it/Rank.asp?Pag=1&Anno=2017&Cod_Soc=&TRank=S&Ss=M&Cat=S3";
        //const string url2 = "http://risultati.fitri.it/Rank.asp?Pag=1&Anno=2017&Cod_Soc=&TRank=S&Ss=M&PunDal=0.00&PunAl=999.99"; //sprint
        //const string url3 = "http://risultati.fitri.it/Rank.asp?Pag=1&Anno=2017&Cod_Soc=&TRank=T&Ss=M&PunDal=0.00&PunAl=999.99";

        string url;
        const string risultatiFitri = "http://risultati.fitri.it/Rank.asp?Pag=1&Anno=";

        List<string> listMatches;
        public ObservableRangeCollection<RankingDataModel> ListAtleti { get; set; }
        IList<RankingDataModel> listBkp { get; set; }

        public RankingAtletiViewModel()
        {
            ListAtleti = new ObservableRangeCollection<RankingDataModel>();
        }

        string ricerca;
        public string Ricerca
        {
            get { return ricerca; }
            set
            {
                this.RaiseAndSetIfChanged(ref ricerca, value);
            }
        }

        public async Task RefreshLine(string ric)
        {
            if (listBkp != null)
            {
                var listFiltered = listBkp.Where(x => x.Atleta.Contains(ric.ToUpper())).ToList().OrderBy(x => x.PosizioneClassifica);
				ListAtleti.ReplaceRange(listFiltered);
            }
        }

        public RankingAtletiViewModel(string anno, string sesso, string tipologia, string categoria)
        {
            ListAtleti = new ObservableRangeCollection<RankingDataModel>();
            string _tipologia = string.Equals(tipologia, "Olimpico") ? "T&Ss=" : "S&Ss=";
            string _sesso = string.Equals(sesso, "Maschile") ? "M" : "F";

            url = $"{risultatiFitri}{anno}&Cod_Soc=&TRank={_tipologia}{_sesso}&Cat={categoria}";
        }

        public async override void Load()
        {
            base.Load();
        }

        string _tableString;
        public async override Task Appearing()
        {
            base.Appearing();

            var htmlTabManager = new HtmlTableManager();
            _tableString = await htmlTabManager.GetHtmlPageString(url);
            await TransalteHtmlString(_tableString);
        }

        Regex regexSimple = new Regex("(>[0-9 A-Z,.]+<)");
        async Task TransalteHtmlString(string html)
        {
			IsBusy = true;
            //var load = Acr.UserDialogs.UserDialogs.Instance.Loading("RicercaTitle");
            //await Task.Delay(100);
            await Task.Run(() =>
             {

                 using (TextReader sr = new StringReader(html))
                 {
                     HtmlDocument doc = new HtmlDocument();
                     doc.Load(sr);
                     var someNode = (from node in doc.DocumentNode.Descendants() where node.Name == "tr" select (node));

                     foreach (var node in someNode)
                     {
                         if (node.InnerHtml.Contains("RiRank.asp?"))
                         {

                             var stringArray = node.InnerHtml.Split(new string[] { "</td>" }, StringSplitOptions.RemoveEmptyEntries);

                             if (stringArray.Count() == 13)
                             {

                                 if (listMatches == null)
                                     listMatches = new List<string>();
                                 listMatches.Clear();

                                 for (int x = 0; x < 13; x++)
                                 {
                                     var testString = stringArray[x].Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty);

                                     foreach (Match match in regexSimple.Matches(testString))
                                     {
                                         var regexValue = match.Groups[1].Value;
                                         var replacedValue = regexValue.Replace('<', ' ').Replace('>', ' ').Trim();
                                         if (!String.IsNullOrEmpty(replacedValue))
                                         {
                                             System.Diagnostics.Debug.WriteLine(replacedValue);
                                             listMatches.Add(replacedValue);
                                             if (listMatches.Count() == 7)
                                             {
                                                 ListAtleti.Add(new RankingDataModel()
                                                 {
                                                     PosizioneClassifica = Convert.ToInt16(listMatches[0]),
                                                     Atleta = listMatches[1],
                                                     AnnoNascita = Convert.ToInt16(listMatches[0]),
                                                     Categoria = listMatches[3],
                                                     Societa = listMatches[4],
                                                     ValueRank = listMatches[5],
                                                     CodSquadra = Convert.ToInt16(listMatches[6])
                                                 });
                                                 break;
                                             }
                                         }
                                     }

                                 }
                             }
                         }

                     }
                     listBkp = ListAtleti.ToList();
                 }

                //load.Dispose();
                IsBusy = false;
             });
        }
    }
}
