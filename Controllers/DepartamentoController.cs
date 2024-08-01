using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly SqlTestContext _context;

        public DepartamentoController(SqlTestContext context)
        {
            _context = context;
        }

        // GET: api/<DepartamentoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> Get()
        {
            var listaDepartamentos = await _context.Departamento.Include(d => d.Municipios).ToListAsync();

            if (listaDepartamentos != null)
            {
                MapearDepartamentos(listaDepartamentos);
            }

            return listaDepartamentos ?? new List<Departamento>();
        }

        private static void MapearDepartamentos(List<Departamento>? listaDepartamentos)
        {
            foreach (var departamento in listaDepartamentos ?? new List<Departamento>())
            {
                MapearMunicipio(departamento);
            }
        }

        private static void MapearMunicipio(Departamento departamento)
        {
            foreach (var municipio in departamento.Municipios)
            {
                municipio.CodigoCompleto = $"{departamento.Codigo}{municipio.Codigo}";
            }
        }

        // GET api/<DepartamentoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> Get(int id)
        {
            var departamento = await _context.Departamento.Include(d => d.Municipios).FirstOrDefaultAsync(x => x.IdDepartamento == id);

            if (departamento != null)
            {
                MapearMunicipio(departamento);
            }

            return departamento != null ? departamento : NotFound();
        }


        // POST api/<DepartamentoController>
        [HttpPost]
        public async Task<ActionResult<Departamento>> Post(Departamento departamento)
        {
            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { Codigo = departamento.IdDepartamento }, departamento);
        }

        // PUT api/<DepartamentoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Departamento departamento)
        {
            if (id != departamento.IdDepartamento)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<DepartamentoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.IdDepartamento == id);
        }
    }
}
