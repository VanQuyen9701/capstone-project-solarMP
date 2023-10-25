using SolarMP.DTOs.ConstructionContract;
using SolarMP.DTOs.Promotions;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IConstructionContract
    {
        Task<List<ConstructionContract>> GetConstructionContractById(string? constructionContractId);
        Task<List<ConstructionContract>> GetAllConstructionContracts();
        Task<bool> UpdateConstructionContract(ConstructionContractDTO upConstructionContract);
        Task<bool> DeleteConstructionContract(string constructionContractId);
        Task<bool> InsertConstructionContract(ConstructionContractDTO constructionContract);
    }
}
