using System;
using System.IO;

/*---------------------------------------------------------------------------------------
 * Mega Escritorio Desk Price Calculator
 * This program will calculate the total purchase price of a desk from Mega Escritorio 
 * 
 * Author: Nikkala Thomson
 * CIT 301C, Winter 2017, Bro. Blazzard
 * -------------------------------------------------------------------------------------*/

namespace MegaEscritorio
{
    class Program
    {
        static void Main()
        {
            int BaseDeskPrice = 20000;    // base desk price in cents 
            double deskWidth = 0.0;       // width of desktop
            double deskLength = 0.0;      // length of desktop
            double surfaceArea = 0.0;     // surface of desktop
            int numDrawers = 0;           // number of drawers in desk
            string surfaceMaterial = "";  // surface material of desktop
            string shippingSpeed = "";    // delivery shipping speed
            string outputFileName = "";   // filname for output
            bool success = false;         // boolean to use for validating input
            int[,] rushOrderArray = new int[3, 3];  //details for rush order shipping fees
            int priceFromSurfaceArea = 0;  
            int priceFromDrawers = 0;
            int surfaceMaterialFee = 0;
            int deliveryFee = 0;
            int deskPrice = 0;           // total desk price in cents 

            Console.WriteLine("This program will calculate the total price of a desk from Mega Escritorio.  \n");
            Console.WriteLine("Please be aware that a surface area of over 1000 square inches will result in a VERY EXPENSIVE DESK!");
            Console.WriteLine("You have been warned.\n");

            // Get information on desk from user

            while (!success)  // get width of desktop
            {
                try
                {
                    Console.Write("Enter desired desktop width in inches:  ");
                    if (Double.TryParse(Console.ReadLine(), out deskWidth) == false)
                        throw new FormatException("\nPlease enter a decimal number.");
                    if (deskWidth <= 0.0)
                        throw new ArgumentException("\nDesktop width must be greater than zero. ");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;
            while (!success)  // get length of desktop
            {
                try
                {
                    Console.Write("Enter desired desktop length in inches:  ");
                    if (Double.TryParse(Console.ReadLine(), out deskLength) == false)
                        throw new FormatException("\nPlease enter a decimal number.");
                    if (deskLength <= 0.0)
                        throw new ArgumentException("\nDesktop length must be greater than zero. ");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // calculate surface area of desktop
            surfaceArea = deskWidth * deskLength;

            success = false;
            while (!success)  // get number of drawers
            {
                try
                {
                    Console.Write("Enter number of desk drawers (0 to 7):  ");
                    if (int.TryParse(Console.ReadLine(), out numDrawers) == false)
                        throw new FormatException("\nPlease enter an integer.");
                    if (numDrawers < 0)
                        throw new ArgumentException("\nNumber of desk drawers cannot be negative. ");
                    if (numDrawers > 7)
                        throw new ArgumentException("\nNumber of desk drawers cannot be greater than seven. ");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;
            while (!success)  // get surface material
            {
                try
                {
                    Console.Write("Enter desired surface material (laminate, oak, or pine):  ");
                    surfaceMaterial = Console.ReadLine().ToLower();
                    switch (surfaceMaterial)
                    {
                        case "laminate":
                        case "oak":
                        case "pine":
                            {
                                success = true;
                                break;
                            }
                        default:
                            {
                                throw new ArgumentException("\nUnable to recognize that material.  Please enter only laminate, oak, or pine.");
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;
            while (!success)  // get surface material
            {
                try
                {
                    Console.Write("Enter desired shipping speed (no rush, 3 day, 5 day, or 7 day):  ");
                    shippingSpeed = Console.ReadLine().ToLower();
                    switch (shippingSpeed)
                    {
                        case "no rush":
                        case "3 day":
                        case "5 day":
                        case "7 day":
                                success = true;
                                break;
                        default:
                                throw new ArgumentException("\nUnable to recognize that shipping speed.  Please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            ReadRushOrderPrices(ref rushOrderArray);

            // calculate total desk price
            priceFromSurfaceArea = CalcPriceFromSurfaceArea(surfaceArea);
            priceFromDrawers = CalcPriceFromDrawers(numDrawers);
            surfaceMaterialFee = CalcSurfaceMaterialFee(surfaceMaterial);
            deliveryFee = CalcShippingFee(surfaceArea, shippingSpeed, rushOrderArray);
            deskPrice = BaseDeskPrice + priceFromDrawers + priceFromSurfaceArea + surfaceMaterialFee + deliveryFee;

            // write order total with line items
            Console.WriteLine(string.Format("\nBase desk price: \t{0,10:C}", BaseDeskPrice / 100));
            Console.WriteLine(string.Format("Price from surface area: {0,9:C}", priceFromSurfaceArea / 100));
            Console.WriteLine(string.Format("Price from drawers: \t{0,10:C}", priceFromDrawers / 100));           
            Console.WriteLine(string.Format("Surface material fee: \t{0,10:C}", surfaceMaterialFee / 100));
            Console.WriteLine(string.Format("Delivery fee: \t\t{0,10:C}", deliveryFee / 100));
            Console.WriteLine(string.Format("Total Desk Price: \t{0,10:C}", deskPrice / 100));

            //write order total with line items to file 
            success = false;
            while (!success)  // get surface material
            {
                try
                {
                    Console.Write("\nEnter filename to save price information (include new path if desired):  \n");
                    outputFileName = Console.ReadLine();
                    string[] lines = {string.Format("\nBase desk price: \t{0,10:C}", BaseDeskPrice / 100),
                    string.Format("Price from surface area: {0,9:C}", priceFromSurfaceArea / 100),
                    string.Format("Price from drawers: \t{0,10:C}", priceFromDrawers / 100),
                    string.Format("Surface material fee: \t{0,10:C}", surfaceMaterialFee / 100),
                    string.Format("Delivery fee: \t\t{0,10:C}", deliveryFee / 100),
                    string.Format("Total Desk Price: \t{0,10:C}", deskPrice / 100)};
                    System.IO.File.WriteAllLines(@outputFileName, lines);
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("\nThank you for shopping at Mega Escritorio!");

            // pause the program until the user presses a key to give them time to read what is on the screen
            Console.ReadKey();
        }
        
        // calculate price from surface area
        private static int CalcPriceFromSurfaceArea(double surfaceArea)
        {
            if (surfaceArea <= 1000)
                return 0;
            else return (int) (surfaceArea - 1000) * 500;
        }

        // calculate price from drawers
        private static int CalcPriceFromDrawers(int numDrawers)
        {
            return 5000 * numDrawers;
        }

        // calculate surface material fee
        private static int CalcSurfaceMaterialFee(string surfaceMaterial)
        {
            switch (surfaceMaterial)
            {
                case "laminate":
                    return 10000;
                case "oak":
                    return 20000;
                case "pine":
                    return 5000;
                default:  // this should never be triggered
                    return 0;
            }

        }

        // calculate rush shipping fee, if any
        private static int CalcShippingFee(double surfaceArea, string shippingSpeed, int[,] rushOrderArray)
        {
            int i;
            int j;

            switch (shippingSpeed) {
                case "no rush":
                    return 0;
                case "3 day": i = 0; break;
                case "5 day": i = 1; break;
                case "7 day": i = 2; break;
                default:  // this should never occur
                    return 0;
            }          

            if (surfaceArea < 1000)
                j = 0;
            else if (surfaceArea < 2000)
                j = 1;
            else j = 2;

            return rushOrderArray[i,j];
        }

        // read in the text file to populate rushOrderArray    
        private static void ReadRushOrderPrices(ref int[,] rushOrderArray)
        {
            try
            {
                string[] rushPrices = File.ReadAllLines(@"H:\OneDrive\BYU-Idaho\ASP.NET 301C\Mega Escritorio\rushOrderPrices.txt");
                int readLineCounter = 0;
                for (int i = 0; i < rushOrderArray.GetLength(0); i++)
                {
                    for (int j = 0; j < rushOrderArray.GetLength(1); j++)
                    { 
                        rushOrderArray[i, j] = int.Parse(rushPrices[readLineCounter]) * 100; //adjusting for cents
                        readLineCounter++;
                    }       
                }
           }
           catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }           
        }
    }
}
