using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numPlayers = 1;
    public GameObject[] players;
    public float totalTime = 90f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI itPlayerText;
    private float time;
    public String[] playerNames;
    void Start()
    {   
        time = totalTime;
        int itPlayer = UnityEngine.Random.Range(0, numPlayers);
        players[itPlayer].GetComponent<PlayerMovement>().isIt = true;
        itPlayerText.gameObject.SetActive(false);
        for(int i = numPlayers; i < 4; i++)
        {
            players[i].SetActive(false);
        }
    }

    void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
            timerText.text = Mathf.Round(time).ToString();
        }
        if (time <= 0) {
            Time.timeScale = 0;
            string nameOfIt = "";
            for (int i = 0; i < numPlayers; i++) {
                if (players[i].GetComponent<PlayerMovement>().isIt) {
                    nameOfIt = playerNames[i]; 
                    break;
                }
            }
            itPlayerText.text = nameOfIt + " eats dog";
            itPlayerText.gameObject.SetActive(true);
        }
    }
}

