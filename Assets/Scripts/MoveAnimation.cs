using System;
using DG.Tweening;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] private bool local;
    [SerializeField] private Direction direction;
    [SerializeField] private float endPosition;
    [SerializeField] private float duration;
    [SerializeField] private int loops;
    [SerializeField] private LoopType loopType;
    [SerializeField] private Ease ease;
    
    
    private Vector3 initialPosition;

    public void StartAnimationMove(bool isLocal, Direction dir,float endPos ,float dur, int loop, LoopType loopTy, Ease easeType)
    {
        initialPosition = isLocal ? transform.localPosition : transform.position;
        
        switch (dir)
        {
            case Direction.X:
                AnimationMove(new Vector3(initialPosition.x + endPos, initialPosition.y, initialPosition.z));
                break;
            case Direction.Y:
                AnimationMove(new Vector3(initialPosition.x, initialPosition.y + endPos, initialPosition.z));
                break;
            case Direction.Z:
                AnimationMove(new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + endPos));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        void AnimationMove(Vector3 dirVector3)
        {
            if (isLocal)
            {
                transform.DOLocalMove(dirVector3, dur).SetLoops(loop, loopTy).SetEase(easeType);
                return;
            }
            
            transform.DOMove(dirVector3, dur).SetLoops(loop, loopTy).SetEase(easeType);
        }
    }
    
    public void StartAnimationMove() => StartAnimationMove(local, direction, endPosition ,duration, loops, loopType, ease);

    public void StopAnimation()
    {
        transform.DOKill();
        transform.position = initialPosition;
    }

    public void StopMove() => transform.DOKill();
}

public enum Direction
{
    X,
    Y,
    Z,
}