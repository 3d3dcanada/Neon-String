using System;
using System.Collections.Generic;

namespace FilamentFrontier.VisualSim
{
    public enum PrintState
    {
        Idle,
        Printing,
        Failed,
        Finished,
    }

    public sealed class RopeSegment
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float Thickness { get; }

        public RopeSegment(float x, float y, float z, float thickness)
        {
            X = x;
            Y = y;
            Z = z;
            Thickness = thickness;
        }
    }

    public sealed class ProceduralSpaghetti
    {
        private readonly List<RopeSegment> _segments = new();

        public PrintState State { get; private set; }
        public IReadOnlyList<RopeSegment> Segments => _segments;
        public float Gravity { get; set; } = 9.81f;
        public float SegmentLength { get; set; } = 0.02f;

        public void SetState(PrintState state)
        {
            State = state;
            if (state == PrintState.Failed)
            {
                _segments.Clear();
            }
        }

        public void Tick(float nozzleX, float nozzleY, float nozzleZ, float vibration)
        {
            if (State != PrintState.Failed)
            {
                return;
            }

            var sag = MathF.Min(0.1f, Gravity * 0.001f);
            var drift = MathF.Sin(nozzleX * 12.0f + _segments.Count) * vibration * 0.02f;

            var segment = new RopeSegment(
                nozzleX + drift,
                nozzleY - sag,
                nozzleZ,
                thickness: MathF.Max(0.001f, 0.005f - _segments.Count * 0.0001f));

            _segments.Add(segment);
        }
    }
}
