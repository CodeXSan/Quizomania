using Microsoft.EntityFrameworkCore;

namespace AssignmentLast.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<QuizSession> QuizSessions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
