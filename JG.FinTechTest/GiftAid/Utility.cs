using System;

namespace JG.FinTechTest.GiftAid
{
    public static class Utility
    {
        const decimal Percentage = 100.0M;

        public static decimal GiftAidCalculation(this decimal donationAmount, decimal taxRate) => 
            donationAmount * (taxRate / (Percentage - taxRate));

        public static decimal Round2(this decimal amount) =>
            Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
}