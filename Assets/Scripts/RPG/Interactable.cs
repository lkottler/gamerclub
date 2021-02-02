using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interactable : MonoBehaviour
{
    private GameObject tag;

    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            tag = TextTag.createTag(this.transform, "sample text");
            Debug.Log("player can pickup item");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            Destroy(tag);
            Debug.Log("player can no longer pickup item");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && Input.GetKey("f"))
        {
            Debug.Log("player attempted to pick up the item");
        }
    }
}
