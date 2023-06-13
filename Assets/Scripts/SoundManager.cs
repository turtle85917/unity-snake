using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sfxClips;
    public int channels;
    public float sfxVolume;
    public static SoundManager instance;
    public enum Sfx {
        Click,
        Die,
        Eat,
        Move
    };
    private AudioSource[] sfxPlayers;
    private int channelCursor;

    public void Awake()
    {
        instance = this;
        GameObject sfxObject = new("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx(Sfx sfx)
    {
        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelCursor) % sfxPlayers.Length;
            if(sfxPlayers[loopIndex].isPlaying) continue;
            channelCursor = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
