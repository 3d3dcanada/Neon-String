using System;

namespace FilamentFrontier.VisualSim
{
    public sealed class NozzleCam
    {
        public int Width { get; }
        public int Height { get; }
        public int FrameRate { get; }
        public float NoiseStrength { get; private set; }
        public float ScanlineStrength { get; private set; }

        public NozzleCam(int width = 640, int height = 480, int frameRate = 12)
        {
            Width = width;
            Height = height;
            FrameRate = frameRate;
            NoiseStrength = 0.18f;
            ScanlineStrength = 0.25f;
        }

        public void SetNoise(float noiseStrength, float scanlineStrength)
        {
            NoiseStrength = MathF.Clamp(noiseStrength, 0.0f, 1.0f);
            ScanlineStrength = MathF.Clamp(scanlineStrength, 0.0f, 1.0f);
        }

        public float CalculateNoiseValue(float time)
        {
            return MathF.Abs(MathF.Sin(time * 24.0f)) * NoiseStrength;
        }

        public float CalculateScanlineValue(float y)
        {
            return (MathF.Sin(y * 80.0f) * 0.5f + 0.5f) * ScanlineStrength;
        }
    }
}
