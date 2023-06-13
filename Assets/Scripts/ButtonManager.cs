using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnPlayClick()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.Save();
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Click);
        SceneManager.LoadScene("Game");
    }

    public void OnReplayClick()
    {
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Click);
        SceneManager.LoadScene("Game");
    }
}
