using System.Security.Cryptography;

namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuantumCircuits circuit = new QuantumCircuits(2);

            circuit.ApplyGate(0, QuantumGates.Hadamard);
            Console.WriteLine("Applying Hadamard gate to qubit 0: ");
            circuit.DisplayQubitStates();

            int measurment1 = circuit.MeasureQubit(0);
            int measurment2 = circuit.MeasureQubit(1);
        }
    }
}
