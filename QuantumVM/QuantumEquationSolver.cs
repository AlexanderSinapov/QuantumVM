using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace QuantumVM
{
    internal class QuantumEquationSolver
    {
        private QuantumCircuits circuit;
        private Dictionary<string, Complex[,]> gateMap;

        public QuantumEquationSolver(int numQubits)
        {
            circuit = new QuantumCircuits(numQubits);

            // Map gate names to gate matrices
            gateMap = new Dictionary<string, Complex[,]>
            {
                {"H", QuantumGates.Hadamard },
                {"X", QuantumGates.PauliX },
                {"Z", QuantumGates.PauliZ }
            };
        }

        public void ParseAndRun(string equation)
        {
            string[] instructions = equation.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string instruction in instructions)
            {
                string trimmedInstruction = instruction.Trim();

                if (trimmedInstruction.StartsWith("H") || trimmedInstruction.StartsWith("X") || trimmedInstruction.StartsWith("Z"))
                {
                    ApplySingleQubitGate(trimmedInstruction);
                }
                else if (trimmedInstruction.StartsWith("CNOT"))
                {
                    ApplyCNOTGate(trimmedInstruction);
                }
                else if (trimmedInstruction.StartsWith("M"))
                {
                    MeasureQubit(trimmedInstruction);
                }
            }

            circuit.DisplayQubitStates();
        }

        private void ApplySingleQubitGate(string instruction)
        {
            var match = Regex.Match(instruction, @"([HXZ])\((q\d+)\)");
            if (match.Success)
            {
                string gateName = match.Groups[1].Value;
                int qubitIndex = int.Parse(match.Groups[2].Value.Substring(1));

                if (gateMap.ContainsKey(gateName))
                {
                    Console.WriteLine($"Applying {gateName} gate to qubit {qubitIndex}");
                    circuit.ApplyGate(qubitIndex, gateMap[gateName]);
                }
            }
        }

        private void ApplyCNOTGate(string instruction)
        {
            var match = Regex.Match(instruction, @"CNOT\((q\d+), \s*(q\d+)\)");
            if (match.Success)
            {
                int controlQubit = int.Parse(match.Groups[1].Value.Substring(1));
                int targetQubit = int.Parse(match.Groups[2].Value.Substring(1));

                Console.WriteLine($"Applying CNOT gate to control qubit {controlQubit} and target qubit {targetQubit}");
                ApplyCNOT(controlQubit, targetQubit);
            }
        }

        private void ApplyCNOT(int controlIndex, int targetIndex)
        {
            // Retrieve the current state (amplitudes) of the control and target qubits
            Qubit controlQubit = circuit.qubits[controlIndex];
            Qubit targetQubit = circuit.qubits[targetIndex];

            // Combine the states of control and target qubits into a 4x1 vector
            Complex[] combinedState = new Complex[4];
            combinedState[0] = controlQubit.Alpha * targetQubit.Alpha; // |00)
            combinedState[1] = controlQubit.Alpha * controlQubit.Beta; // |01)
            combinedState[2] = controlQubit.Beta * targetQubit.Alpha;  // |10)
            combinedState[3] = controlQubit.Beta * targetQubit.Beta;   // |11)

            // Apply the CNOT gate (which is a 4x4 matrix)
            Complex[] newState = new Complex[4];
            Complex[,] cnotGate = QuantumGates.CNOT;

            for (int i = 0; i < 4; i++)
            {
                newState[i] = Complex.Zero;
                for (int j = 0; j < 4; j++)
                {
                    newState[i] += cnotGate[i, j] * combinedState[j]; 
                }
            }

            // Update the control and target qubits with the new state
            // We decompose the resulting 4x1 state vector back into two qubits
            controlQubit.Alpha = newState[0] + newState[1]; // |0) part of the control qubit
            controlQubit.Beta = newState[2] + newState[3];  // |1) part of the control qubit
            targetQubit.Alpha = newState[0] + newState[2];  // |0) part of the target qubit
            targetQubit.Beta = newState[1] + newState[3];   // |1) part of the target qubit

            // Normalize the qubits to maintain valid quantum states
            controlQubit.Normalize();
            targetQubit.Normalize();
        }

        private void MeasureQubit(string instruction)
        {
            // Extract the qubit index to measure
            var match = Regex.Match(instruction, @"M\((q\d+)\)");
            if (match.Success)
            {
                int qubitIndex = int.Parse(match.Groups[1].Value.Substring(1));

                // Measure the qubit and print the result
                int result = circuit.MeasureQubit(qubitIndex);
                Console.WriteLine($"Measurement of qubit {qubitIndex}: {result}");
            }
        }
    }
}
