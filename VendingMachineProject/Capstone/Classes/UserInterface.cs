using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            if(vendingMachine.ReadError != "")
            {
                Console.WriteLine(vendingMachine.ReadError);
                Console.WriteLine("Please exit the vending machine and call Customer Support");
                Console.WriteLine();
            }

            bool done = false;
            while (!done)
            {
                // Top Level Menu

                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) End");

                string mainInput = Console.ReadLine();
                Console.WriteLine();

                switch (mainInput)
                {
                    case "1":
                        Display(); // Display list of all items with slot, name, and price
                        break;
                    case "2":
                        Purchase(); // Progress to purchasing menu
                        break;
                    case "3":
                        done = true; // Exit vending machine
                        break;
                    case "9":
                        vendingMachine.SalesReport(); // Hidden selection, run sales report

                        if (vendingMachine.WriteError != "")
                        {
                            Console.WriteLine(vendingMachine.WriteError);
                            Console.WriteLine("Please exit the vending machine and call Customer Support");
                            Console.WriteLine();
                        }

                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        Console.WriteLine();
                        break;
                }

                void Display() // Display list of all snack items
                {
                    Console.WriteLine(" Slot | Snack Item          | Price | Qty ");
                    Console.WriteLine("==========================================");
                    foreach (VendingMachineItem item in vendingMachine.items) 
                    {
                        Console.Write($"  {item.Slot}   ");
                        Console.Write($" {item.Name}".PadRight(22));
                        Console.Write($" ${item.Price}".PadRight(7));
                        if (item.Stock == 0)
                        {
                            Console.WriteLine("  OUT OF STOCK");
                        }
                        else
                        {
                            Console.WriteLine($"   {item.Stock}");
                        }
                    }
                    Console.WriteLine("==========================================");
                    Console.WriteLine();

                    return;
                }

                void Purchase() // Purchase an item from the machine
                {
                    bool purchase = true;
                    while (purchase)
                    {
                        Console.WriteLine("(1) Feed Money");
                        Console.WriteLine("(2) Select Product");
                        Console.WriteLine("(3) Finish Transaction");

                        Console.WriteLine();
                        Console.WriteLine($"Current Balance: ${vendingMachine.Balance}");


                        string purchaseInput = Console.ReadLine();
                        Console.WriteLine();


                        switch (purchaseInput)
                        {
                            case "1": // Ya pays your money and ya takes yer chances
                                bool feeding = true;
                                while (feeding)
                                {
                                    Console.WriteLine("How much would you like to add?");
                                    Console.WriteLine("1) $1 ");
                                    Console.WriteLine("2) $2 ");
                                    Console.WriteLine("3) $5 ");
                                    Console.WriteLine("4) $10 ");
                                    Console.WriteLine("5) Return to Purchase Menu");
                                    Console.WriteLine();
                                    Console.WriteLine($"Current Balance: ${vendingMachine.Balance}");

                                    string addedMoney = Console.ReadLine();
                                    Console.WriteLine();

                                    switch (addedMoney) // Add various whole-dollar denominations
                                    {
                                        case "1":
                                            vendingMachine.AddMoney(1);
                                            break;
                                        case "2":
                                            vendingMachine.AddMoney(2);
                                            break;
                                        case "3":
                                            vendingMachine.AddMoney(5);
                                            break;
                                        case "4":
                                            vendingMachine.AddMoney(10);
                                            break;
                                        case "5": // Exit Feed Money menu, return to Purchase menu
                                            feeding = false;
                                            break;
                                        default:
                                            Console.WriteLine("Please enter a valid amount");
                                            Console.WriteLine();
                                            break;
                                    }

                                    if (vendingMachine.WriteError != "")
                                    {
                                        Console.WriteLine(vendingMachine.WriteError);
                                        Console.WriteLine("Please exit the vending machine and call Customer Support");
                                        Console.WriteLine();
                                    }
                                }

                                break;
                            case "2":
                                Display(); // Select snack item
                                Console.WriteLine("Please enter a valid slot number to purchase the item");
                                string slot = Console.ReadLine();

                                Console.WriteLine(vendingMachine.Purchase(slot));
                                Console.WriteLine();

                                if (vendingMachine.WriteError != "")
                                {
                                    Console.WriteLine(vendingMachine.WriteError);
                                    Console.WriteLine("Please exit the vending machine and call Customer Support");
                                    Console.WriteLine();
                                }

                                break;
                            case "3": // Exit transaction, receive change, return to top menu
                                Console.WriteLine(vendingMachine.MakeChange());
                                Console.WriteLine();

                                if (vendingMachine.WriteError != "")
                                {
                                    Console.WriteLine(vendingMachine.WriteError);
                                    Console.WriteLine("Please exit the vending machine and call Customer Support");
                                    Console.WriteLine();
                                }

                                purchase = false;
                                break;

                            default:
                                Console.WriteLine("Please enter a valid selection.");
                                Console.WriteLine();
                                break;
                        }
                    }
                }
            }
        }
    }
}

