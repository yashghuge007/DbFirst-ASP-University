using DBfirst_2106.Data;
using DBfirst_2106.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBfirst_2106.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DbfirstContext _context;

        public StudentsController(DbfirstContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return _context.Student1s != null ?
                        View(await _context.Student1s.ToListAsync()) :
                        Problem("Entity set 'DbfirstContext.Student1s'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student1s == null)
            {
                return NotFound();
            }

            var student1 = await _context.Student1s
                .FirstOrDefaultAsync(m => m.SId == id);
            if (student1 == null)
            {
                return NotFound();
            }

            return View(student1);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SId,SName,SHomecity,SDept")] Student1 student1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student1);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student1s == null)
            {
                return NotFound();
            }

            var student1 = await _context.Student1s.FindAsync(id);
            if (student1 == null)
            {
                return NotFound();
            }
            return View(student1);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SId,SName,SHomecity,SDept")] Student1 student1)
        {
            if (id != student1.SId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Student1Exists(student1.SId))
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
            return View(student1);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student1s == null)
            {
                return NotFound();
            }

            var student1 = await _context.Student1s
                .FirstOrDefaultAsync(m => m.SId == id);
            if (student1 == null)
            {
                return NotFound();
            }

            return View(student1);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student1s == null)
            {
                return Problem("Entity set 'DbfirstContext.Student1s'  is null.");
            }
            var student1 = await _context.Student1s.FindAsync(id);
            if (student1 != null)
            {
                _context.Student1s.Remove(student1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Student1Exists(int id)
        {
            return (_context.Student1s?.Any(e => e.SId == id)).GetValueOrDefault();
        }

        public IActionResult SameHomeandDept()
        {


            var obj = _context.Student1s.Select(e => e.SHomecity).Distinct().ToList();
            ViewBag.SHC = obj;

            var obj2 = _context.Student1s.Select(e => e.SDept).Distinct().ToList();
            ViewBag.SDT = obj2;

            return View();
        }

        [HttpPost]
        public IActionResult SameHomeandDeptSearch(string city, string dept)
        {
            var stu1 = _context.Student1s.Where(x => x.SDept == dept && x.SHomecity == city).Select(e => e);

            return View(stu1);
        }
    }
}
