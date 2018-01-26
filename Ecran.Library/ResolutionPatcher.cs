using Echoic.Binary;
using Echoic.Checksum;
using System;

namespace Ecran.Library
{
    public class ResolutionPatcher
    {
        private readonly int _divideValue = (int)Math.Pow(2, 8);
        private readonly int _offsetValue = 0xA68;

        private readonly Blam _blam;

        public ResolutionPatcher(string binaryPath)
        {
            _blam = new Blam(binaryPath);
        }

        public ResolutionPatcher ApplyResolution(Resolution resolution)
        {
            _blam.Patch(new byte[]
            {
                (byte) (resolution.Width % _divideValue),
                (byte) (resolution.Width / _divideValue),

                (byte) (resolution.Height % _divideValue),
                (byte) (resolution.Height / _divideValue),
            }, _offsetValue);

            return this;
        }

        public void ApplyNewHashing()
        {
            _blam.Patch(new Func<byte[]>(() =>
            {
                var forge = new Forge(_blam.Path).Calculate(0, Checksum.FileLength - Checksum.HashLength);
                Array.Reverse(forge);
                return forge;
            })(), Checksum.HashOffset);
        }
    }
}
