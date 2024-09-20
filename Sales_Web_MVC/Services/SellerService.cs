using Microsoft.EntityFrameworkCore;
using Sales_Web_MVC.Data;
using Sales_Web_MVC.Models;
using Sales_Web_MVC.Services.Exceptions;

namespace Sales_Web_MVC.Services
{
    public class SellerService
    {
        private readonly Sales_Web_MVCContext _context;

        public SellerService(Sales_Web_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(_context => _context.Department).FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);  //Seller seller = await _context.Seller.FirstOrDefaultAsync(s => s.Id == id);

            _context.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == x.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
