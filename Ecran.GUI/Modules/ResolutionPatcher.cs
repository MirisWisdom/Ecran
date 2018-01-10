using Echoic.Binary;
using Echoic.Checksum;
using System;

namespace Ecran.GUI
{
    class ResolutionPatcher
    {
        readonly int divideValue = (int)Math.Pow(2, 8);
        readonly int offsetValue = 0xA68;

        readonly Blam blam;

        public ResolutionPatcher(Binary binary)
        {
            blam = new Blam(binary.Path);
        }

        public ResolutionPatcher ApplyResolution(Resolution resolution)
        {
            blam.Patch(new byte[]
            {
                (byte) (resolution.Width % divideValue),
                (byte) (resolution.Width / divideValue),

                (byte) (resolution.Height % divideValue),
                (byte) (resolution.Height / divideValue),
            }, offsetValue);

            return this;
        }

        public void ApplyNewHashing()
        {
            blam.Patch(new Func<byte[]>(() =>
            {
                var forge = new Forge(blam.Path).Calculate();
                Array.Reverse(forge);
                return forge;
            })(), Checksum.HashOffset);
        }
    }
}
