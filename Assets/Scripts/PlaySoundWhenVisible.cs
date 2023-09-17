using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundWhenVisible : MonoBehaviour
{
    [SerializeField] private DefaultObserverEventHandler defaultObserverEventHandler;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        defaultObserverEventHandler.OnTargetFound.AddListener(audioSource.Play);
        defaultObserverEventHandler.OnTargetLost.AddListener(audioSource.Stop);
    }
}