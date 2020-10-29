using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public List<Seller> FindAll()
        {
            //Operacao sincrona, A aplicação vai ficar bloqueada, esperando essa ação terminar
            return _context.Seller.ToList();
        }
        //Inserir um novo vendedor no banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
          
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
