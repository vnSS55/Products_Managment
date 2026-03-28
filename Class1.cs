using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management
{
    // Defines the Product struct
    internal struct Product
    {
        // Declares the fields
        public string name { get; set; }
        
        public int id { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        // Constructor to initialize the fields
        public Product(string Name,int ID, double Price, int Quantity)
        {
            name = Name;
            id = ID;
            price = Price;
            quantity = Quantity;
        }

        // Method to calculate the total stock value of the product
        public double Total_Value()
        {
            return price * quantity;
        }
    }
}