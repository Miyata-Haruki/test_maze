using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMake : MonoBehaviour
{
    int mapSize = 7;
    int[,] maze;
    int cnt = 0;

    [SerializeField]
    GameObject wallObject;
    [SerializeField]
    GameObject groundObject;
    [SerializeField]
    GameObject GoalObject;

    // Start is called before the first frame update
    void Start()
    {
        float wall_x = wallObject.GetComponent<Transform>().localScale.x;
        float wall_y = wallObject.GetComponent<Transform>().localScale.z;
        float wall_h = wallObject.GetComponent<Transform>().localScale.y;
        float ground_h = groundObject.GetComponent<Transform>().localScale.y;
        float goal_h = GoalObject.GetComponent<Transform>().localScale.y;
        int endNum = ((mapSize + 1) / 2) * ((mapSize + 1) / 2) - 1;
        maze = new int[mapSize + 2, mapSize + 2];
        while (endNum > cnt)
        {
            int x = Random.Range(0, (mapSize + 1) / 2) * 2;
            int y = Random.Range(0, (mapSize + 1) / 2) * 2;
            if (cnt == 0) maze[x + 1, y + 1] = 1;
            if (maze[x + 1, y + 1] == 1) WallDig(x, y, 0);
        }
        Output(wall_x, wall_y, wall_h, ground_h);
        Instantiate(GoalObject, new Vector3((float)mapSize * wall_x, 0f - (wall_h / 2) + (goal_h / 2), (float)mapSize * wall_y), Quaternion.identity);
    }

    void WallDig(int x, int y, int oldVec)
    {
        int[] vx = { 0, 2, 0, -2 };
        int[] vy = { -2, 0, 2, 0 };
        bool retFlg = false;
        int r = Random.Range(0, 4);
        if (r == 0 && y <= 0) retFlg = true;
        if (r == 1 && (x + 2) >= mapSize) retFlg = true;
        if (r == 2 && (y + 2) >= mapSize) retFlg = true;
        if (r == 3 && x <= 0) retFlg = true;

        if (retFlg)
        {
            WallDig(x, y, oldVec);
            return;
        }

        if (maze[x + 1 + vx[r], y + 1 + vy[r]] == 0)
        {
            maze[x + 1 + vx[r], y + 1 + vy[r]] = 1;
            maze[x + 1 + vx[r] / 2, y + 1 + vy[r] / 2] = 1;
            cnt++;

            WallDig(x + vx[r], y + vy[r], r);
        }
    }

    void Output(float wall_x, float wall_y, float wall_h, float ground_h)
    {
        GameObject obj = new GameObject();
        obj.name = "Mize";
        for (int x = 0; x < mapSize + 2; x++)
        {
            for (int y = 0; y < mapSize + 2; y++)
            {
                if (maze[x, y] == 0)
                {
                    Instantiate(wallObject, new Vector3((float)x * wall_x, 0f, (float)y * wall_y), Quaternion.identity).transform.parent = obj.transform;
                }
                else
                {
                    Instantiate(groundObject, new Vector3((float)x * wall_x, 0f - (wall_h / 2) - (ground_h / 2), (float)y * wall_y), Quaternion.identity).transform.parent = obj.transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
