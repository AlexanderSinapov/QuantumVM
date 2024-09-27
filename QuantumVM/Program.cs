using System.Text.RegularExpressions;

namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string quantumEquation = Console.ReadLine();
            QuantumEquationSolver solver = new QuantumEquationSolver(2);
            Regex regex = new Regex(@"\bsimulate quantum entanglement\b", RegexOptions.IgnoreCase);
            bool simulate = false;

            if (!false)
            {
                if (regex.IsMatch(quantumEquation))
                {
                    Entanglement entanglement = new Entanglement();
                    entanglement.simulate = true;
                }
            }

            solver.ParseAndRun(quantumEquation);
        }
    }
}
