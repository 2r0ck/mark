using System;
namespace DotNetGigs.ViewModels.Mapping
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal Cost { get; set; }
        public DateTime OrderDate { get; set; }
        public string BarCode {get;set;}    
    }
}