using Photon.Pun;
using UnityEngine;

public class Stone : MonoBehaviourPunCallbacks
{

    [SerializeField] float punchPower = 10f;
    PhotonView view = null;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void SetPower(float power)
    {
        punchPower = power;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (view && !view.IsMine && collision.gameObject.CompareTag("Player"))
        {
            PhotonView collisionView = collision.GetComponent<PhotonView>();
            Vector3 dir = collision.transform.position - this.transform.position;
            if (collision.GetComponent<ActionControlBase>() != null)
            {
                collision.GetComponent<ActionControlBase>().Hit(dir.normalized * punchPower);

            }
        }
        if (collision.tag == "GameZone")
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
