using System;
using UnityEngine;

public class ItMarker : MonoBehaviour
{
    public GameObject[] players;
    public float yOffset = 2.0f;
    void LateUpdate()
    {
        for(int i = 0; i < players.Length; i++) {
            if(players[i].GetComponent<PlayerMovement>().isIt) {
                transform.position = players[i].transform.position + new Vector3(0, yOffset, 0);
                transform.localScale = new Vector3(players[i].transform.localScale.x, -Math.Abs(players[i].transform.localScale.y / 4), players[i].transform.localScale.z);
                break;
            }
        }
    }
}
