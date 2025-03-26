using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numPlayers = 1;
    public GameObject[] players;
    void Start()
    {
        int itPlayer = UnityEngine.Random.Range(0, numPlayers);
        players[itPlayer].GetComponent<PlayerMovement>().isIt = true;
        for(int i = numPlayers; i < 4; i++)
        {
            players[i].SetActive(false);
        }
    }
}

