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

    [SerializeField] GameObject addEntityScript;
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
        addEntityScript.GetComponent<AddEntity>().InitiializeWalls();
    }

    public void ChangeColor(string colorCode)
    {
        currentSelectedPart.ChangeColor(colorCode);
    }
    //Method For Java fuction call
//    void CallJavaScriptFunction(bool b)
//    {
//#if UNITY_WEBGL && !UNITY_EDITOR
//    StringReturnValueFunction(b.ToString());
//#endif
//    }
}
