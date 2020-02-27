using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class RenderTextureSaver : MonoBehaviour
{
    RenderTexture _RenderTexture;
    Camera _Camera;


    public int _Width = 1024;
    public int _Height = 1024;

    Texture2D _Texture;
    // Start is called before the first frame update
    void Awake()
    {
        _Camera = GetComponent<Camera>();
        _RenderTexture = new RenderTexture(_Width,_Height, 32, RenderTextureFormat.ARGB32);
        _RenderTexture.Create();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator TakePhoto()
    {
        _Camera.targetTexture = _RenderTexture;

        if(_Texture)
        {
            DestroyImmediate(_Texture);
        }
        yield return new WaitForEndOfFrame();


        RenderTexture.active = _RenderTexture;
        _Texture = new Texture2D(_RenderTexture.width, _RenderTexture.height, TextureFormat.RGBA32, false);
        _Texture.ReadPixels(new Rect(0, 0, _RenderTexture.width, _RenderTexture.height), 0, 0);
        _Texture.Apply();
        RenderTexture.active = null;

        _Camera.targetTexture = null;
        yield return new WaitForEndOfFrame();

    }

    public Texture2D GetTexture()
    {
        return _Texture;
    }
    
}
