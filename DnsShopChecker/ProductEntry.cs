namespace DnsShopChecker
{
    using System;

    public class ProductEntry
    {
        public int Id { get; private set; }

        public DateTime Created { get; set; }

        public bool IsBought { get; set; }

        public ProductEntry(int id)
        {
            Id = id;
            Created = DateTime.Now;
        }

        public ProductEntry()
        {
        }
    }
}