using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        /*Dependencia para o dbcontext*/
        private readonly SalesWebMvcContext _context;
        //previne que a dependência não possa ser alterada
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //Operação assíncrona que busca os registros de venda por data
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Encontrar as vendas no intervalo de datas definido
            //Transformando o objeto em IQueryable
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller) //Faz o join das tabelas
                .Include(x => x.Seller.Department) // Faz o join das tabelas de departamento
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
