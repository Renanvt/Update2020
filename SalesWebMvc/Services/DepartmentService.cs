using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        /*Dependencia para o dbcontext*/
        private readonly SalesWebMvcContext _context;
        //previne que a dependência não possa ser alterada
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //Método pra retornar todos os departamentos, ordenados por nome
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
