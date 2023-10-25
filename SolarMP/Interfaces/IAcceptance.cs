using SolarMP.DTOs.Acceptances;
using SolarMP.DTOs.Promotions;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IAcceptance
    {
        Task<List<Acceptance>> GetAcceptanceById(string? acceptanceId);
        Task<List<Acceptance>> GetAllAcceptances();
        Task<bool> UpdateAcceptance(AcceptanceDTO upAcceptance);
        Task<bool> DeleteAcceptance(string acceptanceId);
        Task<bool> InsertAcceptance(AcceptanceDTO acceptance);
    }
}
