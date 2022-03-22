using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Contorllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<ProtflioItem> _portflio;

        public HomeController(IUnitOfWork<Owner> owner,IUnitOfWork<ProtflioItem> portflio)
        {
            _owner = owner;
           _portflio = portflio;
        }
        public IActionResult Index()
        {
            var homeviewmodel = new HomeViewModel
            {
                owner = _owner.Entity.GetAll().First(),
                protflioItems = _portflio.Entity.GetAll().ToList()
            };
            return View(homeviewmodel);
        }
    }
}
