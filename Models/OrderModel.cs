using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data;
using OrderAPI.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public List<int> SelectedDishes { get; set; }
        public List<OrderedDishViewModel> OrderedDishes { get; set; }
    }
}
