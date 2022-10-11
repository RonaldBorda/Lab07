using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07
{
    class Program
    {
        public static DataClasses2DataContext context = new DataClasses2DataContext();
        static void Main(string[] args)
        {
            Grouping2Lambda();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] {0,1,2,3,4,5,6};

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach(int num in numQuery)
            {
                Console.WriteLine("{0,1}", num);
            }       
        }
        static void IntroToLINQLambda()
        {
            int[] ints = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var num = ints.Where(x => x % 2 == 0).ToList();
            foreach(var item in num)
            {
                Console.WriteLine(item);
            }
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        
        static void DataSourceLambda()
        {
            var dataCustomers = context.clientes.ToList();
            foreach(var item in dataCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes 
                                       where cust.Ciudad == "Londres" 
                                       select cust;
            
            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void FilteringLambda()
        {
            var dataCustomers = context.clientes
                .Where(x => x.Ciudad == "Londres")
                .Select(x => x).ToList();
            foreach(var item in dataCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "London"
                                        orderby cust.NombreCompañia ascending
                                        select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OrderingLambda()
        {
            var dataLondonCustomers = context.clientes
                .Where(x => x.Ciudad == "London")
                .OrderBy(x => x.NombreCompañia);
                
            foreach(var item in dataLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            foreach(var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (cliente customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);  
                }
            }
        }
        static void GroupingLambda()
        {
            var queryCostumersByCity = context.clientes.GroupBy(x => x.Ciudad);
            foreach (var customerGroup in queryCostumersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (cliente customer in customerGroup)
                {
                    Console.WriteLine("   {0} ", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Grouping2Lambda()
        {
            var dataGroup = context.clientes
                .GroupBy(x => x.Ciudad)
                .Where(xGroup => xGroup.Count() > 2)
                .OrderBy(xGroup => xGroup.Key);
            foreach (var item in dataGroup)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
