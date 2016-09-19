namespace DnsShopChecker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Manager
    {
        public List<ProductEntry> GetProductEntries()
        {
            using (var context = new DnsShopCheckerContext())
            {
                return context.ProductEntries.ToList();
            }
        }

        public List<Product> Update(IEnumerable<Product> products)
        {
            products = products.DistinctBy(p => p.Id).ToList();
            using (var context = new DnsShopCheckerContext())
            {
                var productEntries = context.ProductEntries.ToList();
                var newProducts = products.Where(p => productEntries.All(e => e.Id != p.Id)).ToList();
                context.ProductEntries.AddRange(newProducts.Select(newProduct => new ProductEntry(newProduct.Id)));
                foreach (var productEntry in productEntries)
                {
                    if (products.All(p => p.Id != productEntry.Id))
                    {
                        productEntry.IsBought = true;
                    }
                }
                context.SaveChanges();
                return newProducts;
            }
        }
    }
}