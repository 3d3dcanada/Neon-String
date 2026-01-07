using System;
using System.Collections.Generic;

namespace FilamentFrontier.StaffSim
{
    public enum JobType
    {
        RemovePrint,
        SwapSpool,
        FixClog,
        LevelBed,
    }

    public sealed class Job
    {
        public Guid Id { get; }
        public JobType Type { get; }
        public string TargetPrinter { get; }
        public float RequiredDexterity { get; }
        public float RequiredTechKnowledge { get; }

        public Job(JobType type, string targetPrinter, float requiredDexterity, float requiredTechKnowledge)
        {
            Id = Guid.NewGuid();
            Type = type;
            TargetPrinter = targetPrinter;
            RequiredDexterity = requiredDexterity;
            RequiredTechKnowledge = requiredTechKnowledge;
        }
    }

    public sealed class JobQueue
    {
        private readonly Queue<Job> _queue = new();

        public int Count => _queue.Count;

        public void Enqueue(Job job)
        {
            _queue.Enqueue(job);
        }

        public Job? Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        public IReadOnlyCollection<Job> PeekAll()
        {
            return _queue.ToArray();
        }
    }
}
