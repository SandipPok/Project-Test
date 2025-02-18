﻿namespace Quiz_App.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Fact { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public int CorrectAnswerId { get; set; }
    }
}
