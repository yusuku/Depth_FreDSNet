using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makeocclusion : MonoBehaviour
{
    public GameObject cube;
    public Texture2D depthmap;
    // Start is called before the first frame update
    void Start()
    {
        Makeocclustion(depthmap, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Makeocclustion(Texture2D depthmap,float r)
    {
        Color[] pix = depthmap.GetPixels();
        int width = depthmap.width, height = depthmap.height;
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var pic_dep = pix[x + y * width].r;
                //if (pic_dep <= r && pic_dep!=0f)
                {
                    Debug.Log(pic_dep);
                    Vector2 polar = XY2Polar(x, y, width, height);
                    float radius = pic_dep*10;
                    Vector3 position = PolarToCartesian(polar.x, polar.y, radius);
                    makeCube(position, pix[x + y * width]);
                }
                  
            }
        }

    }

    GameObject makeCube(Vector3 position,Color color)
    {
        var obj = Instantiate(cube, position, Quaternion.identity);
        obj.GetComponent<Renderer>().material.color = color;
        return obj;
    }

    public static Vector3 PolarToCartesian(float phi, float theta, float radius = 1.0f)
    {
        float x = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
        float y = radius * Mathf.Cos(theta);
        float z = radius * Mathf.Sin(theta) * Mathf.Sin(phi);

        return new Vector3(x, y, z);
    }
    Vector2 XY2Polar(int x, int y, int width, int height)
    {
        Vector2 polar;

        polar = new Vector2((1 - x / (float)width) * 2 * Mathf.PI - Mathf.PI, Mathf.PI - y / (float)height * Mathf.PI);

        return polar;
    }
}
