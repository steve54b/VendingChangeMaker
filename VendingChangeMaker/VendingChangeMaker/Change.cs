using System;
using System.Collections.Generic;
using System.Text;

namespace VendingChangeMaker
{
    class Change
    {

        public static string GetChangeMessage()
        {
            // GetChangeMessage: first calls GetChangeAmount, to determine the change amount,
            // then calls GetChangeDemonimations, to determine the coin distrubution by
            // denomination, and, lastly, constructs the message to be displayed to the user
            //
            int changeAmount = GetChangeAmount(); // request price & payment and return difference
            string changeMessage = "";
            if (changeAmount == 0)
                changeMessage = "\nYou have made exact change.  Thank you.";
            else
            {
                // get coin distribution for payout, by denomination
                int[] chgArray = GetChangeDenominations(changeAmount);

                // convert integer (number of pennies) to string showing dollars & cents
                int changeDollars = changeAmount / 100;
                string dollarsString = changeDollars.ToString();
                int changeCents = changeAmount % 100;
                string centsString = "";
                if (changeCents < 10)
                    centsString = $"0{changeCents}";
                else
                    centsString = $"{changeCents}";
                string changeAmountString = $"{changeDollars}.{centsString}";

                // assemble message to display to user
                changeMessage = $"\nYour change of ${changeAmountString} is being paid as follows:\n" +
                    $"{chgArray[0]} quarter(s), {chgArray[1]} dime(s), {chgArray[2]} nickel(s), and " +
                    $"{chgArray[3]} pennies";
            }

            return changeMessage;
        }

        // Requests vending item price & payment, determines change amount due
        public static int GetChangeAmount()
        {
            UserInterface.DisplayPrompt("\nItem price dollars? (no decimals) ");
            int dollars = UserInterface.GetInteger();
            int price = dollars * 100;
            UserInterface.DisplayPrompt("Item price cents? (no decimals) ");
            int cents = UserInterface.GetInteger();
            price += cents;

            UserInterface.DisplayPrompt("Payment dollars? (no decimals) ");
            dollars = UserInterface.GetInteger();
            int amountPaid = dollars * 100;
            UserInterface.DisplayPrompt("Payment cents? (no decimals) ");
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

            // start by dividing change amount by 25, for # of quarters.  If no remainder,
            //  we're done.  Otherwise divide remainder by 10 for # of dimes, then
            //  divide that remainder by 5 for # of nickels, and so forth.
            
            int result = changeAmount / 25;
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
