using UnityEngine;

public class KeepMusicPlaying : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(GameObject.FindGameObjectWithTag("Music"));
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GameObject.Find("BGMusicPlayer").GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}