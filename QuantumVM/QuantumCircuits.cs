using System;
using System.Numerics;

namespace QuantumVM
{
    internal class QuantumCircuits
    {
        public List<Qubit> qubits;

        public QuantumCircuits(int numQubits)
        {
            qubits = new List<Qubit>();
            for (int i = 0; i < numQubits; i++)
            {
                qubits.Add(new Qubit());
            }
        }

        public void ApplyGate(int qubitIndex, Complex[,] gate)
        {
            qubits[qubitIndex].ApplyGate(gate);
        }

        public int MeasureQubit(int qubitIndex)
        {
            return qubits[qubitIndex].Measure();
        }

        public void DisplayQubitStates()
        {
            for (int i = 0; i < qubits.Count; i++)
            {
                Console.WriteLine($"Qubit {i}: |0) amplitude = {qubits[i].Alpha}, |1) amplitude = {qubits[i].Beta}");
            }
        }
    }
}
