using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeManager : MonoBehaviour
{
    public GameObject Snake;
    public GameObject HeadPrefab;
    public GameObject BodyPrefab;
    public List<Vector2> snakeNodes;
    [Range(0, 3)]
    public int snakeRotation;
    public static SnakeManager instance;
    private Vector2 lastDirection;
    private Vector2[] maxPoint;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        snakeNodes = new List<Vector2>{
            new Vector2(5, 2),
            new Vector2(4, 2),
            new Vector2(3, 2)
        };
        maxPoint = new Vector2[4]{
            new Vector2(GameManager.mapSizeX - 1, 0),
            new Vector2(0, 0),
            new Vector2(0, GameManager.mapSizeY - 1),
            new Vector2(0, 0),
        };
        lastDirection = new Vector2(1, 0);
        foreach(Vector2 node in snakeNodes)
        {
            GameObject prefab = BodyPrefab;
            Quaternion rotation = Quaternion.identity;
            if(node.Equals(GetSnakeHead()))
            {
                prefab = HeadPrefab;
                rotation = Quaternion.Euler(new Vector3(0, 0, GetSnakeRotation()));
            }
            GameObject body = Instantiate(prefab, GameManager.instance.GetPosition(node), rotation);
            body.transform.SetParent(Snake.transform, false);
        }
        FoodManager.instance.CreateFood();
        StartCoroutine(ForwardSnake());
    }

    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 input = new(horizontal == 1 ? 0 : 1, vertical == 1 ? 2 : 3);
        if(horizontal != 0 && CheckVaildKeyDown(GetSnakeRotationToVector2((int)input.x)))
            snakeRotation = (int)input.x;
        if(vertical != 0 && CheckVaildKeyDown(GetSnakeRotationToVector2((int)input.y)))
            snakeRotation = (int)input.y;
        if(FoodManager.instance.Food.transform.childCount == 0)
            FoodManager.instance.CreateFood();
        if(snakeNodes.Count > 4)
        {
            if(snakeNodes.FindAll(Item => Item.Equals(GetSnakeHead())).Count > 1)
            {
                SoundManager.instance.PlaySfx(SoundManager.Sfx.Die);
                SceneManager.LoadScene("GameOver");
            }
        }
        RepositionSnake();
    }

    public Vector2 GetSnakeRotationToVector2(int direction)
    {
        return direction switch
        {
            0 => new Vector2(1, 0),
            1 => new Vector2(-1, 0),
            2 => new Vector2(0, 1),
            3 => new Vector2(0, -1),
            _ => new Vector2(0, 0)
        };
    }

    private void MoveSnake(int direction)
    {
        Vector2 newDirection = GetSnakeRotationToVector2(direction);
        Vector2 head = GetSnakeHead();
        if(CheckWall(direction)) {
            SoundManager.instance.PlaySfx(SoundManager.Sfx.Die);
            SceneManager.LoadScene("GameOver");
            return;
        }
        head += newDirection;
        snakeRotation = direction;
        lastDirection = newDirection;
        snakeNodes.Insert(0, head);
        snakeNodes.RemoveAt(snakeNodes.Count - 1);
    }

    private void RepositionSnake()
    {
        for(int i = 0; i < Snake.transform.childCount; i++)
        {
            Vector2 node = snakeNodes[i];
            Component child = Snake.transform.GetChild(i);
            child.transform.position = Vector3.Lerp(child.transform.position, GameManager.instance.GetPosition(node), Time.deltaTime * 5f);
            if(node.Equals(GetSnakeHead()))
            {
                child.GetComponent<SpriteRenderer>().sprite = GameManager.instance.snakeHead;
                child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetSnakeRotation()));
            }
        }
    }

    private Vector2 GetSnakeHead()
    {
        return snakeNodes[0];
    }

    private int GetSnakeRotation()
    {
        return snakeRotation switch
        {
            0 => 270,
            1 => 90,
            2 => 360,
            3 => 180,
            _ => 0,
        };
    }

    private bool CheckWall(int direction)
    {
        Vector2 newDirection = GetSnakeRotationToVector2(direction);
        Vector2 head = GetSnakeHead();
        Vector2 wallPoint = maxPoint[direction];
        if(newDirection.y == 0 && head.x == wallPoint.x) return true;
        if(newDirection.x == 0 && head.y == wallPoint.y) return true;
        return false;
    }

    private bool CheckVaildKeyDown(Vector2 direction)
    {
        Vector2 dir = lastDirection + direction;
        return dir.x != 0 || dir.y != 0;
    }

    IEnumerator ForwardSnake()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        MoveSnake(snakeRotation);
        StartCoroutine(ForwardSnake());
    }
}
