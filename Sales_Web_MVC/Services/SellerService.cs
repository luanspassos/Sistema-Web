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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(_context => _context.Department).FirstOrDefault(s => s.Id == id);
        }


        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);  //Seller seller = _context.Seller.FirstOrDefault(s => s.Id == id);

            _context.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(x => x.Id == x.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
