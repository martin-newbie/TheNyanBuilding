using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCanvas : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
    }
    public void Open()
    {
        animator.SetFloat("speed", 1);
        animator.Play("OpenCat", -1);

    }
    public void Close()
    {
        animator.SetFloat("speed",-1);
        animator.Play("OpenCat", -1);
    }
}
