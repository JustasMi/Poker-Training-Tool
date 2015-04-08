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

        public Question(string q, List<string> a)
        {
            question = q;
            answers = a;
        }

        public string getQuestion()
        {
            return question;
        }

        public List<string> getAnswers()
        {
            return answers;
        }
    }
}
