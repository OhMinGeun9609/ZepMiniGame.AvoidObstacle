using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    StageManager stageManager;
    MiniGameManager gm;
    List<Sprite> sprites;

    public int numBgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    private List<Obstacle> obstaclesList = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        gm = MiniGameManager._instance;
        stageManager = StageManager._instance;

        obstaclesList = FindObjectsOfType<Obstacle>().ToList<Obstacle>();
        obstacleLastPosition = obstaclesList[0].transform.position;
        obstacleCount = obstaclesList.Count;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstaclesList[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
