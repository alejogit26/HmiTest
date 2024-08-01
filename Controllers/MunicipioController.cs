using Api.Data;
using Api.Data.Dto;
using Api.Data.Entities;
using Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly SqlTestContext _context;
        private readonly IMunicipioService _municipioService;

        public MunicipioController(SqlTestContext context, IMunicipioService municipioService)
        {
            _context = context;
            _municipioService = municipioService;
        }

        // GET: api/<MunicipioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Municipio>>> Get()
        {            
            return await _context.Municipio.Select(m => new Municipio
            {
                IdMunicipio = m.IdMunicipio,
                Nombre = m.Nombre,
                Codigo = m.Codigo,
                DepartamentoId = m.DepartamentoId,
                CodigoCompleto = _municipioService.ConsultarCodigoCompleto(m.DepartamentoId, m.Codigo)
            }).ToListAsync();
        }

        // GET api/<MunicipioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio>> Get(int id)
        {
            Municipio? municipio = await _context.Municipio.FirstOrDefaultAsync(x => x.IdMunicipio == id) ?? new Municipio();
            municipio.CodigoCompleto = _municipioService.ConsultarCodigoCompleto(municipio.DepartamentoId, municipio.Codigo);

            if (municipio == null)
            {
                return NotFound();
            }

            return municipio;
        }

        // POST api/<MunicipioController>
        [HttpPost]
        public async Task<ActionResult<Municipio>> Post(Municipio municipio)
        {
            _context.Municipio.Add(municipio);
            await _context.SaveChangesAsync();
            municipio.CodigoCompleto = _municipioService.ConsultarCodigoCompleto(municipio.DepartamentoId, municipio.Codigo);

            return CreatedAtAction(nameof(Get), new { id = municipio.IdMunicipio }, municipio);
        }

        // PUT api/<MunicipioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Municipio municipio)
        {
            if (id != municipio.IdMunicipio)
            {
                return BadRequest();
            }

            _context.Entry(municipio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipioExists(id))
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

        // DELETE api/<MunicipioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _context.Municipio.Remove(municipio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MunicipioExists(int id)
        {
            return _context.Municipio.Any(e => e.IdMunicipio == id);
        }

        [HttpPost("ConsultarDetalleMunicipioPorIndicativo")]

        public async Task<ActionResult<MunicipioDto>> ConsultarDetalleMunicipioPorIndicativo(string indicativo)
        {
            var respuesta = await _municipioService.ConsultarDetalleMunicipioPorIndicativo(indicativo);
            if (respuesta != null)
            {
                return respuesta;
            }
            else
            {
                return NotFound();
            }
        }

    }
}
