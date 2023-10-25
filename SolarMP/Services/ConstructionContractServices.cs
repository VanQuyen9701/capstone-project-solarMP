using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.ConstructionContract;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class ConstructionContractServices : IConstructionContract
    {
        protected readonly solarMPContext context;
        public ConstructionContractServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteConstructionContract(string constructionContractId)
        {
            try
            {
                var con = await this.context.ConstructionContract
                    .Where(x => constructionContractId.Equals(x.ConstructioncontractId))
                    .FirstOrDefaultAsync();
                if (con != null)
                {
                    con.Status = false;
                    this.context.ConstructionContract.Update(con);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Construction Contract found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<ConstructionContract>> GetAllConstructionContracts()
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.Status)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<ConstructionContract>> GetConstructionContractById(string? constructionContractId)
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.Status && x.ConstructioncontractId.Equals(constructionContractId)).ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> InsertConstructionContract(ConstructionContractDTO constructionContract)
        {
            try
            {
                var _constructionContract = new ConstructionContract();
                _constructionContract.ConstructioncontractId = "CON" + Guid.NewGuid().ToString().Substring(0, 7);
                _constructionContract.Startdate = constructionContract.Startdate;
                _constructionContract.Enddate = constructionContract.Enddate;
                _constructionContract.Totalcost = constructionContract.Totalcost;
                _constructionContract.IsConfirmed= constructionContract.IsConfirmed;
                _constructionContract.ImageFile= constructionContract.ImageFile;
                _constructionContract.CustomerId = constructionContract.CustomerId;
                _constructionContract.Staffid = constructionContract.Staffid;
                _constructionContract.PackageId = constructionContract.PackageId;
                _constructionContract.BracketId = constructionContract.BracketId;
                _constructionContract.Status = true;
                await this.context.ConstructionContract.AddAsync(_constructionContract);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateConstructionContract(ConstructionContractDTO upConstructionContract)
        {
            try
            {
                ConstructionContract _constructionContract = await this.context.ConstructionContract.FirstAsync(x => x.ConstructioncontractId == upConstructionContract.ConstructioncontractId);
                if (_constructionContract != null)
                {
                    _constructionContract.Startdate = upConstructionContract.Startdate;
                    _constructionContract.Enddate = upConstructionContract.Enddate;
                    _constructionContract.Totalcost = upConstructionContract.Totalcost;
                    _constructionContract.IsConfirmed = upConstructionContract.IsConfirmed;
                    _constructionContract.ImageFile = upConstructionContract.ImageFile;
                    _constructionContract.CustomerId = upConstructionContract.CustomerId;
                    _constructionContract.Staffid = upConstructionContract.Staffid;
                    _constructionContract.PackageId = upConstructionContract.PackageId;
                    _constructionContract.BracketId = upConstructionContract.BracketId;
                    _constructionContract.Status = true;
                    context.ConstructionContract.Update(_constructionContract);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Construction Contract not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
