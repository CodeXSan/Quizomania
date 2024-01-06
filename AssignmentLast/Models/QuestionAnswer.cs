namespace AssignmentLast.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int QuizSessionId { get; set; }
        public int IsCorrect { get; set; }
        public Questions Question { get; set; }
        public QuizSession QuizSession { get; set; }

    }
}
