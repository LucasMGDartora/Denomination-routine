using System;
using System.Collections.Generic;

namespace DenominationRoutine
{
    class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        static void Main()
        {
            #region Variables

            int[] denominations = { 10, 50, 100 };
            int[] payoutAmounts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            #endregion

            #region Calculate the possible combinations for the values

            /// <summary>
            /// calculate the possible combinations for the values
            /// </summary>
            foreach (int amount in payoutAmounts)
            {
                Console.WriteLine($"For {amount} EUR, the available payout denominations would be:");

                // Calls the function to calculate combinations
                CalculateCombinations(amount, denominations);

                Console.WriteLine();
            }

            #endregion

            Console.ReadLine();
        }

        /// <summary>
        /// This method is responsible for initiating the design of possible modifications for a given withdrawal amount
        /// </summary>
        /// <param name="amount">Amount you want to withdraw from the ATM.</param>
        /// <param name="denominations">Array that contains the denominations available at the ATM.</param>
        static void CalculateCombinations(int amount, int[] denominations)
        {
            // temporarily store the combinations.
            List<int> combination = new List<int>();

            Calculate(amount, denominations, 0, combination);
        }

        /// <summary>
        /// Recursive function that calculates the possible combinations for a given withdrawal amount.
        /// </summary>
        /// <param name="amount">Amount you want to withdraw from the ATM.</param>
        /// <param name="denominations">Array that contains the denominations available at the ATM.</param>
        /// <param name="index">Value of Index</param>
        /// <param name="combination">List of combinations</param>
        static void Calculate(int amount, int[] denominations, int index, List<int> combination)
        {
            #region Validation

            if (amount == 0)
            {
                PrintCombination(combination);
                return;
            }

            if (amount < 0 || index == denominations.Length)
            {
                return;
            }

            #endregion

            // Exclude current denomination
            Calculate(amount, denominations, index + 1, combination);

            // Include current denomination
            combination.Add(denominations[index]);
            Calculate(amount - denominations[index], denominations, index, combination);
            combination.RemoveAt(combination.Count - 1);
        }

        /// <summary>
        /// Prints a combination of denominations on the console.
        /// </summary>
        /// <param name="combination">List that contains all the combinations</param>
        static void PrintCombination(List<int> combination)
        {
            // Print a prefix for each combination
            Console.Write("· ");

            // Initialize the variable to store the sum of notes
            int sum = 0;

            // Initialize the variable to count the number of equal notes
            int count = 0;

            // Traverse through the combination list
            for (int i = 0; i < combination.Count; i++)
            {
                // Add the value of the current note to the total sum
                sum += combination[i];

                // Increment the count of equal notes
                count++;

                // Check if the current note is different from the next one or if we've reached the end of the list
                if (i == combination.Count - 1 || combination[i] != combination[i + 1])
                {
                    // Print the count of consecutive notes and the value of the note
                    Console.Write($"{count} x {combination[i]} EUR ");

                    // Check if the sum of notes is different from the total value calculated by the notes and their count
                    if (sum != combination[i] * count)
                    {
                        // If the sum is different, print the total value
                        Console.Write($"= {sum} EUR");
                    }

                    // Add a plus sign (+) between different combinations
                    if (i < combination.Count - 1)
                    {
                        Console.Write(" + ");
                    }

                    // Reset the sum and count for the next combination
                    sum = 0;
                    count = 0;
                }
            }

            // Print a new line at the end
            Console.WriteLine();
        }
    }
}