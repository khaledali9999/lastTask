using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lastTask
{
    using System.Collections.Generic;
    using System.Linq;
    

    public class QuestionList
    {
        public List<Question> questions = new List<Question>();

        public void AddQuestion(Question q)
        {
            questions.Add(q);
        }

        public List<Question> GetQuestionsByLevel(DifficultyLevel level)
        {
            return questions.Where(q => q.Level == level).ToList();
        }

        public bool IsEmpty()
        {
            return questions.Count == 0;
        }
    }

}
