using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PO1_HospitalDatabase.Data;
using PO1_HospitalDatabase.Data.Models;

namespace PO1_HospitalDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMedicamentsController : ControllerBase
    {
        private readonly ILogger<PatientMedicamentsController> _logger;
        private HospitalContext context;
        public PatientMedicamentsController(ILogger<PatientMedicamentsController> logger,
            HospitalContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientMedicament>>> GetAllPatientMedicamentsAsync()
        {
            try
            {
                var results = await context.patientmedicaments.ToListAsync();
                _logger.LogInformation($"Отримали всі дані з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("patientid/{id}")]
        public async Task<ActionResult<PatientMedicament>> GetPatientMedicamentsByPatientIdAsync(int id)
        {
            try
            {
                var result = await context.patientmedicaments.Where(e=>e.PatientId==id).SingleOrDefaultAsync();
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
        [HttpGet("medicamentid/{id}")]
        public async Task<ActionResult<Diagnose>> GetPatientMedicamentsByMedicamentIdAsync(int id)
        {
            try
            {
                var result = await context.patientmedicaments.Where(e => e.MedicamentId == id).SingleOrDefaultAsync();
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
        public async Task<ActionResult> PostPatientMedicamentsAsync([FromBody] PatientMedicament fulldiagnose)
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
                var user = new PatientMedicament()
                {
                    PatientId = fulldiagnose.PatientId,
                    MedicamentId = fulldiagnose.MedicamentId
                };
                await context.patientmedicaments.AddAsync(user);
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
        [HttpPut("{id1}/{id2}")]
        public async Task<ActionResult> UpdatePatientMedicamentsAsync(int id1,int id2, [FromBody] PatientMedicament updateddiagnose)
        {
            try
            {
                if (updateddiagnose == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var Entity = await context.patientmedicaments.Where(e => e.PatientId == id1 && e.MedicamentId==id2).SingleOrDefaultAsync();
                if (Entity == null)
                {
                    _logger.LogInformation($"ID: {id1} {id2} was not found in the database");
                    return NotFound();
                }
                Entity.PatientId = updateddiagnose.PatientId;
                Entity.MedicamentId = updateddiagnose.MedicamentId;
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
        [HttpDelete("{id1}/{id2}")]
        public async Task<ActionResult> DeletePatientMedicamentsByIdAsync(int id1,int id2)
        {
            try
            {
                var Entity = await context.patientmedicaments.Where(e => e.PatientId == id1 && e.MedicamentId == id2).SingleOrDefaultAsync();
                if (Entity == null)
                {
                    _logger.LogInformation($"Id: {id1} {id2}, не був знайдейний у базі даних");
                    return NotFound();
                }

                context.patientmedicaments.Remove(Entity);
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
