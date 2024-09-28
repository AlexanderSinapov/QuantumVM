using System;
using System.Text.RegularExpressions;

namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command:");
            string input = Console.ReadLine();

            // Initialize Quantum Equation Solver with 2 qubits
            QuantumEquationSolver solver = new QuantumEquationSolver(2);
            Regex regex = new Regex(@"\bsimulate quantum entanglement\b", RegexOptions.IgnoreCase);

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
            else
            {
                // Run the quantum equation solver if no match
                solver.ParseAndRun(input);
            }
        }
    }
}
