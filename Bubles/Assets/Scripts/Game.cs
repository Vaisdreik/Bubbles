using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject bublePrefab;
    public int playerScore = 0;
    public float gameTime = 30;
    public float bubbleSpawnTimeInSec = 1;
    [Range(5,15)]public float bubbleSpeedFromTimeModifer = 10;
    public bool isEndGame = false;

    private float _currentTime;

    public float CurrentTime
    {
        get { return _currentTime; }
        set { _currentTime = value; }
    }

    void Start()
    {
        _currentTime = gameTime;
        StartCoroutine("SpawnBubbles");
    }

    void Update()
    {
        if (isEndGame) return;

        CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0)
        {
            StopAllCoroutines();
            isEndGame = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null)
            {
                Bubble bubble = hit.transform.gameObject.GetComponent<Bubble>();
                if (bubble == null) return;
                playerScore += bubble.GetScore;
                bubble.Burst();
            }
        }
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            GameObject bubble = Instantiate(bublePrefab);
            bubble.transform.SetParent(transform);
            bubble.GetComponent<Bubble>().Move(1f+(gameTime-CurrentTime)/bubbleSpeedFromTimeModifer);
            yield return new WaitForSeconds(bubbleSpawnTimeInSec);
        }
    }

}
