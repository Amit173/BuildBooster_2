using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Part> parts = new List<Part>();
    public GameObject trim;
    public GameObject tin;
    
    public float positionMarker = 0.3048f;
    public float width;
    public float length;
    public float height;
    public float roofPitch;
    public int vertexID;
    public GameObject tempPointer;

    public void GiveExtent(GameObject obj)
    {
        Debug.Log(obj.GetComponent<MeshFilter>().sharedMesh);
    }

    public void BuildingMaker(float W, float L, float H, float RP)
    {
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        width = W;
        length = L;
        height = H;
        roofPitch = RP;
        this.transform.localPosition = new Vector3(width * positionMarker / 2, 0, length * positionMarker / 2);
        this.transform.localPosition = this.transform.localPosition + Vector3.up * (height / 4);

        TrimMaker();
        NorthWallMaker();
        EastWallMaker();
        WestWallMaker();
        SouthWallMaker();
        RoofMaker();
    }

    private void SouthWallMaker()
    {
        GameObject Parent = new GameObject("SouthWall");
        Parent.transform.parent = this.transform;
        if(width%2 ==0)
        {
            for (int i = 0; i < width / 2; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3(i * positionMarker, 0, 0);
                Vector3 vert_1 = new Vector3((i + 1) * positionMarker, 0, 0);
                Vector3 vert_2 = new Vector3((i + 1) * positionMarker, eH, 0);
                Vector3 vert_3 = new Vector3(i * positionMarker, dH, 0);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               2, 1, 0, // First triangle (opposite winding)
               0, 3, 2    // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                // Create a new GameObject to hold the mesh
                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;

                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();  // Add a MeshFilter and MeshRenderer to the GameObject
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                Material m = Resources.Load<Material>("m_Tin");
                meshFilter.mesh = mesh; // Assign the created mesh to the MeshFilter
                Material m_tin = meshRenderer.material = m;
                m.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
                //m_tin = m;
                // Set the position and rotation of the GameObject based on your needs
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
            }

            for (int i = 0; i < width / 2; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3((width - i) * positionMarker, 0, 0);
                Vector3 vert_1 = new Vector3((width - (i + 1)) * positionMarker, 0, 0);
                Vector3 vert_2 = new Vector3((width - (i + 1)) * positionMarker, eH, 0);
                Vector3 vert_3 = new Vector3((width - i) * positionMarker, dH, 0);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               0, 1, 2, // First triangle
               2, 3, 0    // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                meshFilter.mesh = mesh;
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }
        }
        else
        {
            for (int i = 0; i < width / 2 - 1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3(i * positionMarker, 0, 0);
                Vector3 vert_1 = new Vector3((i + 1) * positionMarker, 0, 0);
                Vector3 vert_2 = new Vector3((i + 1) * positionMarker, eH, 0);
                Vector3 vert_3 = new Vector3(i * positionMarker, dH, 0);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               2, 1, 0, // First triangle (opposite winding)
               0, 3, 2    // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                // Create a new GameObject to hold the mesh
                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;

                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();  // Add a MeshFilter and MeshRenderer to the GameObject
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                Material m = Resources.Load<Material>("m_Tin");
                meshFilter.mesh = mesh; // Assign the created mesh to the MeshFilter
                Material m_tin = meshRenderer.material = m;
                m.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
                //m_tin = m;
                // Set the position and rotation of the GameObject based on your needs
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
            }

            for (int i = 0; i < width / 2 - 1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3((width - i) * positionMarker, 0, 0);
                Vector3 vert_1 = new Vector3((width - (i + 1)) * positionMarker, 0, 0);
                Vector3 vert_2 = new Vector3((width - (i + 1)) * positionMarker, eH, 0);
                Vector3 vert_3 = new Vector3((width - i) * positionMarker, dH, 0);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               0, 1, 2, // First triangle
               2, 3, 0    // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                meshFilter.mesh = mesh;
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }
            
            float eH_mid = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (Mathf.FloorToInt(width / 2))))*positionMarker;
            //Debug.Log((Mathf.FloorToInt(width / 2)));
            float dH_mid = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (Mathf.FloorToInt(width / 2) + 1)))*positionMarker; 
            Vector3 vert_mid_0 = new Vector3((Mathf.FloorToInt(width / 2)) * positionMarker, 0, 0);
            Vector3 vert_mid_1 = new Vector3((Mathf.FloorToInt(width / 2) + 1) * positionMarker, 0, 0);
            Vector3 vert_mid_2 = new Vector3((Mathf.FloorToInt(width / 2) + 1) * positionMarker, eH_mid, 0);
            Vector3 vert_mid_3 = new Vector3((width/2) * positionMarker, dH_mid, 0);
            Vector3 vert_mid_4 = new Vector3((Mathf.FloorToInt(width / 2)) * positionMarker, eH_mid, 0);
            Vector3[] coordinates_mid = { vert_mid_0, vert_mid_1, vert_mid_2, vert_mid_3, vert_mid_4 };
            int[] triangles_mid = new int[]
                {
                    4,3,2,
                  2, 1, 0, // First triangle (opposite winding)
                  0, 4, 2    // Second triangle
                };

            Mesh mesh_mid = new Mesh();
            mesh_mid.vertices = coordinates_mid;
            mesh_mid.triangles = triangles_mid;
            mesh_mid.RecalculateNormals();

            GameObject tinObject_mid = new GameObject("TinMesh_" + (width/2).ToString());
            tinObject_mid.transform.parent = Parent.transform;

            MeshFilter meshFilter_mid = tinObject_mid.AddComponent<MeshFilter>();  // Add a MeshFilter and MeshRenderer to the GameObject
            MeshRenderer meshRenderer_mid = tinObject_mid.AddComponent<MeshRenderer>();
            Material m_mid = Resources.Load<Material>("m_Tin");
            meshFilter_mid.mesh = mesh_mid; // Assign the created mesh to the MeshFilter
            Material m_tin_mid = meshRenderer_mid.material = m_mid;
            m_mid.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            //m_tin = m;
            // Set the position and rotation of the GameObject based on your needs
            tinObject_mid.transform.position = Vector3.zero;
            tinObject_mid.transform.rotation = Quaternion.identity;
        }
        

        
    }
    private void NorthWallMaker()
    {
        GameObject Parent = new GameObject("NorthWall");
        Parent.transform.parent = this.transform;
        if (width % 2 == 0)
        {
            for (int i = 0; i < width / 2-1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3(i * positionMarker, 0, positionMarker * length);
                Vector3 vert_1 = new Vector3((i + 1) * positionMarker, 0, positionMarker * length);
                Vector3 vert_2 = new Vector3((i + 1) * positionMarker, eH, positionMarker * length);
                Vector3 vert_3 = new Vector3(i * positionMarker, dH, positionMarker * length);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               0, 1, 2, // First triangle
                2, 3, 0      // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                // Create a new GameObject to hold the mesh
                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                // Add a MeshFilter and MeshRenderer to the GameObject
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();

                // Assign the created mesh to the MeshFilter
                meshFilter.mesh = mesh;

                // Set the position and rotation of the GameObject based on your needs
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;

                // Optionally, you can set a material for the mesh renderer
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }

            for (int i = 0; i < width / 2-1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3((width - i) * positionMarker, 0, positionMarker * length);
                Vector3 vert_1 = new Vector3((width - (i + 1)) * positionMarker, 0, positionMarker * length);
                Vector3 vert_2 = new Vector3((width - (i + 1)) * positionMarker, eH, positionMarker * length);
                Vector3 vert_3 = new Vector3((width - i) * positionMarker, dH, positionMarker * length);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
              2, 1, 0, // First triangle (opposite winding)
            0, 3, 2   // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                meshFilter.mesh = mesh;
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }
        }else
        {
            for (int i = 0; i < width / 2 - 1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3(i * positionMarker, 0, positionMarker * length);
                Vector3 vert_1 = new Vector3((i + 1) * positionMarker, 0, positionMarker * length);
                Vector3 vert_2 = new Vector3((i + 1) * positionMarker, eH, positionMarker * length);
                Vector3 vert_3 = new Vector3(i * positionMarker, dH, positionMarker * length);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
               0, 1, 2, // First triangle
                2, 3, 0      // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                // Create a new GameObject to hold the mesh
                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                // Add a MeshFilter and MeshRenderer to the GameObject
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();

                // Assign the created mesh to the MeshFilter
                meshFilter.mesh = mesh;

                // Set the position and rotation of the GameObject based on your needs
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;

                // Optionally, you can set a material for the mesh renderer
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }

            for (int i = 0; i < width / 2 - 1; i++)
            {
                // Your existing code for calculating vertex positions
                float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i + 1))) * positionMarker;
                float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;

                Vector3 vert_0 = new Vector3((width - i) * positionMarker, 0, positionMarker * length);
                Vector3 vert_1 = new Vector3((width - (i + 1)) * positionMarker, 0, positionMarker * length);
                Vector3 vert_2 = new Vector3((width - (i + 1)) * positionMarker, eH, positionMarker * length);
                Vector3 vert_3 = new Vector3((width - i) * positionMarker, dH, positionMarker * length);
                Vector3[] coordinates = { vert_0, vert_1, vert_2, vert_3 };

                int[] triangles = new int[]
                {
              2, 1, 0, // First triangle (opposite winding)
            0, 3, 2   // Second triangle
                };

                Mesh mesh = new Mesh();
                mesh.vertices = coordinates;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();

                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;
                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
                meshFilter.mesh = mesh;
                tinObject.transform.position = Vector3.zero;
                tinObject.transform.rotation = Quaternion.identity;
                meshRenderer.material = Resources.Load<Material>("m_Tin");
            }

            float eH_mid = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (Mathf.FloorToInt(width / 2)))) * positionMarker;
            //Debug.Log((Mathf.FloorToInt(width / 2)));
            float dH_mid = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (Mathf.FloorToInt(width / 2) + 1))) * positionMarker;
            Vector3 vert_mid_0 = new Vector3((Mathf.FloorToInt(width / 2)) * positionMarker, 0, length*positionMarker);
            Vector3 vert_mid_1 = new Vector3((Mathf.FloorToInt(width / 2) + 1) * positionMarker, 0, length*positionMarker);
            Vector3 vert_mid_2 = new Vector3((Mathf.FloorToInt(width / 2) + 1) * positionMarker, eH_mid, length*positionMarker);
            Vector3 vert_mid_3 = new Vector3((width / 2) * positionMarker, dH_mid, length * positionMarker);
            Vector3 vert_mid_4 = new Vector3((Mathf.FloorToInt(width / 2)) * positionMarker, eH_mid, length*positionMarker);
            Vector3[] coordinates_mid = { vert_mid_0, vert_mid_1, vert_mid_2, vert_mid_3, vert_mid_4 };
            int[] triangles_mid = new int[]
                {
                   3,4,2,
                  2, 0, 1, // First triangle (opposite winding)
                  0, 2, 4    // Second triangle
                };

            Mesh mesh_mid = new Mesh();
            mesh_mid.vertices = coordinates_mid;
            mesh_mid.triangles = triangles_mid;
            mesh_mid.RecalculateNormals();

            GameObject tinObject_mid = new GameObject("TinMesh_" + (width / 2).ToString());
            tinObject_mid.transform.parent = Parent.transform;

            MeshFilter meshFilter_mid = tinObject_mid.AddComponent<MeshFilter>();  // Add a MeshFilter and MeshRenderer to the GameObject
            MeshRenderer meshRenderer_mid = tinObject_mid.AddComponent<MeshRenderer>();
            Material m_mid = Resources.Load<Material>("m_Tin");
            meshFilter_mid.mesh = mesh_mid; // Assign the created mesh to the MeshFilter
            Material m_tin_mid = meshRenderer_mid.material = m_mid;
            m_mid.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            //m_tin = m;
            // Set the position and rotation of the GameObject based on your needs
            tinObject_mid.transform.position = Vector3.zero;
            tinObject_mid.transform.rotation = Quaternion.identity;
        }
    }
    private void WestWallMaker()
    {
        GameObject Parent = new GameObject("WestWall");
        Parent.transform.parent = this.transform;
        for (int i = 0; i < length; i++)
        {
            GameObject leftWallPart = Instantiate(tin, new Vector3(0, 0, positionMarker * i), Quaternion.Euler(new Vector3(0, 90, 0)), Parent.transform);
            leftWallPart.transform.localScale = new Vector3(1, height- 0.02f, 1);
        }
    }

    private void EastWallMaker()
    {
        GameObject Parent = new GameObject("EastWall");
        Parent.transform.parent = this.transform;
        for (int i = 0; i < length; i++)
        {
            GameObject rightWallPart = Instantiate(tin, new Vector3(width * positionMarker, 0, positionMarker * (length - i)), Quaternion.Euler(new Vector3(0, -90, 0)), Parent.transform);
            rightWallPart.transform.localScale = new Vector3(1, height-0.02f, 1);
        }
    }

    

    public void TrimMaker()
    {
        GameObject Parent = new GameObject("Trim");
        Parent.transform.parent = this.transform;
        Debug.Log(trim.GetComponent<MeshFilter>().sharedMesh.bounds);
        Quaternion rot = Quaternion.Euler(0, 0, 90 + roofPitch);
        GameObject frontLeftTrim = Instantiate(trim, new Vector3(0, 0, 0), Quaternion.identity, Parent.transform);
        frontLeftTrim.transform.localScale = new Vector3(1, height, 1);
        GameObject frontRightTrim = Instantiate(trim, new Vector3(positionMarker * width, 0, 0), Quaternion.identity, Parent.transform);
        frontRightTrim.transform.localScale = new Vector3(1, height, 1);
        GameObject backRightTrim = Instantiate(trim, new Vector3(positionMarker * width, 0, positionMarker * length), Quaternion.identity, Parent.transform);
        backRightTrim.transform.localScale = new Vector3(1, height, 1);
        GameObject backLeftTrim = Instantiate(trim, new Vector3(0, 0, positionMarker * length), Quaternion.identity, Parent.transform);
        backLeftTrim.transform.localScale = new Vector3(1, height, 1);

        GameObject leftFrontRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, 0), rot, Parent.transform);
        leftFrontRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        GameObject rightFrontRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, 0), Quaternion.Inverse(rot), Parent.transform);
        rightFrontRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        GameObject leftBackRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, length * positionMarker), rot, Parent.transform);
        leftBackRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        GameObject rightBackRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, length * positionMarker), Quaternion.Inverse(rot), Parent.transform);
        rightBackRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);

        GameObject Top = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker + trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 4, -trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x/2), Quaternion.Euler(new Vector3(45,90,90)), Parent.transform);
        Top.transform.localScale = new Vector3(1, length + positionMarker + trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x/4, 1);
    }

    public void RoofMaker()
    {
        GameObject Parent = new GameObject("Roof");
        Parent.transform.parent = this.transform;
        Quaternion rot = Quaternion.Euler(0, 0, 90 + roofPitch);
        for (int i = 0; i < length; i++)
        {

            GameObject roofRightPart = Instantiate(tin, new Vector3(positionMarker * width / 2, (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2))) * positionMarker, positionMarker * i), Quaternion.Euler(new Vector3(90 + roofPitch, 90, 0)), Parent.transform);
            roofRightPart.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
            GameObject roofLeftPart = Instantiate(tin, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, positionMarker * (length - i)), Quaternion.Euler(new Vector3(90 + roofPitch, -90, 0)), Parent.transform);
            roofLeftPart.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
            roofLeftPart.name = "roofLeftPart";
        }
    }

    public void FrontWallMaker()
    {
        GameObject frontWallPart = Instantiate(MakeNewWallComponent(), new Vector3(positionMarker * 0, (height * positionMarker), 0f), Quaternion.Euler(new Vector3(180, 180, 0)), this.transform);
        MeshFilter meshFilter = frontWallPart.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] originalVertices;
        originalVertices = mesh.vertices;
        Vector3[] modifiedVertices = new Vector3[originalVertices.Length];
        modifiedVertices[2] = new Vector3(originalVertices[2].x, originalVertices[2].y + (Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * (positionMarker / 2), originalVertices[2].z);
        modifiedVertices[4] = new Vector3(originalVertices[4].x, originalVertices[4].y + (Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * (positionMarker / 2), originalVertices[4].z);
        modifiedVertices[5] = new Vector3(originalVertices[5].x, originalVertices[5].y + (Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * (positionMarker / 2), originalVertices[5].z);
        modifiedVertices[8] = new Vector3(originalVertices[8].x, originalVertices[8].y + (Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * (positionMarker / 2), originalVertices[8].z);
        modifiedVertices[9] = new Vector3(originalVertices[9].x, originalVertices[9].y + (Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * (positionMarker), originalVertices[9].z);
        mesh.vertices = modifiedVertices;
        mesh.RecalculateBounds();

        //for (int i= 0; i < width; i++)
        //{
        //    GameObject frontWallPart = Instantiate(tin, new Vector3(positionMarker * i, (height*positionMarker), 0f), Quaternion.Euler(new Vector3(180,180,0)), this.transform);
        //    frontWallPart.transform.localScale = new Vector3(1,height*positionMarker, 1);
        //   // Mesh newTinMesh = frontWallPart.GetComponent<MeshFilter>().sharedMesh;
        //    //
        //   // originalVertices = newTinMesh.vertices;
        //   //Vector3[] modifiedVertices = new Vector3[originalVertices.Length];
        //    //Instantiate(tempPointer, originalVertices[0], Quaternion.identity, this.transform);

        //    //if (meshFilter != null)
        //    //{
        //    //   // Mesh mesh = meshFilter.sharedMesh;
        //    //   // Vector3[] vertices = mesh.vertices;

        //    //    for (int j = 0; j < vertices.Length; j++)
        //    //    {
        //    //        GameObject sphere = Instantiate(tempPointer, frontWallPart.transform.TransformPoint(vertices[j]), Quaternion.identity);
        //    //        sphere.transform.parent = transform;
        //    //        sphere.name = "vertex_" + j.ToString();
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Debug.LogError("MeshFilter component not found!");
        //    //}
        //}
    }

   
    public void NewFrontWallMaker()
    {
       // GameObject temp = MakeNewWallComponent();
        for (int i = 0; i < width / 2; i++)
        {
            float eH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i+1))) * positionMarker;
            float dH = (height + (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (i))) * positionMarker;
            // GameObject frontWallPart = Instantiate(temp, new Vector3(positionMarker * (i), eH, 0f), Quaternion.Euler(new Vector3(180, 180, 0)), this.transform);
            //frontWallPart.transform.localScale = new Vector3(1, height+(Mathf.Tan(roofPitch * Mathf.Deg2Rad))* (i) , 1);
            //MeshFilter meshFilter = frontWallPart.GetComponent<MeshFilter>();
            //if (meshFilter != null)
            //{
            //    Mesh mesh = meshFilter.sharedMesh;
            //    Vector3[] vertices = mesh.vertices;

            //    for (int j = 0; j < vertices.Length; j++)
            //    {
            //        GameObject sphere = Instantiate(tempPointer, frontWallPart.transform.TransformPoint(vertices[j]), Quaternion.identity);
            //        sphere.transform.parent = transform;
            //        sphere.name = "vertex_" + j.ToString();
            //    }
            //}
            //else
            //{
            //    Debug.LogError("MeshFilter component not found!");
            //}
        }
    }


    public GameObject WallComponentMaker()
    {
        Mesh originalMesh = tin.GetComponent<MeshFilter>().sharedMesh;

        // Clone the original mesh to a new mesh
        Mesh newMesh = CloneMesh(originalMesh);

        // Modify the position of a specific vertex (vertex 9 in this case)

        // Replace this with your desired roof pitch value
        // Replace this with your desired position marker value
        GameObject newObject = new GameObject("CreatedTin");
        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = newMesh;
        meshRenderer.material = Resources.Load<Material>("m_Tin");
        Vector3[] originalVert = newMesh.vertices;
        Vector3[] modifiedVertex = new Vector3[originalVert.Length];
        modifiedVertex = originalVert;
        Debug.Log("newMesh information" + newMesh.bounds + "\t Size bounds" + newMesh.bounds.size);
        //modifiedVertex[3] = newObject.transform.TransformPoint(originalVert[3].x, (originalVert[3].y + ((Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * newMesh.bounds.size.x))*positionMarker, originalVert[3].z);

        modifiedVertex[2] = newObject.transform.TransformPoint(originalVert[2].x, (originalVert[2].y - ((Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * newMesh.bounds.size.x / 8)) * positionMarker, originalVert[2].z);
        //modifiedVertex[2] = newObject.transform.TransformPoint(originalVert[2].x, originalVert[2].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[2].z);
        //modifiedVertex[4] = newObject.transform.TransformPoint(originalVert[4].x, originalVert[4].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[4].z);
        //modifiedVertex[5] = newObject.transform.TransformPoint(originalVert[5].x, originalVert[5].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[5].z);
        //modifiedVertex[8] = newObject.transform.TransformPoint(originalVert[8].x, originalVert[8].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[8].z);
        newMesh.vertices = modifiedVertex;

        // Recalculate the normals to ensure correct shading after vertex modification
        newMesh.RecalculateNormals();
        newMesh.RecalculateBounds();
        newMesh.RecalculateTangents();
        // Create a new GameObject and apply the modified mesh


        return newObject;
    }

    public GameObject MakeNewWallComponent()
    {
        Mesh originalMesh = tin.GetComponent<MeshFilter>().sharedMesh;

        // Clone the original mesh to a new mesh
        Mesh newMesh = CloneMesh(originalMesh);

        // Modify the position of a specific vertex (vertex 9 in this case)

        // Replace this with your desired roof pitch value
        // Replace this with your desired position marker value
        GameObject newObject = new GameObject("CreatedTin");
        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = newMesh;
        meshRenderer.material = Resources.Load<Material>("m_Tin");
        Vector3[] originalVert = newMesh.vertices;
        Vector3[] modifiedVertex = new Vector3[originalVert.Length];
        modifiedVertex = originalVert;
        Debug.Log("newMesh information" + newMesh.bounds + "\t Size bounds" + newMesh.bounds.size);
        //modifiedVertex[3] = newObject.transform.TransformPoint(originalVert[3].x, (originalVert[3].y + ((Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * newMesh.bounds.size.x))*positionMarker, originalVert[3].z);

        modifiedVertex[2] = newObject.transform.TransformPoint(originalVert[2].x, (originalVert[2].y - ((Mathf.Tan(roofPitch * Mathf.Deg2Rad)) * newMesh.bounds.size.x/8))*positionMarker, originalVert[2].z);
        //modifiedVertex[2] = newObject.transform.TransformPoint(originalVert[2].x, originalVert[2].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[2].z);
        //modifiedVertex[4] = newObject.transform.TransformPoint(originalVert[4].x, originalVert[4].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[4].z);
        //modifiedVertex[5] = newObject.transform.TransformPoint(originalVert[5].x, originalVert[5].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[5].z);
        //modifiedVertex[8] = newObject.transform.TransformPoint(originalVert[8].x, originalVert[8].y - (Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (newMesh.bounds.size.x / 32)), originalVert[8].z);
        newMesh.vertices = modifiedVertex;

        // Recalculate the normals to ensure correct shading after vertex modification
        newMesh.RecalculateNormals();
        newMesh.RecalculateBounds();
        newMesh.RecalculateTangents();
        // Create a new GameObject and apply the modified mesh


        return newObject;
    }

    private Mesh CloneMesh(Mesh originalMesh)
    {
        Mesh newMesh = new Mesh();
        // Vector3[] justVert = new Vector3[] 
        newMesh.vertices = originalMesh.vertices;
        newMesh.triangles = originalMesh.triangles;
        newMesh.normals = originalMesh.normals;
        newMesh.uv = originalMesh.uv;
        // You can copy other mesh data as well, like colors, tangents, etc., if needed.

        return newMesh;
    }
}
