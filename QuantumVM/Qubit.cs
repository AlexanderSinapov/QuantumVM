using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuantumVM
{
    internal class Qubit
    {
        public Complex Alpha { get; private set; } // Amplitude for |0) state
        public Complex Beta { get; private set; } // Amplitude for |1) state

        public Qubit()
        {
            Alpha = new Complex(1, 0);
            Beta = new Complex(0, 0);
        }

        public void ApplyGate(Complex[,] gate)
        {
            Complex newAlpha = gate[0, 0] * Alpha + gate[0, 1] * Beta;
            Complex newBeta = gate[1, 0] * Alpha + gate[1, 1] * Beta;

            Alpha = newAlpha;
            Beta = newBeta;

            Normalize();
        }

        public void Normalize()
        {
            double magnitude = Math.Sqrt(Math.Pow(Alpha.Magnitude, 2) + Math.Pow(Beta.Magnitude, 2));

            Alpha /= magnitude;
            Beta /= magnitude;
        }

        public int Measure()
        {
            // Measurement based on quantum probability of Alpha and Beta

            double probabilityZero = Math.Pow(Alpha.Magnitude, 2);
            Random random = new Random();

            if (random.NextDouble() < probabilityZero)
            {
                // Collapse the qubit to |0)
                Alpha = new Complex(1, 0);
                Beta = new Complex(0, 0);
                return 0;
            }
            else
            {
                // Collapse the qubit to |1)
                Alpha = new Complex(0, 0);
                Beta = new Complex(1, 0);
                return 1;
            }
            Console.WriteLine(Measure());
        }
    }
}
