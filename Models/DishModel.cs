using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderAPI.Models
{
    public class DishModel
    {
        public int Id { get; set; }
        public string DishName { get; set; }
    }
}
