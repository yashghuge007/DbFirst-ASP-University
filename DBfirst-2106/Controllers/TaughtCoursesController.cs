using DBfirst_2106.Data;
using DBfirst_2106.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DBfirst_2106.Controllers
{
    public class TaughtCoursesController : Controller
    {
        private readonly DbfirstContext _context;

        public TaughtCoursesController(DbfirstContext context)
        {
            _context = context;
        }

        // GET: TaughtCourses
        public async Task<IActionResult> Index()
        {
            var dbfirstContext = _context.TauCourses.Include(t => t.Course).Include(t => t.PIdNavigation);
            return View(await dbfirstContext.ToListAsync());
        }

        // GET: TaughtCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TauCourses == null)
            {
                return NotFound();
            }

            var tauCourse = await _context.TauCourses
                .Include(t => t.Course)
                .Include(t => t.PIdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tauCourse == null)
            {
                return NotFound();
            }

            return View(tauCourse);
        }

        // GET: TaughtCourses/Create
        public IActionResult Create()
        {

            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid");
            ViewData["Coursename"] = new SelectList(_context.Courses, "Courseid", "Coursename");
            ViewData["PId"] = new SelectList(_context.Professors, "PId", "PId");
            return View();
        }

        // POST: TaughtCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PId,Courseid,Id")] TauCourse tauCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tauCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", tauCourse.Courseid);
            ViewData["PId"] = new SelectList(_context.Professors, "PId", "PId", tauCourse.PId);
            return View(tauCourse);
        }

        // GET: TaughtCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TauCourses == null)
            {
                return NotFound();
            }

            var tauCourse = await _context.TauCourses.FindAsync(id);
            if (tauCourse == null)
            {
                return NotFound();
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", tauCourse.Courseid);
            ViewData["PId"] = new SelectList(_context.Professors, "PId", "PId", tauCourse.PId);
            return View(tauCourse);
        }

        // POST: TaughtCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PId,Courseid,Id")] TauCourse tauCourse)
        {
            if (id != tauCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tauCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TauCourseExists(tauCourse.Id))
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
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", tauCourse.Courseid);
            ViewData["PId"] = new SelectList(_context.Professors, "PId", "PId", tauCourse.PId);
            return View(tauCourse);
        }

        // GET: TaughtCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TauCourses == null)
            {
                return NotFound();
            }

            var tauCourse = await _context.TauCourses
                .Include(t => t.Course)
                .Include(t => t.PIdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tauCourse == null)
            {
                return NotFound();
            }

            return View(tauCourse);
        }

        // POST: TaughtCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TauCourses == null)
            {
                return Problem("Entity set 'DbfirstContext.TauCourses'  is null.");
            }
            var tauCourse = await _context.TauCourses.FindAsync(id);
            if (tauCourse != null)
            {
                _context.TauCourses.Remove(tauCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TauCourseExists(int id)
        {
            return (_context.TauCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
