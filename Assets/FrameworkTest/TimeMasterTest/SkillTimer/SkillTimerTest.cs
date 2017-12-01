
using System;
using LuaMVC;
using UnityEngine;
using UnityEngine.UI;

public class Skill
{
    private string name = null;
    public float CDTime = 8.0f;
    public SkillTimer SkillTimer = null;
    public Action SkillEffect = null;

    public Skill( string name, float cdTime ,Action effect)
    {
        this.name = name;
        this.CDTime = cdTime;
        SkillEffect = effect;
        SkillTimer = new SkillTimer(name, false,cdTime);
        SkillTimer.OnCloseTimeAction += CDOver;
    }

    public void UseSkill()
    {
        if (SkillTimer.ready)
        {
            SkillTimer.StartTime();
            SkillEffect();
        }
        else
            Debug.Log(name + "技能尚未冷却");
    }

    private void CDOver(float time)
    {
        Debug.Log(name + "技能已经冷却");
    }
}

public class SkillTimerTest : MonoBehaviour
{
    public Button Skill_One = null;
    public Button Skill_Two = null;
    public Image Skill_One_Mask = null;
    public Image Skill_Two_Mask = null;

    private void Awake()
    {
        Skill skillone = new Skill("闪现",12, () => { Debug.Log("使用了技能一：闪现"); });
        Skill skilltwo = new Skill("时间流逝",5, () =>
        {
            // 给技能一时间流逝效果
            skillone.SkillTimer.TimeBuff(3);
            Debug.Log("使用了技能二：时间流逝");
        });
        skillone.SkillTimer.OnTimingAction = (percent) => { Skill_One_Mask.fillAmount = 1 - skillone.SkillTimer.cdPercent; };
        skilltwo.SkillTimer.OnTimingAction = (percent) => { Skill_Two_Mask.fillAmount = 1 - skilltwo.SkillTimer.cdPercent; };
        Skill_One.onClick.AddListener(skillone.UseSkill);
        Skill_Two.onClick.AddListener(skilltwo.UseSkill);
    }
}