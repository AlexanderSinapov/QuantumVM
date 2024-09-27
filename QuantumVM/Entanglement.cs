using System;
using Raylib_cs;
using System.Numerics;

namespace QuantumVM
{
    class Entanglement
    {
        public bool simulate = false;

        public Entanglement()
        {
            // Screen settings
            int screenWidth = 800;
            int screenHeight = 600;

            // Initialize Raylib window
            Raylib.InitWindow(screenWidth, screenHeight, "QuantumVM - Entanglement Simulator");
            Raylib.SetTargetFPS(60);

            // Initialize 3D Camera
            Camera3D camera = new Camera3D();
            camera.Position = new Vector3(0.0f, 10.0f, 10.0f);  // Camera position
            camera.Target = new Vector3(0.0f, 0.0f, 0.0f);      // What camera is looking at
            camera.Up = new Vector3(0.0f, 1.0f, 0.0f);          // "Up" vector for camera orientation
            camera.FovY = 45.0f;                                // Field of view in Y-axis

            // Initial positions of two particles in 3D space
            Vector3 particle1 = new Vector3(-3.0f, 0.0f, 0.0f);
            Vector3 particle2 = new Vector3(3.0f, 0.0f, 0.0f);

            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                // Update camera based on user input
                Raylib.UpdateCamera(ref camera, CameraMode.Orbital);  // Orbit around target

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
                    Raylib.DrawText("Waiting for the command...", 10, 10, 20, Color.White);
                }

                Raylib.EndMode3D();
                Raylib.EndDrawing();
            }

            // Close window and OpenGL context
            Raylib.CloseWindow();
        }

        // Function to draw a wave-like connection between two points in 3D space
        static void DrawWaveConnection(Vector3 start, Vector3 end, int segments, float amplitude)
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
