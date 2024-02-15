using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
        private List<string> wordList = new List<string>
        {
            "tiger",
            "tree",
            "underground",
            "giraffe",
            "Simbone",
            "Ethan"
            // Add more words as needed
        };

        private string chosenWord = "";
        private string guessedWord = "";
        private int maxAttempts = 8; // Maximum attempts allowed (including additional attempts)
        private int remainingAttempts;
        private string guessedLetters = "";

        public HangmanGamePage()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            chosenWord = SelectRandomWord();
            guessedWord = HideLetters(chosenWord);
            remainingAttempts = maxAttempts;

            WordLabel.Text = guessedWord;
            HangmanImage.Source = "hang1.png"; // Set initial hangman image
        }

        private string SelectRandomWord()
        {
            return wordList[new Random().Next(wordList.Count)];
        }

        private string HideLetters(string word)
        {
            return new string('_', word.Length);
        }

        private void OnGuessClicked(object sender, EventArgs e)
        {
            if (GuessEntry.Text.Length == 0)
            {
                // Handle empty guess entry
                return;
            }

            char guess = GuessEntry.Text.ToLower()[0];

            if (guessedLetters.Contains(guess))
            {
                // Letter already guessed
                return;
            }

            guessedLetters += guess;

            if (chosenWord.Contains(guess))
            {
                // Correct guess
                char[] guessedWordArray = guessedWord.ToCharArray();
                for (int i = 0; i < chosenWord.Length; i++)
                {
                    if (chosenWord[i] == guess)
                    {
                        guessedWordArray[i] = guess;
                    }
                }
                guessedWord = new string(guessedWordArray);
                WordLabel.Text = guessedWord;
            }
            else
            {
                // Incorrect guess
                remainingAttempts--;
                int hangmanImageNumber = maxAttempts - remainingAttempts;
                HangmanImage.Source = $"hang{hangmanImageNumber}.png"; // Update hangman image
            }

            if (guessedWord == chosenWord)
            {
                // Player wins
                DisplayAlert("Congratulations!", $"You guessed the word: {chosenWord}", "OK");
                StartNewGame();
            }
            else if (remainingAttempts == 0)
            {
                // Player loses
                DisplayAlert("Game Over", $"The word was: {chosenWord}", "OK");
                StartNewGame();
            }

            GuessEntry.Text = ""; // Clear the input field
        }
    }
}