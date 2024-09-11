using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WallShowLogic : MonoBehaviour
{
    public int actualIteration;
    public int requieredIterations;

    public bool isShown = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ChangeState();
    }
    public void IterateLeft()
    {
        
        actualIteration--;
        if (actualIteration < 0)
        {
            actualIteration = requieredIterations - 1;
            isShown = !isShown;
            ChangeState();
        }
    }
    public void IterateRight()
    {
        actualIteration++;
        if (requieredIterations == actualIteration)
        {
            actualIteration = 0;
            isShown = !isShown;
            ChangeState();
        }
    }

    private void ChangeState()
    {
        if (isShown)
        {
            anim.SetTrigger("Show");
        }
        else
        {
            anim.SetTrigger("Hide");
        }
    }
}