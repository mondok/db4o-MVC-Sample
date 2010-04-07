using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbShared
{
    public class ClothingType
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime DatePurchased { get; set; }

        public ClothingType()
        {
        }

        public override string ToString()
        {
            return string.Format("Color: {0}, Name: {1}, Description: {2}, Date Purchased: {3}, Id: {4}", this.Color, this.Name,
                                 this.Description, this.DatePurchased, this.Id);
        }
    }
}
