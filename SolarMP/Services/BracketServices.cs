using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Bracket;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class BracketServices : IBracket
    {
        protected readonly solarMPContext context;
        public BracketServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteBracket(string bracketId)
        {
            try
            {
                var bracket = await this.context.Bracket
                    .Where(x => bracketId.Equals(x.BracketId))
                    .FirstOrDefaultAsync();
                if (bracket != null)
                {
                    bracket.Status = false;
                    this.context.Bracket.Update(bracket);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Bracket found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Bracket>> GetAllBrackets()
        {
            try
            {
                var data = await this.context.Bracket.Where(x => x.Status)
                    .Include(x => x.Image)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Bracket>> GetBracketById(string? bracketId)
        {
            try
            {
                var data = await this.context.Bracket.Where(x => x.Status && x.BracketId.Equals(bracketId))
                    .Include(x => x.Image)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<Bracket> InsertBracket(BracketDTO bracket)
        {
            try
            {
                var _bracket = new Bracket();
                _bracket.BracketId = "BKT" + Guid.NewGuid().ToString().Substring(0, 7);
                _bracket.Name= bracket.Name;
                _bracket.Price = bracket.Price;
                _bracket.Manufacturer= bracket.Manufacturer;
                _bracket.Status = bracket.Status;
                await this.context.Bracket.AddAsync(_bracket);
                this.context.SaveChanges();


                if (bracket.image != null && bracket.image.Count > 0)
                {
                    foreach (var image in bracket.image)
                    {
                        var img = new Image();
                        img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                        img.ImageData = image.image;
                        img.BracketId = _bracket.BracketId;
                        await this.context.Image.AddAsync(img);
                        await this.context.SaveChangesAsync();
                    }
                }


                return _bracket;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateBracket(BracketDTO upBracket)
        {
            try
            {
                Bracket _bracket = await this.context.Bracket.FirstAsync(x => x.BracketId == upBracket.BracketId);
                if (_bracket != null)
                {
                    _bracket.Name = upBracket.Name;
                    _bracket.Price = upBracket.Price;
                    _bracket.Manufacturer = upBracket.Manufacturer;
                    _bracket.Status = upBracket.Status;
                    context.Bracket.Update(_bracket);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Bracket not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
