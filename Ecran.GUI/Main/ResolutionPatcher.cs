using Echoic.Binary;
using Echoic.Checksum;
using System;

namespace Ecran.GUI.Main
{
    class ResolutionPatcher
    {
        readonly int divideValue = (int)Math.Pow(2, 8);
        readonly int offsetValue = 0xA68;

        Blam blam;

        public ResolutionPatcher(Blam blamBinary)
        {
            blam = blamBinary;
        }

        public ResolutionPatcher ApplyResolution(Resolution resolution)
        {
            blam.Patch(new Func<byte[]>(() =>
            {
                return new byte[]
                {
                    (byte) (resolution.Width % divideValue),
                    (byte) (resolution.Width / divideValue),

                    (byte) (resolution.Height % divideValue),
                    (byte) (resolution.Height / divideValue),
                };
            })(), offsetValue);

            return this;
        }

        public ResolutionPatcher ApplyNewHashing()
        {
            blam.Patch(new Func<byte[]>(() =>
            {
                var forge = new Forge(blam.Path).Calculate();
                Array.Reverse(forge);
                return forge;
            })(), Checksum.FileLength);

            return this;
        }
    }
}
