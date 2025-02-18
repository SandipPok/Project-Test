using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_App.Data;
using Quiz_App.Mapper;
using Quiz_App.Models;
using System.Diagnostics;

namespace Quiz_App.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private readonly AppDbContext _context;

        public QuizController(ILogger<QuizController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> StartQuiz(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            // Map the Quiz and its related Questions and Answers to the ViewModel
            var viewModel = new StartQuizViewModel
            {
                QuizId = quiz.Id,
                QuizTitle = quiz.Title,
                Questions = quiz.Questions.Select(q => new QuestionViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    Answers = q.Answers.Select(a => new AnswerViewModel
                    {
                        AnswerId = a.Id,
                        AnswerText = a.Text
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuiz(int quizId, List<int> answers)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
            {
                return NotFound();
            }

            // Loop through the questions and track user answers
            var userAnswers = new List<UserAnswer>();
            var score = 0;

            for (int i = 0; i < quiz.Questions.Count; i++)
            {
                var question = quiz.Questions.ElementAt(i);
                var selectedAnswerId = answers[i];
                var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == selectedAnswerId);
                var isCorrect = selectedAnswer != null && selectedAnswer.IsCorrect;

                userAnswers.Add(new UserAnswer
                {
                    UserId = 1, // Example: assuming user 1 is submitting
                    QuestionId = question.Id,
                    AnswerId = selectedAnswerId,
                    IsCorrect = isCorrect
                });

                if (isCorrect) score++;
            }

            // Save user answers (Optional: track the user's score in the database)
            _context.UserAnswers.AddRange(userAnswers);
            await _context.SaveChangesAsync();

            // After submission, show the results with the facts
            var result = new QuizResultViewModel
            {
                Quiz = quiz,
                UserAnswers = userAnswers,
                Score = score
            };

            return View("QuizResult", result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
