namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the Quantum Equation Solver with 2 qubits
            QuantumEquationSolver solver = new QuantumEquationSolver(2);

            string quantumEquation = @"
                H(q0);
                X(q1);
                CNOT(q0, q1);
                M(q0);
                M(q1);
            ";

            // Parse and run the quantum equation
            solver.ParseAndRun(quantumEquation);
        }
    }
}
