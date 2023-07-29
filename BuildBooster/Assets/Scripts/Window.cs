using System;
using UnityEngine;

[Serializable]
public class Window
{
    public GameObject windowReference;
    public float width;
    public float height;
    public float leftOffset;
    public float bottomOffset;

    public Window()
    {
        windowReference = null;
        width = 0.0f;
        height = 0.0f;
        leftOffset = 0.0f;
        bottomOffset = 0.0f;
    }

    public Window(GameObject window, float width, float height, float leftOffset, float bottomOffset)
    {
        this.windowReference = window;
        this.width = width;
        this.height = height;
        this.leftOffset = leftOffset;
        this.bottomOffset = bottomOffset;
    }


    public string GetWindowData()
    {
        string data = "{ windowReference: " + windowReference.name + 
                        ", width: " +  width +
                        ", height: " + height +
                        ", leftOffset: " + leftOffset +
                        ", bottomOffset: " + bottomOffset + " }";

        return data;
    }

}