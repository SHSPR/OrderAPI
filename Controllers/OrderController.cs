using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;
        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/createOrder")]
        public async Task<IResult> Create(OrderModel order)
        {

            order.OrderedDishes = new List<OrderedDishViewModel>();

            order.SelectedDishes = order.SelectedDishes ?? new List<int>();

            if (order.SelectedDishes.Any())
            {
                var nonExistentDishes = order.SelectedDishes.Except(_context.Dishes.Select(d => d.Id)).ToList();

                if (nonExistentDishes.Any())
                {
                    return TypedResults.BadRequest($"Блюда с такими Id: {string.Join(", ", nonExistentDishes)} не существуют");
                }
                else
                {
                    var dishes = _context.Dishes.Where(d => order.SelectedDishes.Contains(d.Id)).ToList();

                    foreach (var dish in dishes)
                    {
                        if (dish != null)
                        {
                            var orderedDish = new OrderedDishViewModel
                            {
                                DishId = dish.Id,
                                Quantity = order.SelectedDishes.Count(d => d == dish.Id),
                            };

                            order.OrderedDishes.Add(orderedDish);
                        }
                    }

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    return TypedResults.Json(order);
                }
            }
            else { return TypedResults.BadRequest("Не выбрано ни одного блюда"); }
        }

        [HttpGet]
        [Route("/getAllOrders")]
        public async Task<IResult> GetAll()
        {
            if (_context.Orders == null)
            {
                return TypedResults.NotFound("Заказов нет");
            }

            return TypedResults.Json(await _context.Orders.Include(order => order.OrderedDishes).ThenInclude(od => od.Dish).ToListAsync());
        }

        [HttpGet]
        [Route("/getOrder/{id}")]
        public async Task<IResult> Get(int id)
        {
            var order = await _context.Orders.Include(order => order.OrderedDishes).ThenInclude(od => od.Dish).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return TypedResults.NotFound("Нет такого заказа");
            }

            return TypedResults.Ok(order);
        }

        [HttpDelete]
        [Route("/deleteOrder/{id}")]
        public async Task<IResult> Delete(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            var order = await _context.Orders.Include(order => order.OrderedDishes).ThenInclude(od => od.Dish).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return TypedResults.NotFound("Такого заказа не существует");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}
