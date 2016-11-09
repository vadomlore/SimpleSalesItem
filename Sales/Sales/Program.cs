using System;
using System.Collections.Generic;
namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<SalesItem>(){
                new SalesItem("xxxx-1", 10),
                new SalesItem("xxxx-1", 10),
                new SalesItem("xxxx-3", 12),
                new SalesItem("xxxx-3", 13),
                new SalesItem("xxxx-4", 15),
            };
            var salesManager = new SalesManager();
            items.ForEach(item => salesManager.Add(item));
            salesManager.SetCategory("xxxx-1", DiscountCategory.C);
            Console.WriteLine(salesManager.SumPrice("xxxx-1"));
        }
    }
}
