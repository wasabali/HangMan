using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class Program
    {
        static string correctWord;
        static char[] letters;
        static Player player;

        static void Main(string[] args)
        {
            try
            {
                StartGame();
                PlayGame();
                EndGame();
            }
            catch
            {
                Console.WriteLine("Opps, something wnet wrong...");
            }
        }


        private static void StartGame()
        {
            string[] words;
            try
            {
                words = File.ReadAllLines(@"/Users/wasabali/Desktop/words.txt.rtf");
            }
            catch
            {
                words = new string[] { "tree", "dog", "cat" , "house" , "game"};
            }

            Random random = new Random();
            correctWord = words[random.Next(0, words.Length)];

            letters = new char[correctWord.Length];
            for (int i = 0; i < correctWord.Length; i++)
                letters[i] = '-';
            
            AskForUserName();
        }

        static void AskForUserName()
        {
            Console.WriteLine("Enter your name:");
            string input = Console.ReadLine();
            if (input.Length >= 2)
                player = new Player(input);
             else
            {
                AskForUserName();
            }
        }

        private static void PlayGame()
        {
            do
            {
                Console.Clear();
                DisplayMaskedWord();
                char guessedLetter = AskForLetter();
                checkLetter(guessedLetter);
            } while (correctWord != new string(letters));

            Console.Clear();
        }

        private static void checkLetter(char guessedLetter)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (guessedLetter == correctWord[i])
                {
                    letters[i] = guessedLetter;
                    player.Score++;
                }
            }

        }

        static void DisplayMaskedWord()
        {
            foreach (char c in letters)
                Console.Write(c);
            
            Console.WriteLine();
        }

        static char AskForLetter()
        {
            string input;
            do
            {
                Console.WriteLine("Enter a letter:");
                input = Console.ReadLine();
            } while (input.Length != 1);

            var letter = input[0];

            if (!player.GuessedLetters.Contains(letter))
                player.GuessedLetters.Add(letter);


            return letter;
        }
        
        private static void EndGame()
        {
            Console.WriteLine("Congrats!");
            Console.WriteLine($"Thanks for playing {player.Name}");
            Console.WriteLine($"Guesses: {player.GuessedLetters.Count} Score: {player.Score}");
        }
    }
}
