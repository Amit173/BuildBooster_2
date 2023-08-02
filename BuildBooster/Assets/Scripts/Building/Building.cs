using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static TreeEditor.TextureAtlas;

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

    public Material m_Wall;
    public Material m_Roof;
    public Material m_Trim;
    public Material m_Wainscot;

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
        WainscotMaker();
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

                
                GameObject tinObject = new GameObject("TinMesh_" + i);
                tinObject.transform.parent = Parent.transform;

                MeshFilter meshFilter = tinObject.AddComponent<MeshFilter>();  
                MeshRenderer meshRenderer = tinObject.AddComponent<MeshRenderer>();
               
                meshFilter.mesh = mesh; 
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
               
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
               
                meshFilter.mesh = mesh; // Assign the created mesh to the MeshFilter
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
            
            meshFilter_mid.mesh = mesh_mid; // Assign the created mesh to the MeshFilter
            meshRenderer_mid.material = m_Wall;
            m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
            for (int i = 0; i < width / 2; i++)
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            }

            for (int i = 0; i < width / 2; i++)
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            }
        }
        else
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
                meshRenderer.material = m_Wall;
                m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
            
            meshFilter_mid.mesh = mesh_mid; // Assign the created mesh to the MeshFilter
            meshRenderer_mid.material = m_Wall;
            m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
            leftWallPart.GetComponent<MeshRenderer>().material = m_Wall;
            m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
            rightWallPart.GetComponent <MeshRenderer>().material = m_Wall;
            m_Wall.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
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
        frontLeftTrim.GetComponent<MeshRenderer>().material = m_Trim;
        
        GameObject frontRightTrim = Instantiate(trim, new Vector3(positionMarker * width, 0, 0), Quaternion.identity, Parent.transform);
        frontRightTrim.transform.localScale = new Vector3(1, height, 1);
        frontRightTrim.GetComponent<MeshRenderer>().material = m_Trim;

        GameObject backRightTrim = Instantiate(trim, new Vector3(positionMarker * width, 0, positionMarker * length), Quaternion.identity, Parent.transform);
        backRightTrim.transform.localScale = new Vector3(1, height, 1);
        backRightTrim.GetComponent <MeshRenderer>().material = m_Trim;

        GameObject backLeftTrim = Instantiate(trim, new Vector3(0, 0, positionMarker * length), Quaternion.identity, Parent.transform);
        backLeftTrim.transform.localScale = new Vector3(1, height, 1);
        backLeftTrim.GetComponent <MeshRenderer>().material = m_Trim;

        GameObject leftFrontRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, 0), rot, Parent.transform);
        leftFrontRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        leftFrontRoofTrim.GetComponent <MeshRenderer>().material = m_Trim;

        GameObject rightFrontRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, 0), Quaternion.Inverse(rot), Parent.transform);
        rightFrontRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        rightFrontRoofTrim.GetComponent<MeshRenderer>().material = m_Trim;

        GameObject leftBackRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, length * positionMarker), rot, Parent.transform);
        leftBackRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        leftBackRoofTrim.GetComponent <MeshRenderer>().material = m_Trim;

        GameObject rightBackRoofTrim = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, length * positionMarker), Quaternion.Inverse(rot), Parent.transform);
        rightBackRoofTrim.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
        rightFrontRoofTrim.GetComponent<MeshRenderer>().material = m_Trim;

        GameObject Top = Instantiate(trim, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker + trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 4, -trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x/2), Quaternion.Euler(new Vector3(45,90,90)), Parent.transform);
        Top.transform.localScale = new Vector3(1, length + positionMarker + trim.GetComponent<MeshFilter>().sharedMesh.bounds.size.x/4, 1);
        Top.GetComponent<MeshRenderer>().material = m_Trim;
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
            roofRightPart.GetComponent<MeshRenderer>().material = m_Roof;
            roofRightPart.name = "roofRightPart" + i.ToString();
            m_Roof.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            GameObject roofLeftPart = Instantiate(tin, new Vector3(positionMarker * width / 2, (height + Mathf.Tan(roofPitch * Mathf.Deg2Rad) * (width / 2)) * positionMarker, positionMarker * (length - i)), Quaternion.Euler(new Vector3(90 + roofPitch, -90, 0)), Parent.transform);
            roofLeftPart.transform.localScale = new Vector3(1, (width / 2) / (Mathf.Cos(roofPitch * Mathf.Deg2Rad)) + 0.5f, 1);
            roofLeftPart.GetComponent<MeshRenderer>().material = m_Roof;
            m_Roof.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
            roofLeftPart.name = "roofLeftPart" + i.ToString();
        }
    }

    public void WainscotMaker()
    {
        GameObject Parent = new GameObject("Wainscot");
        Parent.transform.parent = this.transform;
        for (int i = 0; i < length; i++)
        {
            GameObject leftWainscotPart = Instantiate(tin, new Vector3(-0.01f, 0, positionMarker * i), Quaternion.Euler(new Vector3(0, 90, 0)), Parent.transform);
            leftWainscotPart.transform.localScale = new Vector3(1, (height/6), 1);
            leftWainscotPart.GetComponent<MeshRenderer>().material = m_Wainscot;
            m_Wainscot.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));

            GameObject rightWainscotPart = Instantiate(tin, new Vector3(width * positionMarker + 0.01f, 0, positionMarker * (length - i)), Quaternion.Euler(new Vector3(0, -90, 0)), Parent.transform);
            rightWainscotPart.transform.localScale = new Vector3(1, (height/6), 1);
            rightWainscotPart.GetComponent<MeshRenderer>().material = m_Wainscot;
            m_Wainscot.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
        }

        for(int i = 0; i < width; i++)
        {
            GameObject frontWainscotPart = Instantiate(tin, new Vector3(positionMarker * (width-i), 0, -0.01f), Quaternion.Euler(new Vector3(0, 0, 0)), Parent.transform);
            frontWainscotPart.transform.localScale = new Vector3(1, (height / 6), 1);
            frontWainscotPart.GetComponent<MeshRenderer>().material= m_Wainscot;
            m_Wainscot.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));

            GameObject backWainscotPart = Instantiate(tin, new Vector3(positionMarker * i, 0, length*positionMarker + 0.01f), Quaternion.Euler(new Vector3(0, 180, 0)), Parent.transform);
            backWainscotPart.transform.localScale = new Vector3(1, (height / 6), 1);
            backWainscotPart.GetComponent<MeshRenderer>().material = m_Wainscot;
            m_Wainscot.SetTexture("_BumpMap", Resources.Load<Texture2D>("tin_normal"));
        }
    }

    public void SetWallColor(string colorCode)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(colorCode, out newColor);
        m_Wall.SetColor("_Color", newColor);
        m_Wall.SetTexture("_MainTex", null);
    }
    public void SetRoofColor(string colorCode)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(colorCode, out newColor);
        m_Roof.SetColor("_Color", newColor);
        m_Roof.SetTexture("_MainTex", null);
    }
    public void SetTrimColor(string colorCode)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(colorCode, out newColor);
        m_Trim.SetColor("_Color", newColor);
        m_Trim.SetTexture("_MainTex", null);
    }
    public void SetWainscotColor(string colorCode)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(colorCode, out newColor);
        m_Wainscot.SetColor("_Color", newColor);
        m_Wainscot.SetTexture("_MainTex", null);
    }

    public void SetWallTexture(string textureName)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString("FFFFFF", out newColor);
        m_Wall.SetColor("_Color", newColor);
        m_Wall.SetTexture("_MainTex", Resources.Load<Texture2D>(textureName));
    }
    public void SetRoofTexture(string textureName)
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString("FFFFFF", out newColor);
        m_Roof.SetColor("_Color", newColor);
        m_Roof.SetTexture("_MainTex", Resources.Load<Texture2D>(textureName));
    }
    public void SetWainscotTexture(string textureName)
    {
        m_Wainscot.SetTexture("_MainTex", Resources.Load<Texture2D>(textureName));
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString("FFFFFF", out newColor);
        m_Wainscot.SetColor("_Color", newColor);
    }

}
