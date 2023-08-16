using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using static Unity.Burst.Intrinsics.X86;

public class DownloadHandler : MonoBehaviour
{
    public GltfAsset AVB;
    public Loader loader;
    // Downloads the model using url (in our case we are downloading avatars and we are applying check for current loaded avatar)
    public void DownloadModel(string url)
    {
        //Debug.Log("URL :- " + url);
        //string[] str = url.Split('.');
       // Debug.Log("File type is :-" + str[str.Length - 1]);
        //if (charName != currentCharacterName)
        //{
            Destroy(GameObject.FindGameObjectWithTag("Character"));
            loader.Active();
            _ = myTask(url);
           // if (str[str.Length - 1] == "glb")
           //     isGLB = true;
           // else
           //     isGLB = false;
        
        //}
    }

    public async Task<bool> myTask(string url)
    {
        Debug.Log("Waiting for done the process!");
        AVB.Dispose();
        var ss = await AVB.Load(url);

        if (ss)
        {
            Debug.Log("Yes it's done now");
           // AssignCharacter(AVB.transform.GetChild(0).gameObject);
            loader.Deactive();
            return ss;
        }
        else
        {
            Debug.Log("Nope not done yet!");
            return false;
        }
    }
}
