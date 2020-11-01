using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        //Operação síncrona - A aplicação vai ficar bloqueada esperando a ação ou operação terminar pra depois a aplicação continuar executando
        //Operação assíncrona - Melhora a performance da aplicação pois a aplicação não vai ficar bloqueada esperando a ação termina
        public async Task<List<Department>> FindAllAsync()
        {
            //Tasks -> Objeto que encapsula o processamento assíncrono deixando a programação mais fácil
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
            //await -> Informa ao compilador que é uma chamada Assíncrona
        }
    }
}
