using System;

namespace VendingChangeMaker
{
    class Program
    {
        // (from LaunchCode puzzle)
        // takes purchase price, payment amount
        // dispenses change in coins only

        //
        // NEEDS TEST PROGRAM
        //
        // NEEDS EXCEPTION HANDLING
        //

        static void Main(string[] args)
        {
            bool makePurchase = true;
            while (makePurchase)
            {
                string refundMessage = Change.GetChangeMessage();
                UserInterface.DisplayMessage(refundMessage);

                UserInterface.DisplayPrompt("\nMake another purchase? (Y/N) ");
                string yesOrNo = UserInterface.GetString();
                if (yesOrNo.ToUpper() == "N")
                    makePurchase = false;
            }

            UserInterface.PromptForExit();
        }
    }
}
