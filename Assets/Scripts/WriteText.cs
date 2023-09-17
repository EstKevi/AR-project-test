using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WriteText : MonoBehaviour
{
    [SerializeField] private TextAsset text;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    
    private CancellationTokenSource cts = new();
    
    public void Write(float duration, TextAsset textAsset = null)
    {
        if (textAsset != null)
        {
            text = textAsset;
        }

        if (text == null) throw new NullReferenceException("textAsset is null");
        
        WriteTextAnimation(duration);
    }
    
    public void StopWrite()
    {
        cts.Cancel(); 
        cts.Dispose();
    }

    public void StopWriteAndClear()
    {
        StopWrite();
        Clear();
    }
    
    public void Clear() => textMeshProUGUI.text = string.Empty;
    
    private async UniTask WriteTextAnimation(float duration)
    {
        cts = new CancellationTokenSource();

        var letters = text.ToString();
        var writeSpeed = duration / letters.Length;
        
        try
        {
            foreach (var t in letters)
            {
                textMeshProUGUI.text += t;
                await UniTask.WaitForSeconds(writeSpeed, cancellationToken: cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("uniTask is cancel");
        }
        finally
        {
            cts.Dispose();
            cts = null;
        }
    }
}