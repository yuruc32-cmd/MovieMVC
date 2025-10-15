using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1121754.Data;
using _1121754.Models;

using X.PagedList;
using X.PagedList.Extensions;
using System.Net.Sockets;
using System.Net;


namespace _1121754.Controllers
{
    public class DBMemberController : Controller
    {
        private readonly CmsContext _context;
        public DBMemberController(CmsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                // 非管理員不允許進入，導回登入頁或其他頁
                TempData["Error"] = "您沒有權限進入此頁面，請先以管理員身份登入。";
                return RedirectToAction("First"); // 或其他你想導向的頁面
            }

            var users = await _context.TableMovies1121754.OrderBy(m => m.BookingId).ToListAsync();
            return View(users);


        }
        public async Task<IActionResult> First()
        {
            return View();


        }
        // POST: /Home/Login
        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _context.TableLogins1121754.FirstOrDefault(m => m.Name == name && m.Password == password);

            if (user != null)
            {
                if (name == "admin" && password == "admin123")
                {
                    HttpContext.Session.SetString("Role", "Admin");  // 設定管理員身份
                    HttpContext.Session.SetString("UserName", name);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    return RedirectToAction("Index");
                }

                // 一般使用者登入
                HttpContext.Session.SetString("Role", "User");
                HttpContext.Session.SetString("UserName", name);
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("IndexPage");
            }

            ViewBag.Error = "登入失敗";
            return View("First");
        }
        [HttpPost]
        public IActionResult Register(string name, string password)
        {
            // 檢查是否已有相同帳號
            var exists = _context.TableLogins1121754.Any(m => m.Name == name);
            if (exists)
            {
                ViewBag.Error = "帳號已存在，請使用其他帳號。";
                return View("First");
            }

            // 建立新資料
            var newMember = new Login
            {
                Name = name,
                Password = password
            };

            _context.TableLogins1121754.Add(newMember);
            _context.SaveChanges();
            TempData["Success"] = "註冊成功！請使用帳號密碼登入。";
            // 註冊成功，回到登入頁
            return RedirectToAction("First");
        }

        [HttpGet]
        public IActionResult Booking(int id) // id 是電影的 Id（TableLists1121754 的 Movie_Id）
        {
            var movie = _context.TableLists1121754.FirstOrDefault(m => m.Movie_Id == id);
            if (movie == null)
                return NotFound();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("First");

            ViewBag.Movie = movie;
            ViewBag.UserId = userId;
            return View();
        }


        [HttpPost]
        public IActionResult Booking(int userId, string movie, string time, int ticket)
        {
            var user = _context.TableLogins1121754.FirstOrDefault(x => x.Id == userId);

            var newBooking = new Members
            {
                Id = userId,
                Movie = movie,
                Time = time,
                Ticket = ticket,
                Name = user.Name,
                Password = user.Password,
            };

            _context.TableMovies1121754.Add(newBooking);
            _context.SaveChanges();

            return RedirectToAction("IndexPage");
        }

        public IActionResult MyBooking()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("First");

            var bookings = _context.TableMovies1121754
                .Where(b => b.Id == userId)
                .ToList();

            return View(bookings); // 傳入 List<Members>
        }



        public async Task<IActionResult> IndexPage(int? page = 1)
        {
            // 檢查 Session 是否有 UserId（登入時你會設定這個）
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["LoginRequired"] = "請先登入！";
                return RedirectToAction("First", "DBMember");
            }
            const int pageSize = 4;
            ViewBag.usersModel = GetPagedProcess(page, pageSize);
            return View(await _context.TableLists1121754.Skip<Movie>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToListAsync());
        }
        protected IPagedList<Movie> GetPagedProcess(int? page, int pageSize)
        {
            if (page.HasValue && page < 1)
            {
                return null;
            }
            var listUnpaged = GetStuffFromDatebase();
            IPagedList<Movie> pagelist = listUnpaged.ToPagedList(page ?? 1, pageSize);
            if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
            {
                return null;
            }
            return pagelist;
        }
        protected IQueryable<Movie> GetStuffFromDatebase()
        {
            return _context.TableLists1121754;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.TableMovies1121754.FindAsync(id);
            if (booking != null)
            {
                _context.TableMovies1121754.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyBooking");
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TableMovies1121754 == null)
            {
                var msgObject = new
                {
                    statuscode = StatusCodes.Status400BadRequest,
                    error = "無效的請求，必須提供Id編號!",
                    url = "The url example : /Employees/Details/5"
                };
                return new BadRequestObjectResult(msgObject);
            }

            var member = await _context.TableMovies1121754.FirstOrDefaultAsync(m => m.BookingId == id);

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        //Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Movie,Time,Ticket,Password")] Members member)
        {
            if (ModelState.IsValid)
            {
                _context.TableMovies1121754.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        // GET: DBMember/Edit/5
        // GET Edit 改用 id 參數名稱
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var member = await _context.TableMovies1121754.FindAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        // POST Edit 也用 id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,Name,Movie,Time,Ticket,Password")] Members members)
        {
            if (id != members.BookingId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // 不修改 Id 欄位，只更新其它欄位
                    var existing = await _context.TableMovies1121754.FindAsync(id);
                    if (existing == null) return NotFound();

                    existing.Name = members.Name;
                    existing.Movie = members.Movie;
                    existing.Time = members.Time;
                    existing.Ticket = members.Ticket;
                    existing.Password = members.Password;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.BookingId))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }


        private bool MembersExists(int bookingId)
        {
            return _context.TableMovies1121754.Any(e => e.BookingId == bookingId);
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TableMovies1121754 == null)
            {
                return NotFound();
            }
            var employee = await _context.TableMovies1121754.FirstOrDefaultAsync(m => m.BookingId == id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);


        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TableMovies1121754 == null)
            {
                return Problem("Emtity set 'CmsContext.Employees' is null.");
            }
            var employee = await _context.TableMovies1121754.FindAsync(id);
            if (employee != null)
            {
                _context.TableMovies1121754.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SelectQuery()
        {
            var names = await (from p in _context.TableMovies1121754
                               orderby p.Movie
                               select p.Movie).Distinct().ToListAsync();
            ViewBag.Mylist = names;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SelectName(string Movie)
        {
            var users = await (from p in _context.TableMovies1121754
                               where p.Movie == Movie
                               orderby p.Movie
                               select p).Distinct().ToListAsync();
            return View(users);
        }
    }
}
