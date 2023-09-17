using System;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<Description> descriptionList = new();
    [SerializeField] private float durationWrite = 20;
    [SerializeField] private float delayBanner;

    private void Awake()
    {
        foreach (var description in descriptionList)
        {
            description.defaultObserverEventHandler.OnTargetFound.AddListener(() =>
            {
                description.banner.Show(durationWrite, description.textAsset, delayBanner);
                description.banner.FollowObject = description.anchor.transform;
            });

            description.defaultObserverEventHandler.OnTargetLost.AddListener(() => description.banner.Hide());
        }
    }
}

[Serializable]
public struct Description
{
    public GameObject anchor;
    public Banner banner;
    public DefaultObserverEventHandler defaultObserverEventHandler;
    public TextAsset textAsset;
}