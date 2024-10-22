using System;
using System.Numerics;

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

        public static Complex[,] CNOT = new Complex[4, 4]
        {
            {1, 0, 0, 0 },
            {0, 1, 0, 0 },
            {0, 0, 0, 1 },
            {0, 0, 1, 0 }
        };

        public static Complex[,] PauliY = new Complex[2, 2]
        {
            {new Complex(0, 0), -Complex.ImaginaryOne },
            {Complex.ImaginaryOne,  new Complex(0, 0)},
        };

        public static Complex[,] Phase = new Complex[2, 2]
        {
            {1, 0 },
            {0, Complex.ImaginaryOne },
        };

        public static Complex[,] TGate = new Complex[2, 2]
        {
            {1, 0},
            {0, Math.Pow(Math.E, (Math.Sqrt(-1) * Math.PI)) / 4},
        };

        public static Complex[,] CZ = new Complex[4, 4]
        {
            {1, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 0, 1, 0},
            {0, 0, 0, -1},
        };

        public static Complex[,] SWAP = new Complex[4, 4]
        {
            {1, 0, 0, 0 },
            {0, 0, 1, 0 },
            {0, 1, 0, 0 },
            {0, 0, 0, 1 },
        };
    }
}
