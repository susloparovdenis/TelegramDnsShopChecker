namespace DnsShopChecker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    using HtmlAgilityPack;

    internal class DnsShopParser
    {
        public IEnumerable<Product> GetPhones(int page)
        {
            var htmlDocument = RetrieveHtmlDocument(page);
            return htmlDocument.DocumentNode.SelectNodes("//div[@class=\"object item-list ec-price-item\"]").Select<HtmlNode, Product>(Product.Parse);
        }


        public  IEnumerable<Product> GetPhones()
        {
            for (int i = 0; i < 10; i++)
            {
                IEnumerable<Product> phones;
                try
                {
                    phones = GetPhones(i).ToList();
                }
                catch (Exception e)
                {
                    yield break;
                }
                foreach (var phone in phones)
                {
                    yield return phone;
                }
            }
        }

        private  HtmlDocument RetrieveHtmlDocument(int page = 0)
        {
            Uri target = new Uri($"http://www.dns-shop.ru/catalog/ucenennye_tovary/?order=2&price_item_multigroup[]=117&p={page}");
            var request = (HttpWebRequest)WebRequest.Create(target);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("city_path", "novosibirsk") { Domain = target.Host });
            request.Method = "GET";
            var response = request.GetResponse();
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(response.GetResponseStream(), Encoding.UTF8);
            return htmlDocument;
        }
    }
}