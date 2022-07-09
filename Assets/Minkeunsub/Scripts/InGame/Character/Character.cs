using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PositionInfo
{
    public float x_pos, y_pos;
}

public abstract class Character : MonoBehaviour
{
    public PositionInfo posInfo;

}
