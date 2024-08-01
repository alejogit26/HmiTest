using Api.Data.Dto;
using Api.Data.Entities;

namespace Api.Domain
{
    public interface IMunicipioService
    {
        Task<MunicipioDto?> ConsultarDetalleMunicipioPorIndicativo(string indicativo);
        Task<Departamento> ConsultarDepartamento(int departamentoId);
        string? ConsultarCodigoCompleto(int departamentoId, string? codigoMunicipio);
    }
}