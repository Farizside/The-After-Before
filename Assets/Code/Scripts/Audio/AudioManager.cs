using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
  #region Singleton Instance

  private static AudioManager instance;

  public static AudioManager Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<AudioManager>();
        if (instance == null)
        {
          GameObject audioManagerObject = new GameObject("AudioManager");
          instance = audioManagerObject.AddComponent<AudioManager>();
          DontDestroyOnLoad(audioManagerObject);
        }
      }
      return instance;
    }
  }

  #endregion

  #region Audio Sources

  [SerializeField]
  private AudioSource _musicSource;
  [SerializeField]
  private AudioSource _soundEffectSource;

  #endregion

  #region Audio Libraries

  [SerializeField]
  private MusicLibrary _musicLibrary;
  [SerializeField]
  private SoundLibrary _soundLibrary;

  #endregion

  [SerializeField]
  private AudioMixer _audioMixer; // Assign directly in the Inspector

  public AudioMixerSnapshot Paused;
  public AudioMixerSnapshot Unpaused;
  private bool _isPaused = false;

  private void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
      return;
    }
    instance = this;
    DontDestroyOnLoad(gameObject);
  }

  #region Sound Effects

  public void PlaySound3D(AudioClip clip, Vector3 position)
  {
    if (clip != null)
    {
      _soundEffectSource.clip = clip;
      _soundEffectSource.transform.position = position;
      _soundEffectSource.Play();
    }
  }

  public void PlaySound3D(string soundName, Vector3 position)
  {
    if (_soundLibrary != null)
    {
      AudioClip clip = GetClipFromLibrary(soundName, _soundLibrary);
      if (clip != null)
      {
        PlaySound3D(clip, position);
      }
    }
    else
    {
      Debug.LogError("Sound library not assigned to AudioManager!");
    }
  }
    public void PlaySound2D(string soundName)
    {
        if (_soundLibrary != null)
        {
            AudioClip clip = GetClipFromLibrary(soundName, _soundLibrary);
            if (clip != null)
            {
                _soundEffectSource.clip = clip;
                _soundEffectSource.Play();
            }
        }
        else
        {
            Debug.LogError("Sound library not assigned to AudioManager!");
        }
    }

    #endregion

    #region Music (Optional)

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
  {
    if (_musicLibrary != null)
    {
      StartCoroutine(AnimateMusicCrossfade(GetClipFromLibrary(trackName, _musicLibrary), fadeDuration));
    }
    else
    {
      Debug.LogError("Music library not assigned to AudioManager!");
    }
  }

  private IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration)
  {
    float percent = 0f;
    while (percent < 1f)
    {
      percent += Time.deltaTime / fadeDuration;
      _musicSource.volume = Mathf.Lerp(1f, 0f, percent);
      yield return null;
    }

    _musicSource.clip = nextTrack;
    _musicSource.Play();
    percent = 0f;

    while (percent < 1f)
    {
      percent += Time.deltaTime / fadeDuration;
      _musicSource.volume = Mathf.Lerp(0f, 1f, percent);
      yield return null;
    }
  }

  #endregion

  private AudioClip GetClipFromLibrary(string name, object library)
  {
    if (library != null)
    {
      if (library is MusicLibrary musicLib)
      {
        return musicLib.GetClipFromName(name);
      }
      else if (library is SoundLibrary soundLib)
      {
        return soundLib.GetClipFromName(name);
      }
      else
      {
        Debug.LogError("Invalid library type passed to GetClipFromLibrary!");
        return null;
      }
    }
    else
    {
      Debug.LogError("Library not assigned to AudioManager!");
      return null;
    }
  }

  public void PauseAudio()
  {
    if (_audioMixer != null)
    {
      Paused.TransitionTo(.001f); // Transition to Pause Snapshot with a short duration
    }
    _isPaused = true;
  }
  public void ResumeAudio()
  {
    if (_audioMixer != null)
    {
      Unpaused.TransitionTo(.001f); // Transition to Unpaused Snapshot with a short duration
    }
    _isPaused = false;
  }

  public void Update()
  {
    if (_audioMixer != null)
    {
      string currentSceneName = SceneManager.GetActiveScene().name;

      if (currentSceneName == "Gameplay") 
      {
        if (_isPaused)
        {
          PauseAudio();
        }
        else
        {
          ResumeAudio();
        }
      }
    }
  }
}