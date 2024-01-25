using System;

namespace Task5.CountWord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CountWord();
        }

        static void CountWord() {

            Console.Write("Escreva uma frase: ");
            string sentence = Console.ReadLine();
            
            string[] words = sentence.Split(' ');
            int wordsSum = words.Length;

            if(wordsSum > 1)
            {
                Console.WriteLine($"A frase digitada tem {wordsSum} palavras.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"A frase digitada tem {wordsSum} palavra.");
                Console.ReadLine();

            }                   

           
        }
    }
}
