using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingAnimationScript : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(transform.localScale * 1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
