using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    SceneTransition instance = null;
    public bool dontDestroy = true;

    [SerializeField] string roomPass = null;
    public string RoomPass { get => roomPass; private set { roomPass = value; } }


    public void Awake()
    {
        if (instance == null || ReferenceEquals(this, instance))
        {
            instance = this;
            if (dontDestroy)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(this);
        }

    }

    public void RoomPassSet(string pass)
    {
        RoomPass = pass;
    }
}
