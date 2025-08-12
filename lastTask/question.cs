using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lastTask
{
    
        public enum QuestionType
        {
            TrueFalse,
            Essay
        }

        public enum DifficultyLevel
        {
            Easy,
            Hard
        }

        public class Question
        {
            public string Text { get; set; }
            public QuestionType Type { get; set; }
            public string[] Keywords { get; set; }
            public DifficultyLevel Level { get; set; }
            public int Grade { get; set; }
            public string Answer { get; set; }

            public Question(string text, QuestionType type, string[] keywords, DifficultyLevel level, int grade, string answer)
            {
                Text = text;
                Type = type;
                Keywords = keywords;
                Level = level;
                Grade = grade;
                Answer = answer;
            }
        }

    }

