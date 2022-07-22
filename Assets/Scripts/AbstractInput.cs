using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInput : MonoBehaviour
{
    private float point;
    public float Point { get { return point; } set { point = value; } }

    private bool check;
    public bool Check { get { return check; } set { check = value; } }
    public abstract void GetNewTargetPosition(float f);
    public abstract void addListener();
}
