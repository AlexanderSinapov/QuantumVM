using System.Security.Cryptography;

namespace QuantumVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Qubit qubit = new Qubit();
            Random random = new Random();

            Console.WriteLine(qubit.Measure([]));
        }
    }
}
