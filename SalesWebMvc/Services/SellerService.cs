using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        /*Dependencia para o dbcontext*/
        private readonly SalesWebMvcContext _context;
        //previne que a dependência não possa ser alterada
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //Retorna uma lista com todos os vendedores do banco de dados
        public async Task<List<Seller>> FindAllAsync()
        {
            //Operacao sincrona, A aplicação vai ficar bloqueada, esperando essa ação terminar
            return await _context.Seller.ToListAsync();
        }
        //Inserir um novo vendedor no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

        }
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            //hasAny -> Tem algum
            bool hasAny =await _context.Seller.AnyAsync(x => x.Id == obj.Id); 
      
            if (!hasAny)//Se não existe algum registro no banco de dados com o mesmo Id do objeto
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
