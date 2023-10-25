using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Account;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class AccountServices : IAccount
    {
        protected readonly solarMPContext context;
        public AccountServices(solarMPContext context)
        {
            this.context = context;
        }

        public async Task<Account> delete(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Account.Update(check);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> deleteHardCode(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    this.context.Account.RemoveRange(check);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE"))
                {
                    throw new Exception("data còn ở các bảng khác nên không xóa được đâu!");
                }
                throw new Exception("Lỗi gòi");
            }
        }

        public async Task<List<Account>> getAll()
        {
            try
            {
                var account = await this.context.Account
                    .Include(x=>x.Role)
                    .ToListAsync();
                return account;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> getById(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x=>x.AccountId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Account>> getByName(string name)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.Firstname.Contains(name) || x.Lastname.Contains(name)).ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> register(AccountRegisterDTO dto)
        {
            try
            {
                var account = new Account();
                account.AccountId = "ACC"+Guid.NewGuid().ToString().Substring(0,13);
                account.RoleId = dto.RoleId ?? "4";
                account.Phone = dto.Phone;
                account.Email = dto.Email;
                account.Username = dto.Username;
                account.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                account.Address = dto.Address;
                account.Gender= dto.Gender;
                account.Firstname = dto.Firstname;
                account.Lastname = dto.Lastname;
                account.Status = true;
                account.CreateAt = DateTime.Now;
                account.IsGoogleProvider = dto.IsGoogleProvider;

                await this.context.Account.AddAsync(account);
                await this.context.SaveChangesAsync();
                
                return account;
            }
            catch(Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    if (ex.InnerException.Message.Contains("User"))
                    {
                        throw new Exception("Tên đăng nhập đã tồn tại");
                    }
                    if (ex.InnerException.Message.Contains("email"))
                    {
                        throw new Exception("Email đã tồn tại");
                    }
                    if (ex.InnerException.Message.Contains("phone"))
                    {
                        throw new Exception("Số điện thoại đã tồn tại");
                    }
                }
                throw new Exception("Lỗi đăng ký");
            }
        }

        public async Task<Account> update(AccountUpdateDTO dto)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(dto.AccountId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Address = dto.Address ?? check.Address;
                    check.Gender = dto.Gender ?? check.Gender;
                    check.Firstname= dto.Firstname ?? check.Firstname;
                    check.Lastname= dto.Lastname ?? check.Lastname;
                    if(dto.Password!= null)
                    {
                        check.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                    }         
                    check.IsGoogleProvider = dto.IsGoogleProvider ?? check.IsGoogleProvider;
                    check.Status = (bool)dto.Status;
                    this.context.Account.Update(check);
                    this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
