using AssignmentLast.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentLast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext _context;
        //public static int QuizId=0;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //LoadQuestion();
            return View();
        }
        public IActionResult LoadQuestion()
        {
            List<Questions> question = new List<Questions> {
            new Questions
            {

                Question="What does CPU stand for?",
                QuestionOne="Central Processing Unit",
                QuestionTwo="Computer Peripheral Unit",
                QuestionThree="Central Peripheral Unit",
                QuestionFour="Central Process Unit",
                Result='A'
            },
            new Questions
            {

                Question="Which data structure follows the Last In, First Out (LIFO) principle?",
                QuestionOne="Queue",
                QuestionTwo="Stack",
                QuestionThree="Linked List",
                QuestionFour="Tree",
                Result='B'
            },
            new Questions
            {

                Question="What does the acronym URL stand for in the context of web addresses?",
                QuestionOne="Uniform Resource Locator",
                QuestionTwo="Universal Reference Locator",
                QuestionThree="Unified Resource Locator",
                QuestionFour="Ultimate Reference Locator",
                Result='A'
            },
            new Questions
            {

                Question="Which programming language is known for its simplicity and readability?",
                QuestionOne="Java",
                QuestionTwo="C++",
                QuestionThree="Python",
                QuestionFour="Ruby",
                Result='C'
            },
            new Questions
            {

                Question="What is the purpose of a firewall in network security?",
                QuestionOne="To filter and block malicious traffic",
                QuestionTwo="To enhance internet speed",
                QuestionThree="To store sensitive data",
                QuestionFour="To manage software updates",
                Result='A'
            },
            new Questions
            {

                Question="Which programming paradigm emphasizes reusable code and modular design?",
                QuestionOne="Object-Oriented Programming (OOP)",
                QuestionTwo="Procedural Programming",
                QuestionThree="Functional Programming",
                QuestionFour="Logical Programming",
                Result='A'
            },
            new Questions
            {

                Question="What does HTML stand for in web development?",
                QuestionOne="Hyperlink Text Markup Language",
                QuestionTwo="HyperText Markup Language",
                QuestionThree="High-level Text Management Language",
                QuestionFour="Hyper Transferable Markup Language",
                Result='B'
            },
            new Questions
            {

                Question="What is the primary function of an operating system?",
                QuestionOne="Running applications",
                QuestionTwo="Managing hardware resources",
                QuestionThree="Storing data",
                QuestionFour="Designing user interfaces",
                Result='B'
            },
            new Questions
            {

                Question="In database terminology, what does SQL stand for?",
                QuestionOne="Structured Query Language",
                QuestionTwo="Simple Question Language",
                QuestionThree="System Query Language",
                QuestionFour="Sequential Question Language",
                Result='A'
            },
            new Questions
            {

                Question="What is the purpose of the BIOS in a computer system?",
                QuestionOne="To load the operating system",
                QuestionTwo="To control the display settings",
                QuestionThree="To manage network connections",
                QuestionFour="To run applications",
                Result='A'
            },
            new Questions
            {

                Question="Which type of malware disguises itself as legitimate software?",
                QuestionOne="Virus",
                QuestionTwo="Worm",
                QuestionThree="Trojan Horse",
                QuestionFour="Spyware",
                Result='C'
            },
            new Questions
            {

                Question="What is the role of a router in a computer network?",
                QuestionOne="Connects devices within a local network",
                QuestionTwo="Filters and forwards data between networks",
                QuestionThree="Manages user authentication",
                QuestionFour="Stores and retrieves website content",
                Result='B'
            },
            new Questions
            {

                Question="What is the purpose of version control systems like Git?",
                QuestionOne="Manage and track changes in software code",
                QuestionTwo="Control access to the internet",
                QuestionThree="Monitor system performance",
                QuestionFour="Organize file storage",
                Result='A'
            },
            new Questions
            {

                Question="What does the term \"cloud computing\" refer to?",
                QuestionOne="Storing data on physical servers",
                QuestionTwo="Accessing and using computing resources over the internet",
                QuestionThree="Running programs locally",
                QuestionFour="Creating virtual reality environments",
                Result='B'
            },
            new Questions
            {

                Question="Which protocol is commonly used for secure data transmission over the internet?",
                QuestionOne="HTTP",
                QuestionTwo="FTP",
                QuestionThree="HTTPS",
                QuestionFour="SMTP",
                Result='C'
            },
            new Questions
            {

                Question="What is the purpose of a DNS (Domain Name System) in networking?",
                QuestionOne="Translates domain names to IP addresses",
                QuestionTwo="Encrypts data transmissions",
                QuestionThree="Manages file storage",
                QuestionFour="Controls access to websites",
                Result='A'
            },
            new Questions
            {

                Question="In programming, what is a \"bug\"?",
                QuestionOne="A type of virus",
                QuestionTwo="An unintended error in code",
                QuestionThree="A hardware malfunction",
                QuestionFour="A programming language",
                Result='B'
            },
            new Questions
            {

                Question="What does the acronym LAN stand for in computer networking?",
                QuestionOne="Local Access Network",
                QuestionTwo="Large Area Network",
                QuestionThree="Local Area Network",
                QuestionFour="Longitudinal Access Network",
                Result='C'
            },
            new Questions
            {

                Question="Which data type is commonly used to store whole numbers in programming?",
                QuestionOne="Float",
                QuestionTwo="Integer",
                QuestionThree="String",
                QuestionFour="Boolean",
                Result='B'
            },
            new Questions
            {

                Question="What is the purpose of a compiler in the context of programming languages?",
                QuestionOne="Executes program instructions",
                QuestionTwo="Translates source code into machine code",
                QuestionThree="Manages database queries",
                QuestionFour="Controls user interfaces",
                Result='B'
            } };
            _context.Questions.AddRange(question);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult PostIndex(string name)
        {

            var quizSession = new QuizSession()
            {
                QuizDate = DateTime.Now,
                Score = 0,
                Name=name
            };
            _context.QuizSessions.Add(quizSession);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("QuizId", quizSession.Id);
            return RedirectToAction("InfoBox");
        }
       public IActionResult InfoBox()
        {
            return View();
        }
        public IActionResult Questions()
        {
            var quizId = HttpContext.Session.GetInt32("QuizId");
            var quizCount = _context.QuestionAnswers.Where(x=>x.QuizSessionId== quizId && x.IsCorrect!=-1).ToList();
            var questionIds = quizCount.Select(x=>x.QuestionId).ToList();
            var questionCount = _context.QuestionAnswers.Where(x => x.QuizSessionId == quizId).Count();
            if(questionCount>=1)
            {
                ViewData["PrevQuestion"] = true;
            }
            else
            {
                ViewData["PrevQuestion"] = false;
            }
            ViewData["Next"] = false;
            if (quizCount.Count()<10)
            {
                
                var questions = _context.Questions.Where(x => !questionIds.Contains(x.Id)).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var questionAnswers = new QuestionAnswer()
                {
                    QuestionId = questions.Id,
                    QuizSessionId = (int)quizId,
                    IsCorrect = -1
                };
                ViewData["CurrentQuestionIndex"] = quizCount.Count() + 1;
               
                _context.QuestionAnswers.Add(questionAnswers);
                _context.SaveChanges();
                ViewData["QuestionId"] = questionAnswers.Id;

                return View("Questions", questions);
            }
            else if(quizCount.Count()==10)
            {
                return RedirectToAction("Score");
            }
            else
            {
                return RedirectToAction("../");
            }
        }
        public IActionResult SubmitAnswer(int id, char option, int QuestionAnswerId)
        {
            var quizId = HttpContext.Session.GetInt32("QuizId");
            var question = _context.Questions.Where(x=>x.Id==id).FirstOrDefault();
            var quizSession = _context.QuizSessions.Where(x => x.Id == quizId).FirstOrDefault();
            var checkForData = _context.QuestionAnswers.Where(x => x.QuestionId == id && x.QuizSessionId == quizId).FirstOrDefault();
            if (question.Result==option)
            {
                if (checkForData.IsCorrect != -1)
                {
                    if (checkForData.IsCorrect == 0)
                    {
                        quizSession.Score = quizSession.Score + 1;
                        _context.QuizSessions.Update(quizSession);
                        _context.SaveChanges();
                    }
                    checkForData.IsCorrect = 1;
                    _context.QuestionAnswers.Update(checkForData);
                    _context.SaveChanges();
                    
                    var prevQuestionId = _context.QuestionAnswers.Where(x => x.Id > QuestionAnswerId).Take(1).FirstOrDefault();
                    if(prevQuestionId!=null)
                    {
                        var ques = _context.Questions.Where(x => x.Id == prevQuestionId.QuestionId).FirstOrDefault();
                        ViewData["QuestionId"] = prevQuestionId.Id;
                        ViewData["prevQuestion"] = true;
                        if(prevQuestionId.IsCorrect!=-1)
                            ViewData["Next"] = true;
                        else
                            ViewData["Next"] = false;
                        return View("Questions", ques);
                    }
                }
                else
                {
                    quizSession.Score = quizSession.Score + 1;
                    _context.QuizSessions.Update(quizSession);
                    _context.SaveChanges();
                    var qA = _context.QuestionAnswers.Where(x => x.Id == QuestionAnswerId).FirstOrDefault();
                    qA.IsCorrect = 1;
                    _context.QuestionAnswers.Update(qA);
                    _context.SaveChanges();
                    var prevQuestionId = _context.QuestionAnswers.Where(x => x.Id > QuestionAnswerId).Take(1).FirstOrDefault();
                    if (prevQuestionId != null)
                    {
                        var ques = _context.Questions.Where(x => x.Id == prevQuestionId.QuestionId).FirstOrDefault();
                        ViewData["QuestionId"] = prevQuestionId.Id;
                        ViewData["prevQuestion"] = true;
                        if (prevQuestionId.IsCorrect != -1)
                            ViewData["Next"] = true;
                        else
                            ViewData["Next"] = false;
                        return View("Questions", ques);
                    }
                }
            }
            else
            {
                var qA = _context.QuestionAnswers.Where(x => x.Id == QuestionAnswerId).FirstOrDefault();
                qA.IsCorrect = 0;
                _context.QuestionAnswers.Update(qA);
                _context.SaveChanges();
                var prevQuestionId = _context.QuestionAnswers.Where(x => x.Id > QuestionAnswerId).Take(1).FirstOrDefault();
                if (prevQuestionId != null)
                {
                    var ques = _context.Questions.Where(x => x.Id == prevQuestionId.QuestionId).FirstOrDefault();
                    ViewData["QuestionId"] = prevQuestionId.Id;
                    ViewData["prevQuestion"] = true;
                    if (prevQuestionId.IsCorrect != -1)
                        ViewData["Next"] = true;
                    else
                        ViewData["Next"] = false;
                    return View("Questions", ques);
                }
            }
            
            return RedirectToAction("Questions");
        }
        public IActionResult PreviousQuestion(int qaId)
        {
            var quizId = HttpContext.Session.GetInt32("QuizId");
            var prevQuestions = _context.QuestionAnswers.Where(x =>x.QuizSessionId==quizId && x.Id < qaId).ToList();
            var prevQuestionId = prevQuestions.Select(x => x.QuestionId).LastOrDefault();
            var question = _context.Questions.Where(x => x.Id == prevQuestionId).FirstOrDefault();
            ViewData["QuestionId"] = prevQuestions.Select(x => x.Id).LastOrDefault();
            ViewData["Next"] = true;
            if (prevQuestions.Count()>1)
                ViewData["PrevQuestion"] = true;
            else
                ViewData["PrevQuestion"] = false;
            return View("Questions",question);
        }
        public IActionResult NextQuestion(int qaId)
        {
            var quizId = HttpContext.Session.GetInt32("QuizId");
            var nextQuestions = _context.QuestionAnswers.Where(x=>x.QuizSessionId==quizId && x.Id > qaId).ToList();
            var nextQuestionId = nextQuestions.Select(x => x.QuestionId).FirstOrDefault();
            var question = _context.Questions.Where(x => x.Id == nextQuestionId).FirstOrDefault();
            ViewData["QuestionId"] = nextQuestions.Select(x => x.Id).FirstOrDefault();
            ViewData["PrevQuestion"] = true;
            if (nextQuestions.Select(x=>x.IsCorrect).FirstOrDefault()==-1)
            {
                ViewData["Next"] = false;
            }
            else
            {
                ViewData["Next"] = true;
            }
            return View("Questions", question);
        }
        public IActionResult Score()
        {
            var quizId = HttpContext.Session.GetInt32("QuizId");
            var questionScore = _context.QuizSessions.Where(x=>x.Id== quizId).FirstOrDefault();
            ViewData["Score"] = questionScore.Score;
            return View("Score");
        }
        //public IActionResult QuestionPage()
        //{

        //}
        public IActionResult History()
        {
            var historyData = _context.QuizSessions.OrderByDescending(x=>x.Id).ToList();
            return View(historyData);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
