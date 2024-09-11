namespace Sales_Web_MVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Add(Seller s) { Sellers.Add(s); }
        public int TotalDepartmentSales(DateTime inicial, DateTime final)
        {
            int soma = 0;
            foreach (Seller s in Sellers)
            {
                if (s.BirthDate >= inicial && s.BirthDate <= final)
                {
                    soma += s.Sales.Count();
                }
            }
            return soma;
        }
        public double TotalDepartmentValue(DateTime inicial, DateTime final) { return Sellers.Sum(s => s.TotalSalesValue(inicial, final)); }

    }
}
