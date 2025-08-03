using UnityEngine;
using System;
public enum SoundType
{
    LAND,
    LAUNCH,
    SPRING,
    ROLL,
    RING,
    REVV,
    BRAKE,
    GROUNDPOUND,
    TITLE,
    CONE,
    Level1,
    FINISH,

}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private float BGMVolume = .25f;
    [SerializeField] private SoundList[] soundList;
    [SerializeField] private SoundType StartingTrack;

    private static SoundManager instance;
    private AudioSource[] AudioSources;
    private AudioSource SFXSource, BGMSource, EFXSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            AudioSources = GetComponents<AudioSource>();
            if (AudioSources.Length == 3)
            {
                SFXSource = AudioSources[0];
                BGMSource = AudioSources[1];
                EFXSource = AudioSources[2];
            }

            else
            {
                Debug.Log("There is not 2 audiosources, there is " + AudioSources.Length);
            }


            PlayMusic(StartingTrack, BGMVolume);
        }
    }


    private void Update()
    {
        if (Application.isPlaying)
        {
            if (!BGMSource.isPlaying)
            {
                PlayMusic(StartingTrack, .25f);
            }
        }
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.SFXSource.PlayOneShot(randomClip, volume);
    }

    public static void PlayRandomSoundPitch(SoundType sound, float volume = 1.0f, bool interupting = false, float minPitch = 0.9f, float maxPitch = 1.3f)
    {

        if (instance.SFXSource.isPlaying && !interupting) return;
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        float originalPitch = instance.SFXSource.pitch;
        instance.SFXSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        instance.SFXSource.PlayOneShot(randomClip, volume);
        instance.SFXSource.pitch = originalPitch;
    }
    public static void PlayWithSoundPitch(SoundType sound, float volume = 1.0f, bool interupting = false, float pitch = 1f)
    {

        if (instance.SFXSource.isPlaying && !interupting) return;
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        float originalPitch = instance.SFXSource.pitch;
        instance.SFXSource.pitch = pitch;
        instance.SFXSource.PlayOneShot(randomClip, volume);
        instance.SFXSource.pitch = originalPitch;
    }

    // For environment sounds so it doesn't interupt player sounds
    public static void PlayEFXRandomSoundPitch(SoundType sound, float volume = 1.0f, bool interupting = false, float minPitch = 0.9f, float maxPitch = 1.3f)
    {

        if (instance.EFXSource.isPlaying && !interupting) return;
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        float originalPitch = instance.EFXSource.pitch;
        instance.EFXSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        instance.EFXSource.PlayOneShot(randomClip, volume);
        instance.EFXSource.pitch = originalPitch;
    }

    public static void PlayEFXWithSoundPitch(SoundType sound, float volume = 1.0f, bool interupting = false, float pitch = 1f)
    {

        if (instance.EFXSource.isPlaying && !interupting) return;
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        float originalPitch = instance.EFXSource.pitch;
        //instance.SFXSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        instance.EFXSource.pitch = pitch;
        instance.SFXSource.PlayOneShot(randomClip, volume);
        instance.SFXSource.pitch = originalPitch;
    }
    public static void KillSFX()
    {

        instance.SFXSource.Stop();
    }



    public static void PlayMusic(SoundType sound, float volume = 1.0f)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.BGMSource.PlayOneShot(randomClip, volume);
    }


#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}

