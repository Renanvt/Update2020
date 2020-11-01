using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Dependencia para o SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            //Retorna uma lista de Seller
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost] //notacao
        [ValidateAntiForgeryToken]//previnir ataque csrf
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)//Teste se o modelo foi válidado
            {
                //Valida mesmo se o JavaScript for desabilitado
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); //Aconteçe enquanto o usuário não preencher corretamente o formulário
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            //int?= int opcional
            if(id == null)
            {
                //Requisição feita de forma indevida
                return RedirectToAction(nameof(Error), new { message = "Id not provided "});
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                //Id não existe
                return RedirectToAction(nameof(Error), new { message = "Id not found " });
            }
            //Se tudo der certo
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int ? id)
        {
            //int?= int opcional
            if (id == null)
            {
                //Requisição feita de forma indevida
                return RedirectToAction(nameof(Error), new { message = "Id not provided " });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                //Id não existe
                return RedirectToAction(nameof(Error), new { message = "Id not found " });
            }
            //Se tudo der certo
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided " });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found " });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)//Teste se o modelo foi válidado
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); //Aconteçe enquanto o usuário não preencher corretamente o formulário
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //Macete pra pegar o ID interno da requisição
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
