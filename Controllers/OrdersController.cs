using AgriConnect.Data;
using AgriConnect.Dtos;
using AgriConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Product)
                .Select(o => new {
                    o.Id,
                    o.BuyerId,
                    BuyerName = o.Buyer.Name,
                    o.ProductId,
                    ProductName = o.Product.Name,
                    o.Quantity,
                    o.Status,
                    o.TotalPrice
                })
                .ToListAsync();

            return Ok(orders);
        }


        // POST: api/Orders
        [Authorize(Roles = "farmer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var product = await _context.Products.FindAsync(orderDto.ProductId);
            if (product == null || product.Quantity < orderDto.Quantity)
            {
                return BadRequest("Product not available or insufficient quantity.");
            }

            var order = new Order
            {
                BuyerId = UserId,
                ProductId = orderDto.ProductId,
                Quantity = orderDto.Quantity,
                TotalPrice = product.Price * orderDto.Quantity,
                Status = "Pending"
            };

            product.Quantity -= orderDto.Quantity;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var buyer = await _context.UsersAgri.FindAsync(order.BuyerId);

            var responseDto = new OrderDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                BuyerName = buyer?.Name,
                ProductId = order.ProductId,
                ProductName = product.Name,
                Quantity = order.Quantity,
                Status = order.Status,
                TotalPrice = order.TotalPrice
            };

            return CreatedAtAction(nameof(GetOrders), new { id = responseDto.Id }, responseDto);
        }



        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest();

            _context.Entry(updatedOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // PATCH: api/Orders/complete/5
        [Authorize(Roles = "farmer")]
        [HttpPatch("complete/{id}")]
        public async Task<IActionResult> MarkOrderAsComplete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            if (order.Status == "Completed")
            {
                return BadRequest(new { message = "Order is already marked as completed." });
            }

            order.Status = "Completed";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Order marked as completed successfully.",
                orderId = order.Id,
                newStatus = order.Status
            });
        }

    }

}
