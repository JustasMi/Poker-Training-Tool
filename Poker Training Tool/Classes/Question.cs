using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Training_Tool.Classes
{
    class Question
    {
        private string question;
        private List<string> answers;

        private Table.status question_type;

        public Question(string q, List<string> a, Table.status t)
        {
            question = q;
            answers = a;
            question_type = t;
        }

        public string getQuestion()
        {
            return question;
        }

        public List<string> getAnswers()
        {
            return answers;
        }

        public Table.status getType()
        {
            return question_type;
        }
    }
}
