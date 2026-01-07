using System;

namespace FilamentFrontier.MarketSim
{
    public sealed class FilamentFutures
    {
        private readonly Random _random;

        public float BasePlaPrice { get; private set; }
        public float CurrentPlaPrice { get; private set; }
        public float GlobalOilIndex { get; private set; }
        public float ShippingContainerIndex { get; private set; }

        public FilamentFutures(float basePlaPrice = 18.0f, int? seed = null)
        {
            BasePlaPrice = basePlaPrice;
            CurrentPlaPrice = basePlaPrice;
            GlobalOilIndex = 1.0f;
            ShippingContainerIndex = 1.0f;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public void UpdateDaily()
        {
            GlobalOilIndex = DriftIndex(GlobalOilIndex, 0.03f, 0.85f, 1.4f);
            ShippingContainerIndex = DriftIndex(ShippingContainerIndex, 0.05f, 0.7f, 1.6f);

            var supplyShock = _random.NextDouble() < 0.08 ? 0.2f : 0.0f;
            var indexMultiplier = GlobalOilIndex * ShippingContainerIndex * (1.0f + supplyShock);

            var dailyNoise = (float)(_random.NextDouble() * 0.06f - 0.03f);
            CurrentPlaPrice = MathF.Max(5.0f, BasePlaPrice * indexMultiplier * (1.0f + dailyNoise));
        }

        public void TriggerShippingCrisis()
        {
            ShippingContainerIndex = MathF.Min(2.0f, ShippingContainerIndex + 0.35f);
        }

        public void TriggerOilCrash()
        {
            GlobalOilIndex = MathF.Max(0.6f, GlobalOilIndex - 0.25f);
        }

        public float QuoteBulkPrice(float grams)
        {
            var kilograms = grams / 1000.0f;
            var bulkDiscount = kilograms >= 10.0f ? 0.75f : 0.9f;
            return CurrentPlaPrice * kilograms * bulkDiscount;
        }

        private float DriftIndex(float value, float volatility, float min, float max)
        {
            var delta = (float)(_random.NextDouble() * volatility * 2.0f - volatility);
            return MathF.Clamp(value + delta, min, max);
        }
    }
}
