using System;
using System.Collections.Generic;
using System.Text;

namespace VendingChangeMaker
{
    class Change
    {
        public static string GetChangeMessage()
        {
            int changeAmount = GetChangeAmount();
            string changeMessage = "";
            if (changeAmount == 0)
                changeMessage = "\nYou have made exact change.  Thank you.";
            else
            {
                int[] chgArray = GetChangeDenominations(changeAmount);
                int changeDollars = changeAmount / 100;
                string dollarsString = changeDollars.ToString();
                int changeCents = changeAmount % 100;
                string centsString = "";
                if (changeCents < 10)
                    centsString = $"0{changeCents}";
                else
                    centsString = $"{changeCents}";
                string changeAmountString = $"{changeDollars}.{centsString}";
                changeMessage = $"\nYour change of ${changeAmountString} is being paid as follows:\n" +
                    $"{chgArray[0]} quarter(s), {chgArray[1]} dime(s), {chgArray[2]} nickel(s), and " +
                    $"{chgArray[3]} pennies";
            }

            return changeMessage;
        }

        public static int GetChangeAmount()
        {
            UserInterface.DisplayPrompt("\nItem price dollars? ");
            int dollars = UserInterface.GetInteger();
            int price = dollars * 100;
            UserInterface.DisplayPrompt("Item price cents? ");
            int cents = UserInterface.GetInteger();
            price += cents;

            UserInterface.DisplayPrompt("Payment dollars? ");
            dollars = UserInterface.GetInteger();
            int amountPaid = dollars * 100;
            UserInterface.DisplayPrompt("Payment cents? ");
            cents = UserInterface.GetInteger();
            amountPaid += cents;

            int changeAmount;
            if (amountPaid == price)
                changeAmount = 0;
            else
            {
                if (amountPaid < price)
                {
                    UserInterface.DisplayMessage("Insuffient payment - returning funds");
                    changeAmount = amountPaid;
                }
                else
                {
                    changeAmount = amountPaid - price;
                }
            }
            return changeAmount;
        }

        public static int[] GetChangeDenominations(int changeAmount)
        {
            int[] chgArray = new int[4];  // element 0=.25, 1=.10, 2=.05, 2=.01

            int result = changeAmount / 100;
            if (result > 0)
                chgArray[0] += result * 4;
            changeAmount = changeAmount % 100;
            result = changeAmount / 25;
            if (result > 0)
                chgArray[0] += result;
            changeAmount = changeAmount % 25;
            result = changeAmount / 10;
            if (result > 0)
                chgArray[1] += result;
            changeAmount = changeAmount % 10;
            result = changeAmount / 5;
            if (result > 0)
                chgArray[2] += result;
            changeAmount = changeAmount % 5;
            if (changeAmount > 0)
                chgArray[3] += changeAmount;

            return chgArray;
        }
    }
}
