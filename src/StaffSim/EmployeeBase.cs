using System;
using System.Collections.Generic;

namespace FilamentFrontier.StaffSim
{
    public enum EmployeeTrait
    {
        GCodeWizard,
        Intern,
        ResinHead,
    }

    public sealed class EmployeeBase
    {
        private readonly Dictionary<EmployeeTrait, float> _traitModifiers;

        public string Name { get; }
        public float Dexterity { get; private set; }
        public float TechKnowledge { get; private set; }
        public float Morale { get; private set; }
        public float SalaryPerShift { get; private set; }
        public float SabotageRisk { get; private set; }
        public IReadOnlyCollection<EmployeeTrait> Traits => _traitModifiers.Keys;

        public EmployeeBase(
            string name,
            float dexterity,
            float techKnowledge,
            float morale,
            float salaryPerShift,
            IReadOnlyCollection<EmployeeTrait> traits)
        {
            Name = name;
            Dexterity = MathF.Clamp(dexterity, 0.0f, 1.0f);
            TechKnowledge = MathF.Clamp(techKnowledge, 0.0f, 1.0f);
            Morale = MathF.Clamp(morale, 0.0f, 1.0f);
            SalaryPerShift = MathF.Max(0.0f, salaryPerShift);
            SabotageRisk = 0.0f;
            _traitModifiers = BuildTraitModifiers(traits);
            ApplyTraits();
        }

        public bool HasTrait(EmployeeTrait trait) => _traitModifiers.ContainsKey(trait);

        public void ApplyOvertime(int overtimeHours)
        {
            if (overtimeHours <= 0)
            {
                return;
            }

            var moraleDrop = overtimeHours * 0.05f;
            Morale = MathF.Max(0.0f, Morale - moraleDrop);
            SabotageRisk = MathF.Min(1.0f, SabotageRisk + overtimeHours * 0.02f);
        }

        public void RestoreMorale(float amount)
        {
            Morale = MathF.Clamp(Morale + amount, 0.0f, 1.0f);
            if (Morale > 0.6f)
            {
                SabotageRisk = MathF.Max(0.0f, SabotageRisk - 0.1f);
            }
        }

        public float GetPrintSpeedMultiplier()
        {
            return 1.0f + (_traitModifiers.TryGetValue(EmployeeTrait.GCodeWizard, out var bonus) ? bonus : 0.0f);
        }

        public float GetBedScratchChance()
        {
            return _traitModifiers.TryGetValue(EmployeeTrait.Intern, out var chance) ? chance : 0.0f;
        }

        public bool IsToxicFumeImmune()
        {
            return _traitModifiers.ContainsKey(EmployeeTrait.ResinHead);
        }

        private void ApplyTraits()
        {
            if (HasTrait(EmployeeTrait.GCodeWizard))
            {
                SalaryPerShift *= 1.4f;
            }

            if (HasTrait(EmployeeTrait.Intern))
            {
                SalaryPerShift = 0.0f;
            }

            if (HasTrait(EmployeeTrait.ResinHead))
            {
                Dexterity = MathF.Max(0.1f, Dexterity - 0.2f);
            }
        }

        private static Dictionary<EmployeeTrait, float> BuildTraitModifiers(IReadOnlyCollection<EmployeeTrait> traits)
        {
            var modifiers = new Dictionary<EmployeeTrait, float>();
            foreach (var trait in traits)
            {
                if (modifiers.ContainsKey(trait))
                {
                    continue;
                }

                switch (trait)
                {
                    case EmployeeTrait.GCodeWizard:
                        modifiers[trait] = 0.2f;
                        break;
                    case EmployeeTrait.Intern:
                        modifiers[trait] = 0.1f;
                        break;
                    case EmployeeTrait.ResinHead:
                        modifiers[trait] = 0.0f;
                        break;
                }
            }

            return modifiers;
        }
    }
}
