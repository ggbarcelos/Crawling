using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace WebCrawling
{
    class Program
    {
        static void Main(string[] args)
        {
            startCrawlerasync();
            Console.ReadLine();
        }

        private static async Task startCrawlerasync()
        {
            Startup startup;

            for (int i=1; i<200;i++)
            {
                var url = $"https://comunidade.startse.com/perfil/membro/instituicoes/14/{i}";
                //var url = "https://comunidade.startse.com/startups";
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var divs =
                    htmlDocument.DocumentNode.Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "").Equals("bg-white borders mb-20 pf-15 text-center")).ToList();
                foreach (var div in divs)
                {
                    startup = new Startup()
                    {
                        UrlImg = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value,
                        Nome = div.Descendants("h5").FirstOrDefault().InnerText,
                        Tipo = div.Descendants("p").FirstOrDefault().InnerText,

                    };
                }
            }
        }
    }
}
