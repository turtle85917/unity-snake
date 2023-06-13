using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text Score;
    public Sprite SnakeHead;
    public Sprite SnakeHeadOpen;
    public Sprite snakeHead;
    public static GameManager instance;
    public const int mapSizeX = 9;
    public const int mapSizeY = 5;
    public const float step = 1.5f;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        snakeHead = SnakeHead;
    }

    public void Update()
    {
        Score.text = $"<color>{PlayerPrefs.GetInt("score")}</color>점";
    }

    public Vector3 GetPosition(Vector2 position)
    {
        return new Vector3((position.x - mapSizeX / 2 ) * step, (position.y - mapSizeY / 2 ) * step, 0);
    }
}
