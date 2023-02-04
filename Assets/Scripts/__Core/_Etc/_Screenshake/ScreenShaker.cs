using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

public class ScreenShaker : GenericActionCaller
{
    /*
    [Header("ScreenShaker")]
    [TextArea] public string description = "OptionalDescription";
    public ScreenShakeChannel channel;
    public bool destroyAfterShake = false;

    protected override void Awake()
    {
        channel.Initialize();
        base.Awake();
    }

    public override void MethodToCall()
    {
        base.MethodToCall();
        Play();
    }

    [Button]
    public void Play()
    {
        channel.Shake();
        if (destroyAfterShake)
            Destroy(gameObject, channel.duration);
    }

    [Button]
    public void ResetChannel()
    {
        channel.UpdateTween();
    }

    [Button]
    public void Reinit()
    {
        channel.Reinit();
    }

    [Button]
    public void Repeat()
    {
        StartCoroutine(RepeatCoroutine());
    }

    IEnumerator RepeatCoroutine()
    {
        while(true)
        {
            channel.Shake();
            yield return new WaitForSeconds(1f);
        }
    }


    [System.Serializable]
    public class ScreenShakeChannel
    {
        [Header("Tween Parameters")]
        public TweenChoice tweenChoice;
        [Range(0f, 2f)] public float strengthMultiplier = 0.2f;
        [Range(0f, 3f)] public float duration = 0.25f;
        [Range(0, 100)] public int vibrato = 20;
        [Range(0, 360f)] public float randomness = 90f;
        public Vector3 strengthAxes = Vector3.one;

        [Header("Etc")]
        public Transform customTransformToShake;
        public bool fadeOut = true;

        public Vector3 finalStrength => strengthAxes * strengthMultiplier;

        Tween shakeTween;

        public enum TweenChoice
        {
            SHAKE_POSITION = 0,
            SHAKE_ROTATION = 10
        }

        bool initialized = false;

        Transform TransformToShake => customTransformToShake == null ? Camera.main.transform : customTransformToShake;

        public void Initialize()
        {
            if (initialized)
                return;

            initialized = true;
            if (tweenChoice == TweenChoice.SHAKE_POSITION)
                shakeTween = TransformToShake.DOShakePosition(duration, finalStrength, vibrato, randomness, false, fadeOut)
                    .Pause()
                    .SetAutoKill(false);
            else if (tweenChoice == TweenChoice.SHAKE_ROTATION)
                shakeTween = TransformToShake.DOShakeRotation(duration, finalStrength, vibrato, randomness, fadeOut)
                    .Pause()
                    .SetAutoKill(false);

            shakeTween.ForceInit();
        }

        public void Reinit()
        {
            initialized = true;
            if (tweenChoice == TweenChoice.SHAKE_POSITION)
                shakeTween = TransformToShake.DOShakePosition(duration, finalStrength, vibrato, randomness, false, fadeOut)
                .Pause()
                .SetAutoKill(false);
            else if (tweenChoice == TweenChoice.SHAKE_ROTATION)
                shakeTween = TransformToShake.DOShakeRotation(duration, finalStrength, vibrato, randomness, fadeOut)
                    .Pause()
                    .SetAutoKill(false);

            shakeTween.ForceInit();
        }

        public void Shake()
        {
            // IMPORTANT: If the channel is initialized during another screenshake, its default position will the camera current position.
            if (!initialized)
            {
                //Debug.LogWarning("IMPORTANT: Running an uninitialized tween! It must be initialized ahead of time to avoid bugs.");
                Initialize();
            }

            shakeTween.Restart();
        }

        [Button]
        public void UpdateTween()
        {
            shakeTween?.Complete(true);

            if (tweenChoice == TweenChoice.SHAKE_POSITION)
                shakeTween = TransformToShake.DOShakePosition(duration, finalStrength, vibrato, randomness, false, fadeOut).Pause().SetAutoKill(false);
            else if (tweenChoice == TweenChoice.SHAKE_POSITION)
                shakeTween = TransformToShake.DOShakeRotation(duration, finalStrength, vibrato, randomness, fadeOut).Pause().SetAutoKill(false);

            shakeTween.ForceInit();
        }
    }*/
}
