namespace DnsShopChecker
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;

    using Taikandi.Telebot;
    using Taikandi.Telebot.Types;

    public class Bot
    {
        private static int myId = 24130257;

        private readonly DnsShopParser dnsShopParser = new DnsShopParser();

        private readonly Manager manager = new Manager();

        private readonly Telebot telebot = new Telebot("117677324:AAEM7QAvrgLSxyXekwKRrQuxx-Qc2krAec4");

        public void Do()
        {
            var products = dnsShopParser.GetPhones().ToList();
            var newProducts = manager.Update(products);
            foreach (var newProduct in newProducts)
            {
                SendMessage(newProduct);
            }
        }


        void Start()
        {
            var lines = File.ReadAllLines("filename");
            foreach (var line in lines)
            {
                Debug.Log(line);
            }
        }

        private void SendMessage(Product product)
        {
            var result = telebot.SendMessageAsync(myId, product.ToString()).Result;
            var imgStream = WebRequest.Create(product.Img).GetResponse().GetResponseStream();
            var telegramResult = telebot.SendPhotoAsync(myId, imgStream, "0").Result;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            new Bot().Do();
        }
    }
}