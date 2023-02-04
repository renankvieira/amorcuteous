using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSprite : GenericActionCaller
{
    [Header("Toggle Data")]
    public Sprite[] sprites;

    [Header("Behaviour")]
    public bool changeSprite;

    [Header("References")]
    public UnityEngine.UI.Image _image;
    public SpriteRenderer _spriteRenderer;

    public override void MethodToCall()
    {
        base.MethodToCall();
        ChangeGraphics();
    }

    public void ChangeGraphics()
    {
        Sprite selectedSprite = sprites.GetRandom();
        if (_image != null)
            if (changeSprite)
                _image.sprite = selectedSprite;

        if (_spriteRenderer != null)
            if (changeSprite)
                _spriteRenderer.sprite = selectedSprite;

        if (_image == null && _spriteRenderer == null)
            Debug.LogWarning("[CSBC] Image and SpriteRenderer references not set.", gameObject);
    }
}
