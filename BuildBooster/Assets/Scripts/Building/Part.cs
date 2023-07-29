using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Material material;
    public MaterialPropertyBlock materialblock;
    public Renderer partRenderer;
    private void Start()
    {
        partRenderer = GetComponent<Renderer>();
    }
    public void ChangeColor(string colorCode)
    {
        Color newColor = new Color();
        materialblock = new MaterialPropertyBlock();
        partRenderer.GetPropertyBlock(materialblock);
        ColorUtility.TryParseHtmlString(colorCode, out newColor);
        materialblock.SetColor("_Color", newColor);
        partRenderer.SetPropertyBlock(materialblock);
    }
}
