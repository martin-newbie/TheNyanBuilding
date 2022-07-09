using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{



}

public interface ITouchAble
{
    public abstract void OnTouch();
}

public interface IGauge
{
    public abstract void OnIdle();
}

public interface IBuff
{
    public abstract void OnBuff();
}
