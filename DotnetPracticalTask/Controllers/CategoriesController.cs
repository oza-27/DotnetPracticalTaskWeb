using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotnetPracticalTask.Data;
using DotnetPracticalTask.Models;

namespace DotnetPracticalTask.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return View(await _context.category.ToListAsync());
        }

        //// GET: Categories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.category == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.category
        //        .FirstOrDefaultAsync(m => m.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");

            //return View();
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var category = await _context.category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {   
            if (ModelState.IsValid)
            {
                _context.category.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
                        
            return View(obj);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            var category = _context.category.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Category obj)
        {
            
            var categoryFromDb = _context.category.Where(u => u.ParentCategoryId == obj.CategoryId);
            _context.category.RemoveRange(categoryFromDb);
            var category = _context.category.Find(obj.CategoryId);
            _context.category.Remove(category);
            _context.SaveChanges();
            //if (category != null)
            //{
            //    _context.category.Remove(category);
                
            //}
            return RedirectToAction("Index");
        }
        // GET method for child
        public IActionResult CreateChild(int? id)
        {
            ViewBag.Id = id;
            CategoryVM category = new()
            {
                Category = new(),
                categoryList = _context.category.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CategoryId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(category);
            }
            else
            {
                ViewBag.CategoryList = category;
            }

            //IEnumerable<SelectListItem> categoryList = _context.category.ToList().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.CategoryId.ToString(),
            //    });

            return View();
        }

        [HttpPost, ActionName("CreateChild")]
        [ValidateAntiForgeryToken]
        // POST method
        public IActionResult CreateChild(Category obj)
        {
            _context.category.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool CategoryExists(int id)  
        {
          return _context.category.Any(e => e.CategoryId == id);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _context.category.ToList();
            return Json(new { data = categoryList });
        }
        #endregion
    }
}
