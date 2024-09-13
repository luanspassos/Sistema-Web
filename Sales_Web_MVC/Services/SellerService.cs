using Microsoft.EntityFrameworkCore;
using Sales_Web_MVC.Data;
using Sales_Web_MVC.Models;

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
    }
}
