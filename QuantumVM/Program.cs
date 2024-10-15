using System;
using System.Text.RegularExpressions;

namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command:");
            Console.WriteLine("Enter help for commands!");
            string input = Console.ReadLine();

            // Initialize Quantum Equation Solver with 2 qubits
            QuantumEquationSolver solver = new QuantumEquationSolver(2);
            Regex regex = new Regex(@"\bsimulate quantum entanglement\b", RegexOptions.IgnoreCase);
            Regex help = new Regex(@"\bhelp\b", RegexOptions.IgnoreCase);

            if (regex.IsMatch(input))
            {
                // Initialize QuantumCircuits with 2 qubits for simulation
                QuantumCircuits quantumCircuits = new QuantumCircuits(2);

                // Create an instance of Entanglement with the quantum circuit
                Entanglement entanglement = new Entanglement(quantumCircuits);
                entanglement.simulate = true;

                // Call the method to start the simulation
                entanglement.SimulateEntanglement(0, 1); // Simulating entanglement between qubit 0 and 1

                // Start the simulation visual
                entanglement.StartSimulation();
            }
            else if (help.IsMatch(input))
            {
                DisplayHelpMenu(); // Call the method to display and navigate the menu
            }
            else
            {
                // Run the quantum equation solver if no match
                solver.ParseAndRun(input);
            }

            // Method to display and navigate the help menu
            static void DisplayHelpMenu()
            {
                string[] options = {
                "1. Commands",
                "2. Credits",
                "3. Exit",
            };

                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Help Menu (use arrow keys to navigate):");

                    // Loop through options and highlight the selected one
                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            // Highlight the selected option
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("> " + options[i]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("  " + options[i]);
                        }
                    }

                    // Capture user key press
                    key = Console.ReadKey(true).Key;

                    // Handle arrow key navigation
                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex < 0) selectedIndex = options.Length - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex >= options.Length) selectedIndex = 0;
                    }

                } while (key != ConsoleKey.Enter); // Exit loop when the user presses Enter

                // Handle selection
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine("Useful commands:");
                        Console.WriteLine("simulate quantum entanglement   //Opens a simulation where you can see how quantum entanglement works");
                        Console.WriteLine("H(q0 or q1)                     //Applies Hadamard gate on qubit 0 or 1");
                        Console.WriteLine("X(q0 or q1)                     //Applies Pauli-X gate on qubit 0 or 1");
                        Console.WriteLine("Y(q0 or q1)                     //Applies Pauli-Y gate on qubit 0 or 1");
                        Console.WriteLine("Z(q0 or q1)                     //Applies Pauli-Z gate on qubit 0 or 1");
                        Console.WriteLine("CNOT(q0 or q1)                  //Applies CNOT gate on qubit 0 or 1");
                        Console.WriteLine("M(q0 or q1)                     //Measures qubit 0 or 1");
                        break;
                    case 1:
                        Console.WriteLine("Credits");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("AlexanderSinapov");
                        Console.ResetColor();
                        break;
                    case 2:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
