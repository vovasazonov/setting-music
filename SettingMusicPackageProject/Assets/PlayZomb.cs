using Audio;
using UnityEngine;

public class PlayZomb : MonoBehaviour
{
    public AudioPlayer AudioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}