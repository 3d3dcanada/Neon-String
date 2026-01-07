using System;

namespace FilamentFrontier.VisualSim
{
    public sealed class QualityShaderLogic
    {
        public float LayerHeight { get; private set; }
        public float Vibration { get; private set; }

        public QualityShaderLogic(float layerHeight, float vibration)
        {
            LayerHeight = layerHeight;
            Vibration = vibration;
        }

        public void UpdateInputs(float layerHeight, float vibration)
        {
            LayerHeight = layerHeight;
            Vibration = vibration;
        }

        public float CalculateNormalStrength()
        {
            if (LayerHeight <= 0.3f)
            {
                return 0.4f + (LayerHeight / 0.3f) * 0.4f;
            }

            return 0.8f + MathF.Min(0.4f, (LayerHeight - 0.3f) * 1.2f);
        }

        public float CalculateZBandingOffset(float time, float amplitudeScale = 1.0f)
        {
            var amplitude = MathF.Clamp(Vibration, 0.0f, 1.0f) * 0.02f * amplitudeScale;
            return MathF.Sin(time * 12.0f) * amplitude;
        }
    }
}
