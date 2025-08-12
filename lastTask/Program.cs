namespace lastTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestionList questionList = new QuestionList();
            Console.WriteLine("--------start exam--------");
            Exam exam = new PracticalExam(DateTime.Now, 6);
            exam.showData(questionList);
            Console.WriteLine($"your grade is: {exam.grade} from {exam.MarkOfExam}");
        }
    }
}
