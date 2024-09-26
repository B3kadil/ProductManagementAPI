// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using System.Text;
using System.IO;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        // Метод для получения списка продуктов с поддержкой пагинации
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 0, int pageSize = 10)
        {
            try
            {
                // Пагинация списка продуктов
                var products = await _context.Products
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                // Возврат ошибки сервера в случае неудачи
                return StatusCode(500, $"Ошибка при получении продуктов: {ex.Message}");
            }
        }

        // POST: api/products
        // Метод для создания нового продукта
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                // Установка даты создания и добавление продукта в базу данных
                product.CreatedAt = DateTime.Now;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Возврат ошибки сервера в случае неудачи
                return StatusCode(500, $"Ошибка при создании продукта: {ex.Message}");
            }
        }

        // GET: api/products/{id}
        // Метод для получения продукта по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                // Поиск продукта по ID
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    // Возврат ошибки 404, если продукт не найден
                    return NotFound("Продукт не найден.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Возврат ошибки сервера в случае неудачи
                return StatusCode(500, $"Ошибка при получении продукта: {ex.Message}");
            }
        }

        // PUT: api/products/{id}
        // Метод для обновления продукта по ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                // Возврат ошибки 400, если ID продукта не совпадают
                return BadRequest("ID продукта не совпадают.");
            }

            // Обновление состояния продукта
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                // Сохранение изменений
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Проверка, существует ли продукт
                if (!ProductExists(id))
                {
                    return NotFound("Продукт не найден.");
                }
                else
                {
                    // Возврат ошибки сервера, если произошла ошибка конкуренции данных
                    return StatusCode(500, "Ошибка при обновлении продукта.");
                }
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                return StatusCode(500, $"Ошибка при обновлении продукта: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/products/{id}
        // Метод для удаления продукта по ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                // Поиск продукта для удаления
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound("Продукт не найден.");
                }

                // Удаление продукта
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Возврат ошибки сервера в случае неудачи
                return StatusCode(500, $"Ошибка при удалении продукта: {ex.Message}");
            }
        }

        // GET: api/products/export
        // Метод для экспорта списка продуктов в формате CSV
        [HttpGet("export")]
        public async Task<IActionResult> ExportProducts()
        {
            try
            {
                // Получение списка продуктов
                var products = await _context.Products.ToListAsync();

                // Генерация CSV с использованием ; как разделителя
                var csv = new StringBuilder();
                csv.AppendLine("Id;Name;Description;Price;CreatedAt");

                foreach (var product in products)
                {
                    csv.AppendLine($"{product.Id};{product.Name};{product.Description};{product.Price};{product.CreatedAt}");
                }

                // Создание MemoryStream для отправки CSV файла
                var byteArray = Encoding.UTF8.GetBytes(csv.ToString());
                var stream = new MemoryStream(byteArray);
                return File(stream, "text/csv", "products.csv");
            }
            catch (Exception ex)
            {
                // Возврат ошибки сервера в случае неудачи
                return StatusCode(500, $"Ошибка при экспорте продуктов: {ex.Message}");
            }
        }

        // Метод для проверки существования продукта по ID
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
