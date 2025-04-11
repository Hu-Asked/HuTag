using TMPro;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public int mapNumber = 1;
    public int numMaps = 1;
    public TextMeshProUGUI mapNumberText;
    public void onIncrement()
    {
        mapNumber++;
        if (mapNumber > numMaps)
        {
            mapNumber = 0;
        }
        mapNumberText.text = mapNumber.ToString();
        if (mapNumber == 0)
        {
            mapNumberText.text = "Random";
        }
    }
    public void onDecrement()
    {
        mapNumber--;
        if (mapNumber < 0)
        {
            mapNumber = numMaps;
        }
        mapNumberText.text = mapNumber.ToString();
        if (mapNumber == 0)
        {
            mapNumberText.text = "Random";
        }
    }
}
