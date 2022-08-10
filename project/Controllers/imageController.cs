using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class imageController : Controller
    {
        private readonly imagedbcontext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public imageController(imagedbcontext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: image
        public async Task<IActionResult> Index()
        {
            return View(await _context.images.ToListAsync());
        }

        // GET: image/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemodal = await _context.images
                .FirstOrDefaultAsync(m => m.imageid == id);
            if (imagemodal == null)
            {
                return NotFound();
            }

            return View(imagemodal);
        }

        // GET: image/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("imageid,title,imagefile")] imagemodal imagemodal)
        {
            if (ModelState.IsValid)
            {
                //Save İmage to wwwrootpath/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(imagemodal.imagefile.FileName);
                string extension = Path.GetExtension(imagemodal.imagefile.FileName);
               imagemodal.imagename = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imagemodal.imagefile.CopyToAsync(fileStream);
                }



                _context.Add(imagemodal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imagemodal);
        }

        // GET: image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemodal = await _context.images.FindAsync(id);
            if (imagemodal == null)
            {
                return NotFound();
            }
            return View(imagemodal);
        }

        // POST: image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("imageid,title,imagename")] imagemodal imagemodal)
        {
            if (id != imagemodal.imageid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagemodal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!imagemodalExists(imagemodal.imageid))
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
            return View(imagemodal);
        }

        // GET: image/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemodal = await _context.images
                .FirstOrDefaultAsync(m => m.imageid == id);
            if (imagemodal == null)
            {
                return NotFound();
            }

            return View(imagemodal);
        }

        // POST: image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagemodal = await _context.images.FindAsync(id);
            //delete image from wwwroot/image
            var imagepath = Path.Combine(_hostEnvironment.WebRootPath, "image", imagemodal.imagename);
            if (System.IO.File.Exists(imagepath))
                System.IO.File.Exists(imagepath);
            //delete the record 
            _context.images.Remove(imagemodal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool imagemodalExists(int id)
        {
            return _context.images.Any(e => e.imageid == id);
        }
    }
}
