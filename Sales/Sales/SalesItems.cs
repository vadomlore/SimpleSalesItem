using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales
{
    public enum DiscountCategory
    {
        A,
        B,
        C
    }


    public class SalesItem
    {
        public string ISBN { get; set; }
        public double Price {
            get; set;
        }

        public SalesItem(string isbn, double price)
        {
            this.ISBN = isbn;
            this.Price = price;
        }

        public override string ToString()
        {
            return string.Format("isbn:{0}, price{1}", this.ISBN, this.Price);
        }
    }

    public class SalesManager
    {
        List<SalesItem> salesItems = new List<SalesItem>();
        private Dictionary<string, DiscountCategory> discountCategories = new Dictionary<string, DiscountCategory>();

        public void Add(SalesItem salesItem)
        {
            salesItems.Add(salesItem);
        }

        /// <summary>
        /// set category for same isbn product
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="category"></param>
        public void SetCategory(string isbn, DiscountCategory category)
        {
            discountCategories[isbn] = category;
        }

        public double SumPrice(string isbn)
        {
            var price = salesItems.Where(i => i.ISBN == isbn).Sum(x => { return x.Price; });
            return discountCategories.ContainsKey(isbn) ? price * DiscountCategoryToRate(discountCategories[isbn]) : price;
        }

        public void Print()
        {
            foreach(var salesItem in salesItems)
            {
                Console.WriteLine(salesItem);
            }
        }

        public static double DiscountCategoryToRate(DiscountCategory category)
        {
            switch (Enum.GetName(typeof(DiscountCategory), category))
            {
                case "A": return 1;
                case "B": return 0.9;
                case "C": return 0.7;
                default: return 1;
            }
        }
    }

    public class SalesManagerOperator
    {
        public enum Operation
        {
            Add, //add item to salesmanager
            Sum, // sum the item price,
            Discount, //discount the salesitem,
            List
        }

        public object Command(Operation op, SalesManager smg, Dictionary<string, object> param)
        {
            switch (op)
            {
                case Operation.Add:
                    smg.Add(new SalesItem((string)param["isbn"], (double)param["price"]));
                    return null;
                case Operation.Sum:
                    return smg.SumPrice((string)param["isbn"]);
                case Operation.Discount:
                    smg.SetCategory((string)param["isbn"], (DiscountCategory)Enum.Parse(typeof(DiscountCategory), (string)param["discount"]));
                    return null;
                case Operation.List:
                    smg.Print();
                    return null;
                default:
                    throw new InvalidOperationException("Invalid Operation");
            }
        }
    }
}
