using System;

namespace FilamentFrontier.DarkWebSim
{
    public sealed class TheDarkSlicer
    {
        public float ProcessingPower { get; private set; }
        public float EncryptionLayers { get; private set; }
        public bool IsUnlocked { get; private set; }

        public TheDarkSlicer(float processingPower)
        {
            ProcessingPower = MathF.Max(1.0f, processingPower);
        }

        public void SetProcessingPower(float processingPower)
        {
            ProcessingPower = MathF.Max(1.0f, processingPower);
        }

        public void LoadEncryptedFile(float encryptionLayers)
        {
            EncryptionLayers = MathF.Max(0.0f, encryptionLayers);
            IsUnlocked = false;
        }

        public float CalculateDecryptTimeSeconds()
        {
            if (EncryptionLayers <= 0.0f)
            {
                return 0.0f;
            }

            return EncryptionLayers / ProcessingPower * 12.0f;
        }

        public bool AttemptDecrypt(out float timeSeconds)
        {
            timeSeconds = CalculateDecryptTimeSeconds();
            if (EncryptionLayers <= 0.0f)
            {
                IsUnlocked = true;
                return true;
            }

            if (ProcessingPower <= 0.0f)
            {
                return false;
            }

            IsUnlocked = true;
            return true;
        }

        public float CalculateCustomGcodeSpeedBonus() => IsUnlocked ? 0.3f : 0.0f;
    }
}
