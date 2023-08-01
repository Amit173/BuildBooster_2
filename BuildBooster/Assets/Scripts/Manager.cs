using System.Runtime.InteropServices;
using UnityEngine;
public class Manager : MonoBehaviour
{
    //[DllImport("__Internal")]
    //public static extern string StringReturnValueFunction(string b);
    public static Manager instance;
    [SerializeField]
    public Part currentSelectedPart = null;
    public float width;
    public float length;
    public float height;
    public float roofPitch;
    [SerializeField]
    private Building wareHouse;

    public Material m_Wall;
    public Material m_Roof;
    public Material m_Trim;
    public Material m_Wainscot;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
        WebGLInput.captureAllKeyboardInput = false;
#endif
    }

    public void SetCurrentPart(Part part)
    {
        currentSelectedPart = part;
        //if (part != null)
        //{
        //    part = null;
        //    currentSelectedPart = part;
        //} else
        //{
            
        //}
    }

    public void MakeWareHouse(string value)
    {
        string[] arr = value.Split(',');
        width = float.Parse(arr[0]);
        length = float.Parse(arr[1]);
        height = float.Parse(arr[2]);
        roofPitch = float.Parse(arr[3]);
        wareHouse.BuildingMaker(width, length, height, roofPitch);
    }
    public void MakeBuilding()
    {
        wareHouse.BuildingMaker(width,length,height,roofPitch);
    }

    public void ChangeColor(string partName, string colorCode)
    {
        switch(partName)
        {
            case "Roof":
                {
                    wareHouse.SetRoofColor(colorCode);
                    break;
                }
            case "Walls":
                {
                    wareHouse.SetWallColor(colorCode);
                    break;
                }
            case "Wainscoat":
                {
                    wareHouse.SetWainscotColor(colorCode); 
                    break;
                }
            case "Trim":
                {
                    wareHouse.SetTrimColor(colorCode);
                    break;
                }
        }
    }

    public void ChangeTexture(string partName, string textureName)
    {
        switch (partName)
        {
            case "Roof":
                {
                    wareHouse.SetRoofTexture(textureName);
                    break;
                }
            case "Walls":
                {
                    wareHouse.SetWallTexture(textureName);
                    break;
                }
            case "Wainscoat":
                {
                    wareHouse.SetWainscotTexture(textureName);
                    break;
                }
        }
    }

    //Method For Java fuction call
    //    void CallJavaScriptFunction(bool b)
    //    {
    //#if UNITY_WEBGL && !UNITY_EDITOR
    //    StringReturnValueFunction(b.ToString());
    //#endif
    //    }
}
