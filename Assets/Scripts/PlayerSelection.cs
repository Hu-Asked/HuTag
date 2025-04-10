using TMPro;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public int numberOfPlayers = 1;
    public TextMeshProUGUI numberOfPlayersText;
    public void onIncrement()
    {
        numberOfPlayers++;
        if (numberOfPlayers > 4)
        {
            numberOfPlayers = 1;
        }
        numberOfPlayersText.text = numberOfPlayers.ToString();
    }
    public void onDecrement()
    {
        numberOfPlayers--;
        if (numberOfPlayers < 1)
        {
            numberOfPlayers = 4;
        }
        numberOfPlayersText.text = numberOfPlayers.ToString();
    }
}
