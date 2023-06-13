using UnityEngine;

public class Eyes : MonoBehaviour
{
    public GameObject nearestFood;
    public static Eyes instance;
    public const float eyeLeft = -0.05f;
    public const float eyeRight = 0.05f;
    public const float eyeUp = 0.15f;
    public const float eyeDown = 0.05f;
    public const float lerpTime = 5f;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        nearestFood = null;
    }

    public void Update()
    {
        if(nearestFood != null)
            transform.localPosition = Vector3.Lerp(transform.localPosition, GetPositionHeadFromFood(), Time.deltaTime * lerpTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, eyeUp, 0), Time.deltaTime * lerpTime);
    }

    private Vector2 GetPositionHeadFromFood()
    {
        Vector2 position = new(0, 0.1f);
        Vector2 distance = FoodManager.instance.foodPosition - SnakeManager.instance.snakeNodes[0];
        switch(distance.x)
        {
            case 1:
                switch(SnakeManager.instance.snakeRotation)
                {
                    case 0:
                        position.y = eyeUp;
                        break;
                    case 1:
                        position.y = eyeDown;
                        break;
                    case 2:
                        position.x = eyeRight;
                        break;
                    case 3:
                        position.x = eyeLeft;
                        break;
                }
                break;
            case -1:
                switch(SnakeManager.instance.snakeRotation)
                {
                    case 0:
                        position.y = eyeDown;
                        break;
                    case 1:
                        position.y = eyeUp;
                        break;
                    case 2:
                        position.x = eyeLeft;
                        break;
                    case 3:
                        position.x = eyeRight;
                        break;
                }
                break;
            default:
                break;
        }
        switch(distance.y)
        {
            case 1:
                switch(SnakeManager.instance.snakeRotation)
                {
                    case 0:
                        position.x = eyeLeft;
                        break;
                    case 1:
                        position.x = eyeRight;
                        break;
                    case 2:
                        position.y = eyeUp;
                        break;
                    case 3:
                        position.y = eyeDown;
                        break;
                }
                break;
            case -1:
                switch(SnakeManager.instance.snakeRotation)
                {
                    case 0:
                        position.x = eyeRight;
                        break;
                    case 1:
                        position.x = eyeLeft;
                        break;
                    case 2:
                        position.y = eyeDown;
                        break;
                    case 3:
                        position.y = eyeUp;
                        break;
                }
                break;
            default:
                break;
        }
        return position;
    }
}
