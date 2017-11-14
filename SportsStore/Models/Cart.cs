using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CartLine
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> collection = new List<CartLine>();

        public virtual void AddItem(Product p, int q)
        {
            CartLine line = collection
                .Where(x => x.Product.ID == p.ID)
                .FirstOrDefault();
            if(line==null)
            {
                collection.Add(new CartLine
                {
                    Product = p,
                    Quantity = q
                });
            }
            else
            {
                line.Quantity += q;
            }
        }

        public virtual void RemoveLine(Product p) => collection.RemoveAll(x => x.Product.ID == p.ID);

        public virtual decimal ComputeTotalValue() => collection.Sum(q => q.Product.Price * q.Quantity);

        public virtual void Clear() => collection.Clear();

        public virtual IEnumerable<CartLine> Lines => collection;


    }
}
