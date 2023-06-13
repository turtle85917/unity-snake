using UnityEngine;

public class EatFood : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.transform.tag.Equals("Head"))
            return;
        FoodManager.instance.eatFood = true;
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 500);
        PlayerPrefs.Save();
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Eat);
        Destroy(gameObject);
    }
}
