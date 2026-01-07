using System;
using System.Collections.Generic;

namespace FilamentFrontier.StaffSim
{
    public sealed class ShiftScheduler
    {
        private readonly List<EmployeeBase> _activeStaff = new();

        public int ShiftLengthHours { get; }

        public ShiftScheduler(int shiftLengthHours = 8)
        {
            ShiftLengthHours = shiftLengthHours;
        }

        public void AssignToShift(EmployeeBase employee)
        {
            if (!_activeStaff.Contains(employee))
            {
                _activeStaff.Add(employee);
            }
        }

        public void RemoveFromShift(EmployeeBase employee)
        {
            _activeStaff.Remove(employee);
        }

        public IReadOnlyList<EmployeeBase> GetActiveStaff() => _activeStaff;

        public void ApplyOvertime(int overtimeHours)
        {
            foreach (var employee in _activeStaff)
            {
                employee.ApplyOvertime(overtimeHours);
            }
        }

        public float CalculateTotalSalary()
        {
            var total = 0.0f;
            foreach (var employee in _activeStaff)
            {
                total += employee.SalaryPerShift;
            }

            return total;
        }
    }
}
