using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    internal void OnObjectCollected(GameObject gObj)
    {
        Destroy(gObj);
        Close();
    }

    private void Close()
    {
        animator.Play("window-closing");
    }
}
