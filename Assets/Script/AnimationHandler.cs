using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    

    private Animator _anim;


    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void isRunning()
    {
        _anim.SetBool("isRunning", true);
        _anim.SetBool("isWaiting", false);
        _anim.SetBool("isPunching", false);
    }

    public void isWaiting()
    {
        _anim.SetBool("isRunning", false);
        _anim.SetBool("isWaiting", true);
        _anim.SetBool("isPunching", false);
    }

    public void isPunching()
    {
        _anim.SetBool("isRunning", false);
        _anim.SetBool("isWaiting", false);
        _anim.SetBool("isPunching", true);
    }

}
