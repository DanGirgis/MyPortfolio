using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using MyPortfolio.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interface;

namespace MyPortfolio.Controllers
{
    public class Portfolio : Controller
    {
        private readonly IUnitOfWork<ProtflioItem> _portfolio;
        private readonly IHostingEnvironment _hosting;

        public Portfolio(IUnitOfWork<ProtflioItem> portfolio, IHostingEnvironment hosting)
        {
            _portfolio = portfolio;
            _hosting = hosting;
        }

        // GET: ProtflioItems
        public IActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: ProtflioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProtflioItem = _portfolio.Entity.GetById(id);
            if (ProtflioItem == null)
            {
                return NotFound();
            }

            return View(ProtflioItem);
        }

        // GET: ProtflioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProtflioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProtfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                ProtflioItem ProtflioItem = new ProtflioItem
                {
                    NameProject = model.NameProject,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };

                _portfolio.Entity.Insert(ProtflioItem);
                _portfolio.save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: ProtflioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProtflioItem = _portfolio.Entity.GetById(id);
            if (ProtflioItem == null)
            {
                return NotFound();
            }

            ProtfolioViewModel ProtfolioViewModel = new ProtfolioViewModel
            {
                Id = ProtflioItem.Id,
                Description = ProtflioItem.Description,
                ImageUrl = ProtflioItem.ImageUrl,
                NameProject = ProtflioItem.NameProject
            };

            return View(ProtfolioViewModel);
        }

        // POST: ProtflioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ProtfolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    ProtflioItem ProtflioItem = new ProtflioItem
                    {
                        Id = model.Id,
                        NameProject = model.NameProject,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };

                    _portfolio.Entity.Update(ProtflioItem);
                    _portfolio.save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtflioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProtflioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProtflioItem = _portfolio.Entity.GetById(id);
            if (ProtflioItem == null)
            {
                return NotFound();
            }

            return View(ProtflioItem);
        }

        // POST: ProtflioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtflioItemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}