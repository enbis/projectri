using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TriRanking.Core
{
    public class HtmlTableManager
    {
        public HtmlTableManager()
        {
        }

        public async Task<string>  GetHtmlPageString(string url)
        {
			HttpClient client = new HttpClient();
			var str = await client.GetStringAsync(url);
			return str;
        }
    }
}
