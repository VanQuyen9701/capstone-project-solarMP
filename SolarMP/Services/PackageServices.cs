using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Package;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class PackageServices : IPackage
    {
        protected readonly solarMPContext context;
        public PackageServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<Package> delete(string id)
        {
            try
            {
                var check = await this.context.Package.Where(x=>x.PackageId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Package.Update(check);
                    await this.context.SaveChangesAsync();

                    return check;
                }
                return null;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getAll()
        {
            try
            {
                var check = await this.context.Package.Where(x => x.Status)
                    .Include(x=>x.PackageProduct)
                        .ThenInclude(x=>x.Product)
                            .ThenInclude(x=>x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getAllForAdmin()
        {
            try
            {
                var check = await this.context.Package
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Package> getById(string id)
        {
            try
            {
                var check = await this.context.Package.Where(x => x.PackageId.Equals(id))
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .FirstOrDefaultAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getByName(string name)
        {
            try
            {
                var check = await this.context.Package.Where(x => x.Status && x.Name.Contains(name))
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Package> insert(PackageCreateDTO dto)
        {
            try
            {
                var package = new Package();
                package.Status = true;
                package.Name = dto.Name;
                package.Price = 0;
                package.Description = dto.Description;
                package.PackageId = "PCK" + Guid.NewGuid().ToString().Substring(0,13);

                await this.context.Package.AddAsync(package);
                await this.context.SaveChangesAsync();

                return package;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> insertProduct(PackageProductCreateDTO dto)
        {
            try
            {            
                foreach(var x in dto.listProduct)
                {
                    var product = new PackageProduct();
                    product.ProductId = x.productId;
                    product.PackageId = dto.PackageId;
                    product.Status = true;
                    product.Quantity = x.quantity;

                    await this.context.PackageProduct.AddAsync(product);
                    await this.context.SaveChangesAsync();
                    var pro = await this.context.Product.Where(x => x.ProductId.Equals(x.ProductId)).FirstOrDefaultAsync();
                    var pck = await this.context.Package.Where(x => x.PackageId.Equals(dto.PackageId)).FirstOrDefaultAsync();
                    pck.Price += pro.Price* x.quantity;

                    this.context.Package.Update(pck);
                    await this.context.SaveChangesAsync();
                }
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
