namespace DnsShopChecker
{
    using System.Linq;
    using System.Text.RegularExpressions;

    using HtmlAgilityPack;

    public class Product
    {
        private const string domain = "http://www.dns-shop.ru";

        public string URL => $"{domain}/catalog/ucenennye_tovary/{Id}/";

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Img { get; set; }

        public string Descr()
        {
            return $"{Name}\n" +
                   $"{Description}\n" +
                   $"{URL}";
        }

        public override string ToString()
        {
            return $"{Name}\n" +
                   $"{Description}\n" +
                   $"{URL}";
        }

        public static Product Parse(HtmlNode node)
        {
            var descNode = node.SelectSingleNode(".//div[@class=\"item-desc\"]").ChildNodes.First();
            var href = node.SelectSingleNode(".//a[@class=\"ec-price-item-link\"]").Attributes["href"];
            var id = Regex.Match(href.Value, @"\/(\d+)\/$", RegexOptions.None).Groups[1].Value;
            return new Product
                   {
                       Name = node.SelectSingleNode(".//a[@class=\"ec-price-item-link\"]").InnerText,
                       Description = descNode.InnerText,
                       Id = int.Parse(id),
                       Img = node.SelectSingleNode(".//img").Attributes["src"].Value
                   };
        }
    }
}