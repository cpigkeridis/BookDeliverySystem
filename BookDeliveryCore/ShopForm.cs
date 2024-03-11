using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDeliveryCore
{
    public class ShopForm
    {
        public string? USERNAME { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? ADDRESS { get; set; }
        public string? POSTAL_CODE { get; set; }
        public string? PHONE_NUMBER { get; set; }
        public string?CITY { get; set; }
        public List<Item> Items { get; set; } 
        public ShopForm()
        {
            Items = new List<Item>();
        }
    }

    public class Item
    {
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal ITEM_PRICE { get; set; }
    }

    public class OrderItems : Item
    {

        public string? ORDER_ID { get; set; }
    }

 }
