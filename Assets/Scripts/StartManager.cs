using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject playerSelection;
    public GameObject mapSelection;
    public GameObject gameTimeSelection;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void onStartGame() {
        SceneManager.LoadScene(mapSelection.GetComponent<MapSelection>().mapNumber);
        GameObject.Find("GameManager").GetComponent<GameManager>().numPlayers = playerSelection.GetComponent<PlayerSelection>().numberOfPlayers;
        GameObject.Find("GameManager").GetComponent<GameManager>().totalTime = gameTimeSelection.GetComponent<TimeSelection>().gameTime;
        Destroy(this.gameObject);
    }
}
