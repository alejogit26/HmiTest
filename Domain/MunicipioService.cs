using Api.Data;
using Api.Data.Dto;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain
{
    public class MunicipioService : IMunicipioService
    {
        private readonly SqlTestContext _context;

        public MunicipioService(SqlTestContext context)
        {
            _context = context;
        }

        public async Task<MunicipioDto?> ConsultarDetalleMunicipioPorIndicativo(string indicativo)
        {
            string indicativoMunicipio = indicativo.Substring(2);
            string indicativoDepartamento = indicativo.Substring(0, 2);
            var departamentoActual = await _context.Departamento.FirstOrDefaultAsync(x => x.Codigo == indicativoDepartamento);
            var municipioActual = await _context.Municipio.FirstOrDefaultAsync(x => x.Codigo == indicativoMunicipio) ?? new Municipio();
            municipioActual.CodigoCompleto = ConsultarCodigoCompleto(municipioActual.DepartamentoId, municipioActual.Codigo);

            return new MunicipioDto()
            {
                Municipio = municipioActual,
                Departamento = departamentoActual
            };
        }

        public async Task<Departamento> ConsultarDepartamento(int departamentoId)
        {
            var departamentoActual = await _context.Departamento.FirstOrDefaultAsync(x => x.IdDepartamento == departamentoId) ?? new Departamento();

            return departamentoActual;
        }

        public string? ConsultarCodigoCompleto(int departamentoId, string? codigoMunicipio)
        {
            var departamentoActual = _context.Departamento.FirstOrDefaultAsync(x => x.IdDepartamento == departamentoId).GetAwaiter().GetResult() 
                                     ?? new Departamento();

            return $"{departamentoActual.Codigo}{codigoMunicipio}";
        }
    }
}
