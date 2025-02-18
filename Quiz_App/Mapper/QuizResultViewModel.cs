using Quiz_App.Models;

namespace Quiz_App.Mapper
{
    public class QuizResultViewModel
    {
        public Quiz Quiz { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
        public int Score { get; set; }
    }

    public class StartQuizViewModel
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }

    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
    }

}
