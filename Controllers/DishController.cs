using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly OrderDbContext _context;
        public DishController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/createDish")]
        public async Task<IResult> Create(DishModel dish)
        {
            _context.Add(dish);
            await _context.SaveChangesAsync();

            return TypedResults.Json(dish);
        }

        [HttpGet]
        [Route("/getAllDishes")]
        public async Task<IResult> GetAll(OrderDbContext context)
        {
            return TypedResults.Ok(await context.Dishes.ToArrayAsync());
        }


        [HttpDelete]
        [Route("/deleteDish")]
        public async Task<IResult> Delete(int id)
        {
            if (await _context.Dishes.FindAsync(id) is DishModel dish)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();

                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.NotFound("Такого блюда не существует");
            }
        }
    }
}
