using System;

namespace QuizAboutMe
{
    class Program
    {
        static string[][] quizContent = new string[][]
        {
            new string[] {"Do you believe my full name is Aleksandr Vladimirovich?", "Boolean", "true" },
            new string[] {"I like motocycles. And all of those I had were - Harley-Davidson. Do you believe it?", "Boolean", "false"},
            new string[] {"I went to Tibet. And climbed Everest - the North Face of it.", "Boolean", "false"},
            new string[] { "What country besides the USA and Russia I\'ve been?", "String", "Mongolia, Spain, China, Italy, France, Switzerland, Netherlands"},
            new string[] { "What is the meaning of life if it\'s filled with surprises?", "Range" }
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
