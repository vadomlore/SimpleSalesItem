using System;
using System.Collections.Generic;
namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            //var items = new List<SalesItem>(){
            //    new SalesItem("xxxx-1", 10),
            //    new SalesItem("xxxx-1", 10),
            //    new SalesItem("xxxx-3", 12),
            //    new SalesItem("xxxx-3", 13),
            //    new SalesItem("xxxx-4", 15),
            //};
            var command = new SalesManagerOperator();
            var smg = new SalesManager();
            Console.WriteLine("Command e.g add isbn:xxxx-34 price:12.5");
            Console.WriteLine("Command e.g sum isbn:xxxx-34");
            Console.WriteLine("Command e.g discount isbn:xxxx-34 discount:A");
            Console.WriteLine("Command e.g list");

            while (true)
            {
                try
                {
                    Console.WriteLine("Command:");
                    var line = Console.ReadLine();
                    var cmd = line.Split(new char[] { ' ' });
                    if (cmd.Length <= 0)
                    {
                        Console.WriteLine("Invalid operation");
                    }
                    var param = new Dictionary<string, object>();
                    for (int i = 1; i < cmd.Length; i++)
                    {
                        var kv = cmd[i].Split(new char[] { ':' });
                        param[kv[0]] = kv[1];
                    }
                    if(cmd[0] == "list")
                    {
                        command.Command(SalesManagerOperator.Operation.List, smg, param);
                    }
                    else if (cmd[0] == "add")
                    {
                        if (!IsValidParameter(new List<string>() { "isbn", "price" }, line))
                        {
                            throw new InvalidOperationException();
                        }
                        param["price"] = Double.Parse((string)param["price"]);
                        command.Command(SalesManagerOperator.Operation.Add, smg, param);
                        Console.WriteLine("Add done");
                    }

                    else if (cmd[0] == "sum")
                    {
                        if (!IsValidParameter(new List<string>() { "isbn" }, line))
                        {
                            throw new InvalidOperationException();
                        }
                        var result = command.Command(SalesManagerOperator.Operation.Sum, smg, param);
                        Console.WriteLine("sum result {0}", result);
                    }
                    else if (cmd[0] == "discount")
                    {
                        if (!IsValidParameter(new List<string>() { "isbn", "discount" }, line))
                        {
                            throw new InvalidOperationException();
                        }
                        command.Command(SalesManagerOperator.Operation.Discount, smg, param);
                        Console.WriteLine("Discount done");
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                catch(Exception e){
                    Console.WriteLine(e.ToString());
                }
            }
            //items.ForEach(item => salesManager.Add(item));
            //salesManager.SetCategory("xxxx-1", DiscountCategory.C);
            //Console.WriteLine(salesManager.SumPrice("xxxx-1"));
        }
        static bool IsValidParameter(List<string> param, string cmd)
        {
            if(param == null || param.Count <= 0)
            {
                return true;
            }
            foreach(var p in param)
            {
                if (!cmd.Contains(p + ":"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
