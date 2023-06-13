using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject Food;
    public GameObject FoodPrefab;
    public Vector2 foodPosition;
    public bool eatFood;
    public static FoodManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if(eatFood)
        {
            eatFood = false;
            Vector2 bodyPos = SnakeManager.instance.snakeNodes[^1];
            SnakeManager.instance.snakeNodes.Add(bodyPos);
            GameObject body = Instantiate(SnakeManager.instance.BodyPrefab, GameManager.instance.GetPosition(bodyPos), Quaternion.identity);
            body.transform.SetParent(SnakeManager.instance.Snake.transform, false);
        }
    }

    public void CreateFood()
    {
        foodPosition = GetRandom();
        GameObject apple = Instantiate(
            FoodPrefab,
            GameManager.instance.GetPosition(foodPosition),
            Quaternion.identity
        );
        apple.transform.SetParent(Food.transform, false);
    }

    public Vector2 GetRandom()
    {
        Vector2 newPos = foodPosition;
        while(SnakeManager.instance.snakeNodes.Equals(newPos) || foodPosition.Equals(newPos))
            newPos = new Vector2(Random.Range(0, GameManager.mapSizeX), Random.Range(0, GameManager.mapSizeY));
        return newPos;
    }
}
