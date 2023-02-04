using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

#if FEEL
using MoreMountains.Feedbacks;

public class ScreenShakerForFeel : MonoBehaviour
{
    [Header("ScreenShaker")]
    public ScreenShakeChannel channel;
    public bool destroyAfterShake = false;

    MMFeedbacks feedbacks;

    void Start()
    {
        feedbacks = GetComponent<MMFeedbacks>();
        if (feedbacks == null)
        {
            Debug.LogWarning("[SSFF] No feedback found: " + gameObject.name, gameObject);
            return;
        }

        for (int i = 0; i < feedbacks.Feedbacks.Count; i++)
            if (feedbacks.Feedbacks[i].Label == "Camera Shake")
                feedbacks.Feedbacks[i].onPlay += Play;

        channel.Initialize();
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

    [System.Serializable]
    public class ScreenShakeChannel
    {
        [Range(0f, 2f)] public float strengthMultiplier = 0.05f;
        [Range(0f, 3f)] public float duration = 0.25f;
        [Range(0, 100)] public int vibrato = 20;
        [Range(0, 360f)] public float randomness = 90f;
        public Vector3 strengthAxes = Vector3.one;
        public bool fadeOut = true;

        public Vector3 finalStrength => strengthAxes * strengthMultiplier;

        Tween shakeTween;
        bool initialized = false;

        public void Initialize()
        {
            if (initialized)
                return;

            initialized = true;
            shakeTween = Camera.main.DOShakePosition(duration, finalStrength, vibrato, randomness, fadeOut)
                .Pause()
                .SetAutoKill(false);
            shakeTween.ForceInit();
        }

        public void Reinit()
        {
            initialized = true;
            shakeTween = Camera.main.DOShakePosition(duration, finalStrength, vibrato, randomness, fadeOut)
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

            shakeTween = Camera.main.DOShakePosition(duration, finalStrength, vibrato, randomness, fadeOut).Pause().SetAutoKill(false);
            shakeTween.ForceInit();
        }
    }
}
#endif