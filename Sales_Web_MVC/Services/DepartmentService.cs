using Microsoft.EntityFrameworkCore;
using Sales_Web_MVC.Data;
using Sales_Web_MVC.Models;

namespace Sales_Web_MVC.Services
{
    public class DepartmentService
    {
        private readonly Sales_Web_MVCContext _context;

        public DepartmentService(Sales_Web_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllDepartmentsAsync() 
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
