using System;

namespace DotNetGigs.Models.Entities
{
    
    public class Order
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal Cost { get; set; }
        public DateTime OrderDate { get; set; }
        public string BarCode {get;set;}
    }
}