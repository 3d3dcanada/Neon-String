using System;
using System.Collections.Generic;

namespace FilamentFrontier.EnvironmentSim
{
    public interface IPrinterPowerConsumer
    {
        bool IsActive { get; }
        float CurrentStateAmps { get; }
        string DisplayName { get; }
        void OnPowerLost();
    }

    public sealed class PowerGridManager
    {
        private readonly List<IPrinterPowerConsumer> _consumers = new();

        public float BreakerAmps { get; private set; }
        public float TotalAmps { get; private set; }
        public bool IsBlackout { get; private set; }

        public event Action<float>? BlackoutTriggered;

        public PowerGridManager(float breakerAmps = 15.0f)
        {
            BreakerAmps = breakerAmps;
        }

        public void Register(IPrinterPowerConsumer consumer)
        {
            if (!_consumers.Contains(consumer))
            {
                _consumers.Add(consumer);
            }
        }

        public void Unregister(IPrinterPowerConsumer consumer)
        {
            _consumers.Remove(consumer);
        }

        public void InstallSubPanel()
        {
            BreakerAmps = 100.0f;
            IsBlackout = false;
        }

        public void UpdateGrid()
        {
            TotalAmps = 0.0f;
            foreach (var consumer in _consumers)
            {
                if (consumer.IsActive)
                {
                    TotalAmps += consumer.CurrentStateAmps;
                }
            }

            if (!IsBlackout && TotalAmps > BreakerAmps)
            {
                TriggerBlackout();
            }
        }

        private void TriggerBlackout()
        {
            IsBlackout = true;

            foreach (var consumer in _consumers)
            {
                consumer.OnPowerLost();
            }

            BlackoutTriggered?.Invoke(TotalAmps);
        }
    }
}
