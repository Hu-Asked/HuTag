using System.Security.Cryptography;
using System.Xml.Schema;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] players;
    public float minSize = 5.0f;
    public float padding = 2.0f;
    public float minYMultiplier = 0.5f;
    public float maxYMultiplier = 1.5f;
    public float zoomSpeed = 5.0f;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();   
    }
    void LateUpdate()
    {
        int num = 0;
        Vector2 avgPos = Vector2.zero;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null && players[i].gameObject.activeSelf)
            {
                avgPos += (Vector2)players[i].position;
                num++;
            }
        }
        if(num == 0) return;
        avgPos /= num;
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].gameObject.activeSelf)
            {
                minX = Mathf.Min(minX, players[i].position.x);
                maxX = Mathf.Max(maxX, players[i].position.x);
                minY = Mathf.Min(minY, players[i].position.y);
                maxY = Mathf.Max(maxY, players[i].position.y);
            }
        }
        float sizeX = (maxX - minX) / 2 + padding;
        float sizeY = (maxY - minY) / 2 + padding;
        
        float aspectRatio = Screen.width / (float)Screen.height;
        float targetSize = Mathf.Max(sizeY, sizeX / aspectRatio);
        targetSize = Mathf.Max(targetSize, minSize);
        
        Vector3 targetPosition = new(avgPos.x, Mathf.Clamp(avgPos.y, targetSize * minYMultiplier, targetSize*maxYMultiplier), transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, zoomSpeed * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    }
}
