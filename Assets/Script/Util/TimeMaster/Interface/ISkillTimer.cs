
namespace LuaMVC
{
    public interface ISkillTimer : ITimer
    {
        float cdTime { get; set; }
        bool ready { get; set; }
        float cdPercent { get; }
        void TimeBuff(float buff);
        void TimeDsbuff(float dsbuff);
    }
}