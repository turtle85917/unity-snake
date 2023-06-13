using UnityEngine;

public class EnterArea : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.transform.tag.Equals("Head"))
            return;
        GameManager.instance.snakeHead = GameManager.instance.SnakeHeadOpen;
        Eyes.instance.nearestFood = transform.parent.gameObject;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.transform.tag.Equals("Head"))
            return;
        // 뱀의 머리에서 바라보는 방향에 먹이가 있는가?
        if(SnakeManager.instance.snakeNodes[0] + SnakeManager.instance.GetSnakeRotationToVector2(SnakeManager.instance.snakeRotation) != FoodManager.instance.foodPosition)
            GameManager.instance.snakeHead = GameManager.instance.SnakeHead;
        if(Eyes.instance.nearestFood == null)
            Eyes.instance.nearestFood = transform.parent.gameObject;
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.transform.tag.Equals("Head"))
            return;
        GameManager.instance.snakeHead = GameManager.instance.SnakeHead;
        Eyes.instance.nearestFood = null;
    }
}
