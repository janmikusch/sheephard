﻿/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

/*
 * The code in this file was adapted from: https://scottlilly.com/create-better-random-numbers-in-c/
 */
using System;
using System.Security.Cryptography;

namespace Sheephard
{
    public static class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider();

        public static int Between(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            Generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }
}