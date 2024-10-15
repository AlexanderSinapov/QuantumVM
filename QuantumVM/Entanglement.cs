using System;
using Raylib_cs;
using System.Numerics;

namespace QuantumVM
{
    class Entanglement
    {
        private Vector3 particle1;
        private Vector3 particle2;
        private Camera3D camera;
        private int screenWidth = 800;
        private int screenHeight = 600;
        public bool simulate = false;
        private QuantumCircuits quantumCircuit;

        public Entanglement(QuantumCircuits quantumCircuit)
        {
            this.quantumCircuit = quantumCircuit;

            // Initial particle positions
            particle1 = new Vector3(-3.0f, 0.0f, 0.0f);
            particle2 = new Vector3(3.0f, 0.0f, 0.0f);

            // Initialize 3D Camera
            camera = new Camera3D();
            camera.Position = new Vector3(0.0f, 10.0f, 10.0f);
            camera.Target = new Vector3(0.0f, 0.0f, 0.0f);
            camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
            camera.FovY = 45.0f;
        }

        public void StartSimulation()
        {
            // Initialize Raylib window
            Raylib.InitWindow(screenWidth, screenHeight, "QuantumVM - Entanglement Simulator");
            Raylib.SetTargetFPS(60);

            // Main loop
            while (!Raylib.WindowShouldClose())
            {
                UpdateCamera();
                RenderScene();
            }

            // Cleanup and close window
            Raylib.CloseWindow();
        }

        private void UpdateCamera()
        {
            // Update camera based on user input
            Raylib.UpdateCamera(ref camera, CameraMode.Orbital); // Orbit around target
        }

        private void RenderScene()
        {
            // Start drawing
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);

            Raylib.BeginMode3D(camera);

            if (simulate)
            {
                // Move particle1 with mouse control in the XZ plane
                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    Vector2 mousePos = Raylib.GetMousePosition();
                    particle1.X = (mousePos.X / screenWidth) * 10 - 5;  // Mapping mouse X to 3D
                    particle1.Z = (mousePos.Y / screenHeight) * 10 - 5; // Mapping mouse Y to 3D
                    Console.WriteLine(quantumCircuit.MeasureQubit(0));
                    Console.WriteLine(quantumCircuit.MeasureQubit(1));
                }

                // Simulate entanglement: Particle2 mirrors Particle1's movement
                particle2.X = -particle1.X;
                particle2.Z = -particle1.Z;

                // Draw the particles as spheres
                Raylib.DrawSphere(particle1, 0.5f, Color.Blue);   // Particle 1
                Raylib.DrawSphere(particle2, 0.5f, Color.Red);    // Particle 2

                // Draw the wave-like connection between the particles
                DrawWaveConnection(particle1, particle2, 20, 0.3f);
            }
            else
            {
                Raylib.DrawText("Waiting for entanglement...", 10, 10, 20, Color.White);
            }

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        // Function to simulate entanglement when CNOT is applied
        public void SimulateEntanglement(int controlQubitIndex, int targetQubitIndex)
        {
            // Apply Hadamard gate to control qubit
            quantumCircuit.ApplyGate(controlQubitIndex, QuantumGates.Hadamard);

            // Apply CNOT gate to control and target qubit
            quantumCircuit.ApplyGate(controlQubitIndex, QuantumGates.CNOT);

            // Assuming the circuit has exactly two qubits in a simple entanglement case
            int controlQubitResult = quantumCircuit.MeasureQubit(controlQubitIndex);
            int targetQubitResult = quantumCircuit.MeasureQubit(targetQubitIndex);

            if (controlQubitResult == targetQubitResult)
            {
                simulate = true; // Start the visualization once entanglement is confirmed
            }
        }

        // Function to draw a wave-like connection between two points in 3D space
        private static void DrawWaveConnection(Vector3 start, Vector3 end, int segments, float amplitude)
        {
            Vector3 direction = Vector3.Normalize(end - start);  // Direction from start to end
            float length = Vector3.Distance(start, end);         // Distance between the points

            for (int i = 0; i < segments; i++)
            {
                float t = (float)i / (segments - 1);             // Normalized parameter [0, 1]
                Vector3 currentPos = Vector3.Lerp(start, end, t); // Linear interpolation

                // Calculate wave effect using sine function
                float waveOffset = MathF.Sin(t * MathF.PI * 2) * amplitude;

                // Apply wave effect perpendicular to the direction vector
                Vector3 wave = new Vector3(-direction.Z, 0, direction.X) * waveOffset;

                // Draw a small sphere at each wave position to simulate a wave effect
                Raylib.DrawSphere(currentPos + wave, 0.1f, Color.Green);
            }
        }
    }
}
