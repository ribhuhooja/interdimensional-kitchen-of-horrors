using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

// allows you to color an object based on the day night cycle, with some flexibility
// needs to reference a sprite renderer to function. If none is given, it will revert
// to the sprite renderer component on whatever it's attached to. If it still doesn't find
// anything, it will throw an exception
public class DayNightCycleColorer : MonoBehaviour
{

    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    private float dayLength;
    [SerializeField] private float offsetPercentage;
    
    // how "flat" the peaks and valleys of the day-night cycle are
    [SerializeField] private float flatness;
    
    // The sprite that is to be colored by the day-night cycle
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    // CURRENTLY DISABLED
    // the whole flatcos calculation is expensive, so for many 
    // renderers if they have the same, common values they can
    // just use this static value
    // private static float CommonInterpolationPercentage = 0;

    private void Awake()
    {
        if (spriteRenderer == null && !TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            throw new NullReferenceException("no sprite renderer for Day-Night to color");
        }

        dayLength = GameManager.Instance.DayLength;
    }

    private void OnEnable()
    {
        GameManager.Instance.SubscribeToPerFrame(set_color);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnsubscribeFromPerFrame(set_color);
    }


    // see this discussion here:
    // https://math.stackexchange.com/questions/100655/cosine-esque-function-with-flat-peaks-and-valleys
    private static float flat_cosine(float x, float flatness)
    {
        float cosine = Mathf.Cos(x);
        float bsq = flatness * flatness;
        return cosine * Mathf.Sqrt((1 + bsq)/(1 + bsq * cosine * cosine));
    }

    private void set_color(float time)
    {
        // to get smaller numbers
        time %= (2*dayLength);
        // to start at the beginning of the day instead of the middle
        time -= offsetPercentage * dayLength;
        // the function has a period of pi, but we want it to be dayLength
        // So we multiply the argument by pi
        double t = 0.5 * (1 - flat_cosine((Mathf.PI * time / dayLength), flatness));
        
        spriteRenderer.color = Color.Lerp(dayColor, nightColor, (float)t);
    }

}
