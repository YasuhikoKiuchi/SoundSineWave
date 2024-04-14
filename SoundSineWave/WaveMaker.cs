using System;
using System.Collections.Generic;

namespace SoundSineWave
{
    internal class WaveMaker
    {
        internal int[] Execute(WaveMakerParam p)
        {
            var list = new List<int>();

            double theta = 0;
            for (int i = 0; i < p.DataSize; i++)
            {
                int a = (int)(Math.Sin(theta) * p.R);
                list.Add(a);
                theta += p.Dt;
            }

            return list.ToArray();
        }
    }
}
