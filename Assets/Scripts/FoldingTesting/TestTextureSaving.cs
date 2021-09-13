using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTextureSaving : MonoBehaviour
{
    bool grab;

    public RawImage m_Display;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("SPace");
            grab = true;
        }
    }

    private void OnPostRender()
    {
        if (grab)
        {
            print("SetTexture");
            
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();
            
            if (m_Display != null)
                m_Display.texture = texture;
            
            grab = false;
        }
    }
}
