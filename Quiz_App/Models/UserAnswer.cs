namespace Quiz_App.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnserId { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
