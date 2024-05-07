using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private float dayLength;
    
    // how "flat" the peaks and valleys of the day-night cycle are
    [SerializeField] private float flatness;
    
    // The sprite that is to be colored by the day-night cycle
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float flat_cosine(float x)
    {
        float cosine = Mathf.Cos(x);
        float bsq = flatness * flatness;
        return cosine * Mathf.Sqrt((1 + bsq)/(1 + bsq * cosine * cosine));
    }

    private void set_color(float time)
    {
        double t = 0.5 * (1 - flat_cosine((time / dayLength)));
        spriteRenderer.color = Color.Lerp(dayColor, nightColor, (float)t);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
