using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTargeter : MonoBehaviour
{
    public enum TARGET_COLOR
    {
        BLACK,
        RED,
        GREEN,
        BLUE
    }

    public TARGET_COLOR targetColor = TARGET_COLOR.BLACK;
    public Material[] targetColorMaterials;

    private Renderer hintRenderer;
    private int prevColorIndex = -1;

    private void Start()
    {
        hintRenderer = transform.Find("Hint").GetComponent<Renderer>();
    }

    public void TargetingColor()
    {
        int currentColorIndex;
        do
        {
            currentColorIndex = Random.Range(0, targetColorMaterials.Length);
        }
        while (prevColorIndex == currentColorIndex);

        prevColorIndex = currentColorIndex;

        targetColor = (TARGET_COLOR)currentColorIndex;
        hintRenderer.material = targetColorMaterials[currentColorIndex];
    }
}
