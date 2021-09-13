using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshTester : MonoBehaviour
{
    [EditorButton]
    public void ShowMeshParams()
    {
        var mesh = GetComponent<MeshFilter>().mesh;

        var triangles = mesh.triangles;
        var verticies = mesh.vertices;


        print("Triangles");
        foreach (var triangle in triangles)
        {
            print(triangle);
        }
        
        print("Vectices");
        foreach (var vert in verticies)
        {
            print(vert);
        }
    }

    [EditorButton]
    public void Move()
    {
        var mesh = GetComponent<MeshFilter>().mesh;

        var triangles = mesh.triangles;
        var verticies = mesh.vertices.ToList();
        
        int index = verticies.FindIndex(x => x.x == 1f && x.y == 0f && x.z == 0f);
        
        foreach (var vert in verticies)
        {
            print(vert);
        }
        
        print(index);
        print(verticies.Count);
        
        verticies[index]= new Vector3(0f, 1f, 0f);

        mesh.vertices = verticies.ToArray();
    }
}
