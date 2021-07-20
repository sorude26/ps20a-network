using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    int phase = 0;
    Animator animator;

    public void DestroyEffect()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
