using System;
using System.Collections.Generic;

namespace FilamentFrontier.StaffSim
{
    public sealed class StaffGenerator
    {
        private readonly Random _random;

        public StaffGenerator(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public EmployeeBase Generate(string name)
        {
            var dexterity = NextFloat(0.4f, 0.9f);
            var techKnowledge = NextFloat(0.3f, 0.95f);
            var morale = NextFloat(0.5f, 0.9f);
            var baseSalary = NextFloat(45.0f, 120.0f);

            var traits = RollTraits();
            return new EmployeeBase(name, dexterity, techKnowledge, morale, baseSalary, traits);
        }

        private IReadOnlyCollection<EmployeeTrait> RollTraits()
        {
            var traits = new List<EmployeeTrait>();
            if (_random.NextDouble() < 0.18)
            {
                traits.Add(EmployeeTrait.GCodeWizard);
            }

            if (_random.NextDouble() < 0.15)
            {
                traits.Add(EmployeeTrait.Intern);
            }

            if (_random.NextDouble() < 0.12)
            {
                traits.Add(EmployeeTrait.ResinHead);
            }

            return traits;
        }

        private float NextFloat(float min, float max)
        {
            return (float)(_random.NextDouble() * (max - min) + min);
        }
    }
}
