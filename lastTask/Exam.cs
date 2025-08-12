using lastTask;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lastTask;

namespace lastTask
{

    public class Exam
    {
        public DateTime ExamDate { get; set; }
        public int MarkOfExam { get; set; }  
        public int grade { get; set; }      

        public Exam(DateTime examDate, int markOfExam)
        {
            ExamDate = examDate;
            MarkOfExam = markOfExam;
            grade = 0;
        }

        public virtual void showData(QuestionList qList)
        {
            Console.WriteLine("This is the base Exam showData method.");
        }
    }





 public class PracticalExam : Exam
{
    public PracticalExam(DateTime examDate, int markOfExam) : base(examDate, markOfExam) { }

    public override void showData(QuestionList qList)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select mode:");
            Console.WriteLine("1 - Doctor mode");
            Console.WriteLine("2 - Student mode");
            Console.WriteLine("0 - Exit");
            string choice = Console.ReadLine();

            if (choice == "1")
                DoctorMode(qList);
            else if (choice == "2")
                StudentMode(qList);
            else if (choice == "0")
                break;
            else
            {
                Console.WriteLine("Invalid choice, please try again.");
                Console.ReadKey();
            }
        }
    }

        private void DoctorMode(QuestionList qList)
        {
            Console.Clear();
            Console.WriteLine("Doctor mode:");

            Console.Write("How many questions do you want to enter? ");
            if (!int.TryParse(Console.ReadLine(), out int numQuestions) || numQuestions <= 0)
            {
                Console.WriteLine("Invalid number.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select question type: 1 - True/False  2 - Essay");
            string typeChoice = Console.ReadLine();
            QuestionType type;
            string[] keywords = null;

            if (typeChoice == "1")
            {
                type = QuestionType.TrueFalse;
            }
            else if (typeChoice == "2")
            {
                type = QuestionType.Essay;
                Console.Write("Enter two keywords (separated by space): ");
                keywords = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (keywords.Length < 2)
                {
                    Console.WriteLine("Please enter at least two keywords.");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select question difficulty: 1 - Easy  2 - Hard");
            string levelChoice = Console.ReadLine();
            DifficultyLevel level = levelChoice == "1" ? DifficultyLevel.Easy : DifficultyLevel.Hard;

            for (int i = 0; i < numQuestions; i++)
            {
                Console.WriteLine($"Enter question text number {i + 1}:");
                string text = Console.ReadLine();

                // If essay, check keywords
                if (type == QuestionType.Essay)
                {
                    bool containsAll = keywords.All(kw => text.Contains(kw));
                    if (!containsAll)
                    {
                        Console.WriteLine("The question must contain both keywords. Please try again.");
                        i--;
                        continue;
                    }
                }

                Console.Write("Enter question grade: ");
                if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 0)
                {
                    Console.WriteLine("Invalid grade.");
                    i--;
                    continue;
                }

                Console.WriteLine("Enter the answer:");
                string answer = Console.ReadLine();

                var question = new Question(text, type, keywords ?? Array.Empty<string>(), level, grade, answer);
                qList.AddQuestion(question);

                Console.WriteLine("Question added successfully.\n");
            }

            Console.WriteLine("Finished adding questions. Press any key to return.");
            Console.ReadKey();
        }




        private void StudentMode(QuestionList qList)
        {
            Console.Clear();
            Console.WriteLine("Student mode:");

            if (qList.IsEmpty())
            {
                Console.WriteLine("No questions in the system. Please ask the doctor to add questions first.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select question difficulty: 1- Easy  2- Hard");
            string levelChoice = Console.ReadLine();
            DifficultyLevel level = levelChoice == "1" ? DifficultyLevel.Easy : DifficultyLevel.Hard;

            var questions = qList.GetQuestionsByLevel(level);

            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available at this difficulty level.");
            }
            else
            {
                Console.WriteLine($"Number of questions in {level} level: {questions.Count}");

                grade = 0;       
                MarkOfExam = 0; 

                foreach (var q in questions)
                {
                    MarkOfExam += q.Grade;  

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"Question: {q.Text}");
                    Console.WriteLine($"Type: {q.Type}");
                    Console.WriteLine($"Grade: {q.Grade}");

                    Console.WriteLine("Enter your answer:");
                    string studentAnswer = Console.ReadLine();

                    if (studentAnswer.Trim().Equals(q.Answer.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Correct answer!");
                        grade += q.Grade;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong answer. The correct answer is: {q.Answer}");
                    }
                }

                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Final grade: {grade} out of {MarkOfExam}");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

    }



}

