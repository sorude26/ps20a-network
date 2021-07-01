using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Stone : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
        if (collision.tag == "GameZoon")
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
