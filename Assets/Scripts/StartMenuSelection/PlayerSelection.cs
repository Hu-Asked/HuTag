using TMPro;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public TextMeshProUGUI numberOfPlayersText;
    public void onIncrement()
    {
        numberOfPlayers++;
        if (numberOfPlayers > 4)
        {
            numberOfPlayers = 2;
        }
        numberOfPlayersText.text = numberOfPlayers.ToString();
    }
    public void onDecrement()
    {
        numberOfPlayers--;
        if (numberOfPlayers < 2)
        {
            numberOfPlayers = 4;
        }
        numberOfPlayersText.text = numberOfPlayers.ToString();
    }
}
