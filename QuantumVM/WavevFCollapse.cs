using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumVM
{
    internal class WavevFCollapse
    {
        public WavevFCollapse(int x, int y, int z, int Lx, int Ly, int Lz, int Nx, int Ny, int Nz)
        {
            psi(x, y, z, Lx, Ly, Lz, Nx, Nx, Nz);
        }

        public int psi(int x, int y, int z, int Lx, int Ly, int Lz, int Nx, int Ny, int Nz)
        {
            int probability = 0;
            double collapsed = Math.Sqrt(8 / (Lx * Ly * Lz)) *
                               Math.Sin((Nx * Math.PI * x) / Lx) *
                               Math.Sin((Ny * Math.PI * y) / Ly) *
                               Math.Sin((Nz * Math.PI * z) / Lz);
            

            if (collapsed > 0)
            {
                probability = 1;
            }
            else if (probability == 0)
            {
            probability = 0; 
            }

            Console.WriteLine($"The probability of finding a particle at x: {x}, y: {y}, z: {z}, \n with Lx: {Lx}, Ly: {Ly}, Lz: {Lz}, \n with Nx: {Nx}, Ny: {Ny}, Nz: {Nz} is {probability}");
            return probability;
        }
    }
}
