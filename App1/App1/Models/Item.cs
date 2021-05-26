using System;

namespace App1.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string Date { get; set; }  =  DateTime.Now.ToShortDateString();

        public int Importance { get; set; } = 50;

        public string Category { get; set; }

        public override bool Equals(object obj)
        {
            Item item = obj as Item;
            return this.Id == item.Id;
        }
    }
}