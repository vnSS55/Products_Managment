using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Creates a list with "Product" as its type variable,
            // meaning each item in the list will be an object of the "Product" struct.
            // Makes it easier to add new products.
            var products = new List<Product>();

            // List of new products to add to the products list,
            // for easier comprehension and code readability.
            List<(string, int, double, int)> new_product = new List<(string, int, double, int)> {
                ("Tomato",102,  1.5, 300),
                ("Sponge",504,  2,   200),
                ("Chocolate",322, 1.5, 80)
            };

            Upd_products(products, new_product);

            int option;
            int count = 1;

            while (true)
            {
                // update
                Upd_products(products,new_product);

                // Start of the visual interface
                Console.WriteLine("PRODUCT MANAGEMENT - XPTO COMPANY\n");
                Console.WriteLine("NAME >>> PRICE >>> QUANTITY >>> STOCK VALUE\n");
                Console.WriteLine("---------------------------------------------");

                // Products table
                foreach (var item in products)
                {
                    Console.Write($"{count}º - {item.name} >>> {item.id}ID >>> {item.price} EUR >>> {item.quantity} Units >>> {item.Total_Value()} EUR\n");
                    Console.WriteLine("---------------------------------------------");
                    count++;
                }

                Console.WriteLine("\nOptions:");
                Console.WriteLine("1 - Add Product");
                Console.WriteLine("2 - Remove Product");
                Console.WriteLine("3 - Seach Products");
                Console.WriteLine("4 - Exit");
                Console.Write(">>");
                option = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (option)
                {
                    case 1:
                        Add_product(new_product);
                        break;
                    case 2:
                        Remove_product(products);
                        break;
                    case 3:
                        Search_product(products);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        Console.Clear();
                        break;
                }
                if (option == 4) break;
            }
        }
        static void Upd_products(List<Product> products, List<(string, int, double, int)> new_product)
        {
            // Checks if there are any new products to add.
            if (new_product.Count() != 0)
            {
                // Iterates through the new_product list and adds all present entries.
                // Variables are created for each field.
                foreach (var (name, id, price, quantity) in new_product)
                {
                    products.Add(new Product(name, id, price, quantity));
                }
                new_product.Clear();
            }
        }


        static void Add_product(List<(string, int, double, int)> new_product)
        {
            Console.Clear();

            int option;
            string nam;
            int idd;
            double pric;
            int quantit;

            while (true)
            {
                if (new_product.Count > 0)
                {
                    Console.WriteLine("Added products\n");
                    foreach (var item in new_product)
                    {
                        Console.Write($"{item.Item1} >>> {item.Item2}ID >>> {item.Item3} EUR >>> {item.Item4} Units \n");
                    }
                }
                else
                {
                    Console.WriteLine("Any products added yet...");
                }

                Console.WriteLine("1 - Exit \n2 - Continue");
                Console.Write(">>");
                option = int.Parse(Console.ReadLine());

                if (option == 1) break;
                // Modify to not get error's when the input is incorrect   ***************************************************************
                Console.WriteLine("\nEnter the name");
                nam = Console.ReadLine();

                Console.WriteLine("Enter the code of the product");
                idd = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the price");
                pric = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter the quantity");
                quantit = int.Parse(Console.ReadLine());

                new_product.Add((nam, idd, pric, quantit));

                Console.Clear();
            }
            Console.Clear();
        }

        static void Remove_product(List<Product> products)
        {
            int idd;
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"{products.Count} products | Product List\n");
                foreach (var item in products)
                {
                    Console.Write($"{item.name} >>> {item.id}ID >>> {item.price} EUR >>> {item.quantity} Units >>> {item.Total_Value()} EUR\n");
                    Console.WriteLine("---------------------------------------------");
                }

                Console.WriteLine("\nEnter the ID of the product to remove");
                idd = int.Parse(Console.ReadLine());

                // LINQ use for better reading
                // its if basically search the list products for a certain id
                // if found return true, else false
                // (name) . (method) ( (create a var) => (specify what to find) == (what to find))
                if (products.Any(items => items.id == idd))
                {
                    // same thing here, but i use both remove and LINQ write less
                    products.Remove(products.First(item => item.id == idd));
                    Console.WriteLine("Product removed...");
                }
                else
                {
                    Console.WriteLine("The ID was not found");
                }

                Console.WriteLine("Do you want to continue to remove ? (1 - Leave | 2 - Continue)");
                idd = Convert.ToInt32(Console.ReadLine());
                if (idd == 1) break;
            }
            Console.Clear();
        }

        static void Search_product(List<Product> products)
        {
            // obrigatory variables
            int idd = 0;
            double pric = 0 ;
            int quantit = 0;
            int option;

            // a IEnumerable variable because of the return type of LINQ Where
            // "= Enumerable.Empty<Product>()" its is the innitialization, needed because of the default that gives 
            // no value to filtered products
            IEnumerable<Product> filtered_products = Enumerable.Empty<Product>(); 

            while (true) 
            {
                Console.WriteLine("What do u want to filter out");
                Console.WriteLine("1 - price\n2 - stock amount\n3 - ID\n4 - Leave");
                Console.Write(">");
                option = Convert.ToInt32(Console.ReadLine());


                switch (option)
                {
                    case 1:
                        Console.WriteLine("\n1 - More than\n2 - Lower than");
                        Console.Write(">");
                        option = Convert.ToInt32(Console.ReadLine());
                        switch(option)
                        {
                            case 1:
                                Console.WriteLine("Write the value");
                                Console.Write(">");
                                pric = Convert.ToInt32(Console.ReadLine());
                                filtered_products = products.Where(items => items.price >= pric);
                                break;
                            case 2:
                            default:
                                Console.WriteLine("Write the value");
                                Console.Write(">");
                                pric = Convert.ToInt32(Console.ReadLine());
                                filtered_products = products.Where(items => items.price <= pric);
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("\n1 - More than\n2 - Lower than\n");
                        Console.Write(">");
                        option = Convert.ToInt32(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine("Write the quantity");
                                Console.Write(">");
                                quantit = Convert.ToInt32(Console.ReadLine());
                                filtered_products = products.Where(items => items.quantity >= quantit);
                                break;
                            case 2:
                                Console.WriteLine("Write the quantity");
                                Console.Write(">");
                                quantit = Convert.ToInt32(Console.ReadLine());
                                filtered_products = products.Where(items => items.quantity <= quantit);
                                break;
                            default:
                                Console.WriteLine("Choose between 1 and 2");
                                Console.Clear();
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Write the ID:");
                        Console.Write(">");
                        idd = Convert.ToInt32(Console.ReadLine());
                        // filters all the products with a specific id, return an IEnumerable
                        filtered_products = products.Where(items => items.id == idd);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid option...");
                        break;
                }

                if (filtered_products.Count() > 0)
                {
                    foreach (var item in filtered_products)
                    {
                        Console.Write($"{item.name} >>> {item.id}ID >>> {item.price} EUR >>> {item.quantity} Units >>> {item.Total_Value()} EUR\n");
                        Console.WriteLine("---------------------------------------------");
                    }
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                }

                
                Console.Clear();

                if (option == 4) break;
            }
            
        }
    }
}