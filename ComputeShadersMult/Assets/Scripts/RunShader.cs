using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunShader : MonoBehaviour
{
    public ComputeShader shader;

    // Start is called before the first frame update
    void Start()
    {
        computeShader(256, 256, 24);
    }
    
    void Update() {
        computeShader(256, 256, 24);
    }

    void computeShader(int width, int height, int depth){
        
        // obtain the CSMain function for compute shader
        int kernelHandle = shader.FindKernel("CSMain");

        // create a renderTexture
        RenderTexture tex = new RenderTexture(width, height, depth);

        // gives compute shader access to write to the texture
        tex.enableRandomWrite = true;
        tex.Create();

        // move from cpu to gpu
        shader.SetTexture(kernelHandle, "Result", tex);

        // number of thread groups to spawn
        shader.Dispatch(kernelHandle, width/8, height/8, 1);

        //this.GetComponent<MeshRenderer>().material = tex;
    }
}
