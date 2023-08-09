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

    public AddEntity addEntityScript;

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
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString("#B2BEB5", out newColor);
        m_Roof.SetTexture("_MainTex", null);
        m_Roof.SetColor("_Color",newColor);

        m_Wall.SetTexture("_MainTex", null);
        m_Wall.SetColor("_Color", newColor);

        m_Wainscot.SetTexture("_MainTex", null);
        m_Wainscot.SetColor("_Color", newColor);

        m_Trim.SetTexture("_MainTex", null);
        m_Trim.SetColor("_Color",newColor);
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
        //if (addEntityScript.north.wallReference != null)
        //{
        //    Debug.Log("IF ke ander hu");
        //    addEntityScript.Deinit();
        //}
        wareHouse.BuildingMaker(width, length, height, roofPitch);
        //addEntityScript.GetComponent<AddEntity>().InitiializeWalls();
        //addEntityScript.updateDoorPositions();
       //addEntityScript.updateWindowPositions();
    }
    public void MakeBuilding()
    {
        //addEntityScript.Deinit();
        wareHouse.BuildingMaker(width,length,height,roofPitch);
        //addEntityScript.CheckWallData(addEntityScript.north);
       // addEntityScript.GetComponent<AddEntity>().InitiializeWalls();
        //addEntityScript.CheckWallData(addEntityScript.north);
        addEntityScript.updateDoorPositions();
        addEntityScript.updateWindowPositions();
    }

    public void ChangeColor(string str)
    {
        string[] arr = str.Split("_");
        string partName = arr[0];
        string colorCode = arr[1];
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

    public void ChangeTexture(string str)
    {
        string[] arr = str.Split("_");
        string partName = arr[0];
        string textureName = arr[1];
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
