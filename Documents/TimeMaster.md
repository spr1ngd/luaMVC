
# LuaMVC.TimeMaster

## 1、什么是TimeMaster

> TimeMaster提供多种环境下的计时器/定时器
> - 普通计时器(支持注册多个定时任务，任务可重复)
> - 游戏技能计时器(支持buff效果，增/减cd时间)
> - 倒计时器(倒计时/倒计时回答,可增/减计时数)
> - 可记录计时器(在退出程序后依旧计时，重启后连续计时)

## 2、TimeMaster的优势

> - 1.TimeMaster可快速实现你的相关需求
> - 2.TimeMaster自动管理计时器，构造注册事件即可自动执行
> - 3.自动运行无GC

## 3、API详解

### 3.1 Timer 普通计时器

- 3.1.1 void StartTime();
> 开始计时方法,调用此方法会自动执行已注册的OnStartTimeAction委托事件
- 3.1.2 void CloseTime();
> 结束计时方法,调用此方法会自动执行已注册的OnCloseTimeAction委托事件
- 3.1.3 void PauseTime();
> 暂停计时方法
- 3.1.4 void ResumeTime();
> 恢复计时方法
- 3.1.5 void Clear();
> 清空当前计时

### 3.2 SkillTimer 游戏技能计时器

> 游戏技能计时器继承至普通计时器

- 3.2.1 void TimeBuff(float buffTime);
> 用于增益，直接减少固定冷却时间
- 3.2.2 void TimeDsbuff(float dsbuffTime);
> 用于减益，增加冷却时间

### 3.3 Countdown 倒计时器

> 继承至SkillTimer
> 重写部分方法以实现计时

### 3.4 RecordTimer 可记录计时器

> 继承至Timer
> 程序结束/程序开始自动读取历史记录，并还原构造新的计时器

## 使用案例