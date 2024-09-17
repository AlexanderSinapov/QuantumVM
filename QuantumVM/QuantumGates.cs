using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QuantumVM
{
    internal class QuantumGates
    {
        // Hadamard Gate
        public static Complex[,] Hadamard = new Complex[2, 2]
        {
            {new Complex(1 / Math.Sqrt(2), 0), new Complex(1 / Math.Sqrt(2), 0) },
            {new Complex(1 / Math.Sqrt(2), 0), new Complex(-1 / Math.Sqrt(2), 0) }
        };

        // Pauli-X
        public static Complex[,] PauliX = new Complex[2, 2]
        {
            {0, 1},
            {1, 0}
        };

        // Pauli-Z
        public static Complex[,] PauliZ = new Complex[2, 2]
        {
            {1, 0 },
            {0, -1}
        };
    }
}
