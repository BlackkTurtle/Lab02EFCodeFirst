using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PO1_HospitalDatabase.Data;
using PO1_HospitalDatabase.Data.Models;

namespace PO1_HospitalDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnoseController : ControllerBase
    {
        private readonly ILogger<DiagnoseController> _logger;
        private HospitalContext context;
        public DiagnoseController(ILogger<DiagnoseController> logger,
            HospitalContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnose>>> GetAllDiagnoseAsync()
        {
            try
            {
                var results = await context.diagnoses.ToListAsync();
                _logger.LogInformation($"Отримали всі дані з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Diagnose>> GetDiagnoseByIdAsync(int id)
        {
            try
            {
                var result = await context.diagnoses.Where(e=>e.DiagnoseId==id).SingleOrDefaultAsync();
                if (result == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostDiagnoseAsync([FromBody] Diagnose fulldiagnose)
        {
            try
            {
                if (fulldiagnose == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт є некоректним");
                }
                var user = new Diagnose()
                {
                    DiagnoseName = fulldiagnose.DiagnoseName,
                    Comments = fulldiagnose.Comments,
                    PatientId = fulldiagnose.PatientId
                };
                await context.diagnoses.AddAsync(user);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiagnoseAsync(int id, [FromBody] Diagnose updateddiagnose)
        {
            try
            {
                if (updateddiagnose == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var Entity = await context.diagnoses.Where(e => e.DiagnoseId == id).SingleOrDefaultAsync();
                if (Entity == null)
                {
                    _logger.LogInformation($"ID: {id} was not found in the database");
                    return NotFound();
                }
                Entity.DiagnoseName = updateddiagnose.DiagnoseName;
                Entity.PatientId = updateddiagnose.PatientId;
                Entity.Comments = updateddiagnose.Comments;
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiagnoseByIdAsync(int id)
        {
            try
            {
                var Entity = await context.diagnoses.Where(e => e.DiagnoseId == id).SingleOrDefaultAsync();
                if (Entity == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                context.diagnoses.Remove(Entity);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
