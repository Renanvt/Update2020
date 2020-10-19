
using System.Collections.Generic;


namespace SalesWebMvc.Models.ViewModels
{
    //Tela de cadastro de vendedor
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department>  Departments { get; set; }
        
    }
}
