using TMPro;
using UnityEngine;

public class TimeSelection : MonoBehaviour
{
    public float gameTime = 90f;
    public float incrementCount = 15f;
    public TextMeshProUGUI gameTimeText;
    public void onIncrement()
    {
        gameTime += incrementCount;
        if (gameTime > 180f)
        {
            gameTime = 15f;
        }
        gameTimeText.text = gameTime.ToString();
    }
    public void onDecrement()
    {
        gameTime -= incrementCount;
        if (gameTime <= 0f)
        {
            gameTime = 4;
        }
        gameTimeText.text = gameTime.ToString();
    }
}
