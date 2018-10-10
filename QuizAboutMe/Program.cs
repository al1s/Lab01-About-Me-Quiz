using System;
using System.Text.RegularExpressions;

namespace QuizAboutMe
{
    class Program
    {
        static void Main(string[] args)
        {
            QuizEngine();
        }

        // Quiz questions with correct answers and expected answer type.
        // Format: 
        //      required string Question
        //      required string CheckType
        //      string Correct answer (it's optional for "Range" questions)
        static string[][] quizContent = new string[][]
        {
            new string[] {"Do you believe my full name is Aleksandr Vladimirovich?", "Boolean", "true" },
            new string[] {"I like motocycles. And all of those I had were - Harley-Davidson. Do you believe it?", "Boolean", "false"},
            new string[] {"I went to Tibet. And climbed Everest - the North Face of it.", "Boolean", "false"},
            new string[] { "What country besides the USA and Russia I\'ve been?", "String", "Mongolia, Spain, China, Italy, France, Switzerland, Netherlands"},
            new string[] { "What is the meaning of life if it\'s filled with surprises?", "Range" }
        };

        // Quiz manager
        static void QuizEngine()
        {
            int counter = default(int);
            foreach (string[] quizElm in quizContent)
            {
                string userInput = GetUserAnswerTo(quizElm[0], quizElm[1]);
                counter += UserInputHandler(quizElm, userInput);
            }
            Console.WriteLine($"Correct answers: {counter} of {quizContent.Length}");
            Console.ReadLine();
        }

        // Filter user input for anything but letters and digits
        static string Sanitize(string input)
        {
            return input;
        }

        // Prompt user with a question and an expected answer category
        static string GetUserAnswerTo(string question, string questionCategory)
        {
            string acceptableUserInput = string.Empty;
            Console.WriteLine(question);
            switch (questionCategory)
            {
                case "Boolean":
                    acceptableUserInput = "Expected [Y]es/[N]o, default - Yes: ";
                    break;
                case "String":
                    acceptableUserInput = "Expected string: ";
                    break;
                case "Range":
                    acceptableUserInput = "Expected number: ";
                    break;
            }
            Console.Write(acceptableUserInput);
            string userInput = Console.ReadLine();
            userInput = Sanitize(userInput);
            return userInput;
        }

        // Convert user input into boolean
        static bool ConvertToBoolean(string str)
        {
            Regex regex = new Regex(@"^y", RegexOptions.IgnoreCase);
            bool value = regex.IsMatch(str == string.Empty ? "y" : str);
            return value;
        } 

        static int ConvertToInt(string str)
        {
            int result = default(int);
            try
            {
                result = Int32.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine("Numeric answer expected");
            }
            return result;
        }

        static int UserInputHandler(string[] quizElm, string userInput)
        {
            int result = default(int);
            switch (quizElm[1])
            {
                case "Boolean":
                    result = HandleBooleanAnswer(userInput, Convert.ToBoolean(quizElm[2]));
                    break;
                case "String":
                    result = HandleStringAnswer(userInput, quizElm[2]);
                    break;
                case "Range":
                    result = HandleRangeAnswer(userInput);
                    break;
            };
            if (result == 1) Console.WriteLine("Correct answer!");
            else Console.WriteLine("The answer is incorrect!");
            return result;
        }
        // Check whether the user answer matches with a correct answer
        static int HandleBooleanAnswer(string userAnswer, bool correctAnswer)
        {
            return ConvertToBoolean(userAnswer) == correctAnswer ? 1 : 0;
        }

        // Check whether the user answer matches with a correct answer
        static int HandleStringAnswer(string userAnswer, string correctAnswer)
        {
            Regex regex = new Regex($@".*{userAnswer}.*", RegexOptions.IgnoreCase);
            bool value = regex.IsMatch(correctAnswer);
            return value ? 1 : 0;
        }

        // Check whether the user answer matches with a correct answer
        static int HandleRangeAnswer(string userAnswer)
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(42 - 2, 42 + 3);
            return ConvertToInt(userAnswer) == rndNumber ? 1 : 0;
        }
    }
}
