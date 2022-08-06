using DBfirst_2106.Data;
using DBfirst_2106.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DBfirst_2106.Controllers
{
    public class EnrollCoursesController : Controller
    {
        private readonly DbfirstContext _context;

        public EnrollCoursesController(DbfirstContext context)
        {
            _context = context;
        }

        // GET: EnrollCourses
        public async Task<IActionResult> Index()
        {
            var dbfirstContext = _context.StuEnrollCourses.Include(s => s.Course).Include(s => s.SIdNavigation);
            return View(await dbfirstContext.ToListAsync());
        }

        // GET: EnrollCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StuEnrollCourses == null)
            {
                return NotFound();
            }

            var stuEnrollCourse = await _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuEnrollCourse == null)
            {
                return NotFound();
            }

            return View(stuEnrollCourse);
        }

        // GET: EnrollCourses/Create
        public IActionResult Create()
        {
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid");
            ViewData["SId"] = new SelectList(_context.Student1s, "SId", "SId");
            return View();
        }

        // POST: EnrollCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SId,Courseid,Grade,Id")] StuEnrollCourse stuEnrollCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stuEnrollCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", stuEnrollCourse.Courseid);
            ViewData["SId"] = new SelectList(_context.Student1s, "SId", "SId", stuEnrollCourse.SId);
            return View(stuEnrollCourse);
        }

        // GET: EnrollCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StuEnrollCourses == null)
            {
                return NotFound();
            }

            var stuEnrollCourse = await _context.StuEnrollCourses.FindAsync(id);
            if (stuEnrollCourse == null)
            {
                return NotFound();
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", stuEnrollCourse.Courseid);
            ViewData["SId"] = new SelectList(_context.Student1s, "SId", "SId", stuEnrollCourse.SId);
            return View(stuEnrollCourse);
        }

        // POST: EnrollCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SId,Courseid,Grade,Id")] StuEnrollCourse stuEnrollCourse)
        {
            if (id != stuEnrollCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stuEnrollCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StuEnrollCourseExists(stuEnrollCourse.Id))
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
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", stuEnrollCourse.Courseid);
            ViewData["SId"] = new SelectList(_context.Student1s, "SId", "SId", stuEnrollCourse.SId);
            return View(stuEnrollCourse);
        }

        // GET: EnrollCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StuEnrollCourses == null)
            {
                return NotFound();
            }

            var stuEnrollCourse = await _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuEnrollCourse == null)
            {
                return NotFound();
            }

            return View(stuEnrollCourse);
        }

        // POST: EnrollCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StuEnrollCourses == null)
            {
                return Problem("Entity set 'DbfirstContext.StuEnrollCourses'  is null.");
            }
            var stuEnrollCourse = await _context.StuEnrollCourses.FindAsync(id);
            if (stuEnrollCourse != null)
            {
                _context.StuEnrollCourses.Remove(stuEnrollCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StuEnrollCourseExists(int id)
        {
            return (_context.StuEnrollCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult courseandprof()
        {
            return View();
        }
        [HttpPost]
        public IActionResult courseandprofSearch(string course, string prof)
        {
            var x = _context.TauCourses
                .Include(s => s.Course)
                .Include(s => s.PIdNavigation)
                .Where(e => e.PIdNavigation.PName == prof).Select(e => e).ToList();

            var obj = _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .Where(e => e.Course.Coursename == course).Select(e => e).ToList();



            if (x != null && obj != null)
                return View();

            return NotFound();
        }

        public IActionResult topperformersdept()
        {
            var obj = _context.Student1s
                 .Select(e => e.SDept).Distinct().ToList();
            ViewBag.list1 = obj;
            return View();
        }
        [HttpPost]
        public IActionResult topperformersdeptSearch(string dept)
        {


            var obj = _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .Where(x => x.SIdNavigation.SDept == dept)
                .OrderByDescending(x => x.Grade)
                .Take(5)
                .ToList();
            return View(obj);
        }

        public IActionResult topperformerscourse()
        {
            var obj = _context.Courses
                 .Select(e => e.Courseid).ToList();
            ViewBag.list = obj;
            return View();
        }
        [HttpPost]
        public IActionResult topperformerscourseSearch(int courseid)
        {
            var obj = _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .Where(x => x.Courseid == courseid)
                .OrderByDescending(x => x.Grade)
                .Take(5)
                .ToList();
            return View(obj);
        }

        public IActionResult studentsincourse()
        {


            ViewData["Pid"] = new SelectList(_context.Professors, "PId", "PName");
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Coursename");

            var obj = _context.Professors.Select(x => x.PId).ToList();
            ViewBag.Pidlist = obj;

            var obj2 = _context.Courses.Select(x => x.Courseid).ToList();
            ViewBag.Courseidlist = obj2;


            return View();
        }


        [HttpPost]
        public IActionResult studentsincourseSearch(int courseid, int prof)
        {


            var obj = _context.StuEnrollCourses
                .Include(s => s.Course)
                .Include(s => s.SIdNavigation)
                .Where(x => x.Courseid == courseid).
                ToList();

            var obj2 = _context.TauCourses
                .Include(s => s.Course)
                .Include(s => s.PIdNavigation)
                .Where(s => s.PIdNavigation.PId == prof)
                .Where(x => x.Courseid == courseid)
                .ToList();




            if (obj2.Count == 0 || obj.Count == 0)
            {
                return this.NotFound("No Such Records Found");
            }


            VM1 model = new VM1();
            model.Courseids = obj;
            model.Profs = obj2;

            return View(model);

        }


        public JsonResult GetProfs(string term)
        {
            List<string> profs;

            profs = _context.Professors.Select(x => x.PName).ToList();

            return Json(profs);
        }
    }
}
