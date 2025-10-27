using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 2.5f;
    public float holeSizeMax = 4f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    MiniGameManager gameManager;

    private void Start()
    {
        gameManager = MiniGameManager._instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
            gameManager.AddScore(1);
    }
}
