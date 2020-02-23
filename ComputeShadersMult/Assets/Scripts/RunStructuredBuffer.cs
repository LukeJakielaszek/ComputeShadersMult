using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStructuredBuffer : MonoBehaviour
{    
    public ComputeShader compute;
    public ComputeBuffer buffer;
    public int[] cols;
    
    void Start () {
        var mesh = GetComponent<MeshFilter>().mesh;
        
        int n = mesh.vertexCount;
        Debug.Log(n);

        buffer = new ComputeBuffer (n, sizeof(int));

        cols = new int[n];
        for (int i = 0; i < n; ++i)
            cols[i] = 0;
        buffer.SetData (cols);

        int kernel = compute.FindKernel("CSMain");
        compute.SetBuffer(kernel,"bufColors", buffer);
        compute.Dispatch(kernel,n/4,1,1);
        buffer.GetData(cols);

        for (int i = 0; i < n; ++i)
            Debug.Log (cols[i]);

        buffer.Dispose();
    }
}
