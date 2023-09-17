using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Banner : MonoBehaviour
{
    [Header("Objects for open ro close")]
    [SerializeField] private Behaviour[] showHideObjects;
    [Space]
    [Header("following object")]
    [SerializeField] private Transform followObject;
    [Space]
    [Header("animation")]
    [SerializeField] private MoveAnimation moveAnimation;
    [Space]
    [Header("text for write")]
    [SerializeField] private WriteText writeText;
    [SerializeField] private float writeDuration = 20;
    [SerializeField] private TextAsset textAsset;

    private bool useAnimation;
    private CancellationTokenSource cts = new();

    public Transform FollowObject
    {
        get => followObject;
        set => followObject = value;
    }
    
    private void Awake() => useAnimation = moveAnimation != null;

    private void Update()
    {
        transform.position = followObject.position;
        transform.LookAt(Camera.main.transform);
    }

    public void Show(float durationWrite = 0, TextAsset text = null, float showDelay = 0)
    {
        if (durationWrite != 0) writeDuration = durationWrite;
        if (text != null) textAsset = text;

        if (showDelay != 0)
        {
            UniTask.RunOnThreadPool(() => ShowAsync(showDelay)).Forget();
            return;
        }
        
        Animate();
    }

    private async UniTask ShowAsync(float delay)
    {
        cts = new CancellationTokenSource();

        await UniTask.WaitForSeconds(delay, cancellationToken: cts.Token);

        Animate();
    }

    public void StopShowAsync()
    {
        cts.Cancel(); 
        cts.Dispose();
    }

    private void Animate()
    {
        foreach (var sho in showHideObjects)
        {
            sho.enabled = true;
        }

        writeText.Write(writeDuration, textAsset);
        
        if(useAnimation is false) return;
        
        moveAnimation.StartAnimationMove();
    }

    public void Hide()
    {
        writeText.StopWriteAndClear();
        
        StopShowAsync();

        foreach (var sho in showHideObjects)
        {
            sho.enabled = false;
        }

        if(useAnimation is false) return;
        
        moveAnimation.StopAnimation();
    }
}