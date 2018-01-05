
namespace LuaMVC
{
    using System;
    using System.Collections.Generic;

    public interface ITimer
    {
        string name { get; }
        bool timing { get; }
        bool timeScaleEffect { get; }
        float elapsed { get; set; }
        IList<IntervalEvent> intervalEvents { get; }

        Action OnStartTimeAction { get; set; }
        Action<float> OnCloseTimeAction { get; set; }
        Action<float> OnTimingAction { get; set; }

        void StartTime();
        void CloseTime();
        void PauseTime();
        void ResumeTime();
        void ClearTime();
    }
}