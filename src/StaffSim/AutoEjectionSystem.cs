using System;

namespace FilamentFrontier.StaffSim
{
    public sealed class AutoEjectionSystem
    {
        public float RobotCost { get; }
        public float RobotMaintenancePerShift { get; }
        public float HumanSalaryPerShift { get; }
        public int ShiftsPerWeek { get; }

        public AutoEjectionSystem(
            float robotCost,
            float robotMaintenancePerShift,
            float humanSalaryPerShift,
            int shiftsPerWeek = 6)
        {
            RobotCost = robotCost;
            RobotMaintenancePerShift = robotMaintenancePerShift;
            HumanSalaryPerShift = humanSalaryPerShift;
            ShiftsPerWeek = shiftsPerWeek;
        }

        public float CalculateWeeklyRobotCost()
        {
            return RobotMaintenancePerShift * ShiftsPerWeek;
        }

        public float CalculateWeeklyHumanCost(int headCount)
        {
            return HumanSalaryPerShift * headCount * ShiftsPerWeek;
        }

        public float CalculateBreakEvenWeeks(int headCount)
        {
            var weeklyHuman = CalculateWeeklyHumanCost(headCount);
            var weeklyRobot = CalculateWeeklyRobotCost();
            var savings = MathF.Max(0.01f, weeklyHuman - weeklyRobot);
            return RobotCost / savings;
        }

        public string GetRecommendation(int headCount)
        {
            var breakEvenWeeks = CalculateBreakEvenWeeks(headCount);
            if (breakEvenWeeks < 12.0f)
            {
                return "Robotic arms pay off in under a quarter. Install the Infinite-Z mod.";
            }

            if (breakEvenWeeks < 30.0f)
            {
                return "Robots pay off within the year. Keep the humans on call.";
            }

            return "Stick with staff for now. Robots are a vanity purchase.";
        }
    }
}
