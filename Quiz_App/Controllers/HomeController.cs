using Microsoft.AspNetCore.Mvc;
using Quiz_App.Data;

namespace Quiz_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quizzes = _context.Quizzes.ToList();

            return View(quizzes);
        }
    }

}
