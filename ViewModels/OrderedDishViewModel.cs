using OrderAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderAPI.ViewModels
{
    public class OrderedDishViewModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }

        public DishModel Dish { get; set; }
        public int Quantity { get; set; }

        //public int OrderId { get; set; }
    }
}
