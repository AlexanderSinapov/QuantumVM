namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string quantumEquation = Console.ReadLine();
            QuantumEquationSolver solver = new QuantumEquationSolver(2);

            solver.ParseAndRun(quantumEquation);
        }
    }
}
