using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoCurso.Domain;

namespace ProjetoCurso.Persistence.Contrato
{
    public interface IPalestrantePersist
    {
        //PALESTRANTES
        Task<Palestrante[]>NomeAllPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
    }
}