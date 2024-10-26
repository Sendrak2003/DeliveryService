using DeliveryService.DTO;
using DeliveryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly DeliveryContext _context;

        public OrdersController(ILogger<OrdersController> logger, DeliveryContext context)
        {
            _logger = logger;
            _context = context;
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            _logger.LogInformation("Получение всех заказов");

            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterOrders(string district, DateTime firstDeliveryDateTime)
        {
            try
            {
                _logger.LogInformation("Фильтрация заказов для района {District} с временем {FirstDeliveryDateTime}", district, firstDeliveryDateTime);

                if (string.IsNullOrWhiteSpace(district) || firstDeliveryDateTime == default)
                {
                    _logger.LogWarning("Некорректные входные данные: район = {District}, время = {FirstDeliveryDateTime}", district, firstDeliveryDateTime);
                    return BadRequest("Некорректные входные данные.");
                }

                var filterTime = firstDeliveryDateTime.AddMinutes(30);
                var filteredOrders = await _context.Orders
                    .Where(o => o.District == district && o.DeliveryTime >= firstDeliveryDateTime && o.DeliveryTime <= filterTime)
                    .ToListAsync();

                _logger.LogInformation("Найдено {Count} заказов.", filteredOrders.Count);

                return Ok(filteredOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при фильтрации заказов для района {District} с временем {FirstDeliveryDateTime}", district, firstDeliveryDateTime);
                return StatusCode(500, "Внутренняя ошибка сервера.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderDto orderDto)
        {
            if (orderDto == null)
            {
                _logger.LogWarning("Попытка добавить пустой заказ.");
                return BadRequest("Некорректные входные данные.");
            }

            try
            {
                var order = new Order
                {
                    Weight = orderDto.Weight,
                    District = orderDto.District,
                    DeliveryTime = orderDto.DeliveryTime
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Заказ добавлен с ID {OrderId}.", order.Id);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении заказа.");
                return StatusCode(500, "Ошибка при добавлении заказа.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .SingleOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
