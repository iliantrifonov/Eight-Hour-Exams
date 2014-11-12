namespace GameLogic
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameLogic
    {
        private static char[] allowedChars = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static int CalculateRank(int wins, int losses)
        {
            return (wins * 100) + (losses * 15);
        }

        public static bool IsValidNumber(string number)
        {
            if (number.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < number.Length; i++)
            {
                if (!allowedChars.Contains(number[i]))
                {
                    return false;
                }

                for (int j = 0; j < number.Length; j++)
                {
                    if (number[i] == number [j] && i != j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static BullsAndCows GetCowAndBullCount(string number, string guess)
        {
            int bullsCount = 0;
            int cowsCount = 0;

            if (number.Length != 4 || guess.Length != 4)
            {
                throw new ArgumentException("The number or guess has invalid lenght");
            }

            foreach (var character in number)
            {
                if (!allowedChars.Contains(character))
                {
                    throw new ArgumentException("Symbol is not allowed");
                }
            }

            foreach (var character in guess)
            {
                if (!allowedChars.Contains(character))
                {
                    throw new ArgumentException("Symbol is not allowed");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                char curguess = guess[i];
                
                if (curguess == number[i])
                {
                    bullsCount++;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (curguess == number[j])
                        {
                            cowsCount++;
                        }
                    }
                }
            }

            return new BullsAndCows()
            {
                BullsCount = bullsCount,
                CowsCount = cowsCount
            };
        }
    }
}
