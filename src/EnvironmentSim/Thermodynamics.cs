using System;
using System.Collections.Generic;

namespace FilamentFrontier.EnvironmentSim
{
    public enum FilamentMaterial
    {
        PLA,
        ABS,
        PETG,
        TPU,
        ASA,
        CarbonFiberNylon,
        Unknown,
    }

    public interface IPrinterThermalEmitter
    {
        bool IsActive { get; }
        float HeatOutput { get; }
        FilamentMaterial Material { get; }
        string DisplayName { get; }
        void ApplyEnvironmentalEffect(EnvironmentalEffect effect);
    }

    public readonly struct EnvironmentalEffect
    {
        public float ClogChanceModifier { get; }
        public float WarpChanceModifier { get; }
        public bool HeatCreepWarning { get; }
        public bool ColdWarpWarning { get; }

        public EnvironmentalEffect(
            float clogChanceModifier,
            float warpChanceModifier,
            bool heatCreepWarning,
            bool coldWarpWarning)
        {
            ClogChanceModifier = clogChanceModifier;
            WarpChanceModifier = warpChanceModifier;
            HeatCreepWarning = heatCreepWarning;
            ColdWarpWarning = coldWarpWarning;
        }
    }

    public sealed class Thermodynamics
    {
        private readonly List<IPrinterThermalEmitter> _printers = new();

        public float RoomTempC { get; private set; }
        public float VentilationFlow { get; private set; }
        public float AmbientTempC { get; private set; }
        public float HeatPerPrinterPerMinuteC { get; }

        public Thermodynamics(
            float ambientTempC = 22.0f,
            float ventilationFlow = 1.0f,
            float heatPerPrinterPerMinuteC = 0.5f)
        {
            AmbientTempC = ambientTempC;
            RoomTempC = ambientTempC;
            VentilationFlow = MathF.Max(0.1f, ventilationFlow);
            HeatPerPrinterPerMinuteC = heatPerPrinterPerMinuteC;
        }

        public void Register(IPrinterThermalEmitter printer)
        {
            if (!_printers.Contains(printer))
            {
                _printers.Add(printer);
            }
        }

        public void Unregister(IPrinterThermalEmitter printer)
        {
            _printers.Remove(printer);
        }

        public void InstallExhaustFans(float addedFlow)
        {
            VentilationFlow = MathF.Clamp(VentilationFlow + addedFlow, 0.1f, 5.0f);
        }

        public void InstallAcUnit(float targetAmbientC)
        {
            AmbientTempC = MathF.Clamp(targetAmbientC, 10.0f, 26.0f);
        }

        public void TickMinute()
        {
            var activePrinters = 0;
            foreach (var printer in _printers)
            {
                if (printer.IsActive)
                {
                    activePrinters++;
                }
            }

            var heatGain = activePrinters * HeatPerPrinterPerMinuteC;
            var cooling = (RoomTempC - AmbientTempC) * 0.1f * VentilationFlow;
            RoomTempC = MathF.Max(AmbientTempC, RoomTempC + heatGain - cooling);

            ApplyEffects();
        }

        private void ApplyEffects()
        {
            foreach (var printer in _printers)
            {
                if (!printer.IsActive)
                {
                    continue;
                }

                var isHot = RoomTempC > 40.0f;
                var isCold = RoomTempC < 15.0f;

                var clogModifier = 0.0f;
                var warpModifier = 0.0f;

                if (isHot && printer.Material == FilamentMaterial.PLA)
                {
                    clogModifier = 0.5f;
                }

                if (isCold && printer.Material == FilamentMaterial.ABS)
                {
                    warpModifier = 0.5f;
                }

                var effect = new EnvironmentalEffect(
                    clogChanceModifier: clogModifier,
                    warpChanceModifier: warpModifier,
                    heatCreepWarning: isHot && printer.Material == FilamentMaterial.PLA,
                    coldWarpWarning: isCold && printer.Material == FilamentMaterial.ABS);

                printer.ApplyEnvironmentalEffect(effect);
            }
        }
    }
}
