using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoHiROMConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("LoHiROMConvert - Convert SNES Games from LoROM to HiROM");
                string file = args[0];
                string outfile = args[1];

                byte[] lFile = File.ReadAllBytes(file);

                byte[] oFile = new byte[lFile.Length * 2];

                int div = 0x8000;
                int pcs = (lFile.Length / div);

                int hirompos = 0;
                int pcpos = 0;

                for (int c = 0; c < pcs; c++)
                {
                    for (int d = 0; d < div; d++)
                    {
                        pcpos = d + (c * div);
                        hirompos = d + (c * 0x10000);
                        oFile[hirompos] = ((c == 0) ? (byte)0xFF : lFile[pcpos]);
                        oFile[hirompos + div] = lFile[pcpos];
                    }
                }

                File.WriteAllBytes(outfile, oFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Usage: LoHiROMConvert [InFilePath] [OutFilePath]");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
