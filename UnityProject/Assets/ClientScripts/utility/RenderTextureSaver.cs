using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class RenderTextureSaver : MonoBehaviour
{
    RenderTexture _RenderTexture;
    Camera _Camera;

    public bool _UseScreenSize = true;
    public int _Width = 1024;
    public int _Height = 1024;

    Texture2D _Texture;
    // Start is called before the first frame update
    void Awake()
    {
        _Camera = GetComponent<Camera>();
        if (_UseScreenSize == false)
        {

            _RenderTexture = new RenderTexture(_Width, _Height, 32, RenderTextureFormat.ARGB32);
            _RenderTexture.Create();
        }
        else
        {
            _RenderTexture = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGB32);
            _RenderTexture.Create();

        }


    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator TakePhoto(TextureFormat format)
    {
        _Camera.targetTexture = _RenderTexture;

        if(_Texture)
        {
            DestroyImmediate(_Texture);
        }
        yield return new WaitForEndOfFrame();


        RenderTexture.active = _RenderTexture;
        _Texture = new Texture2D(_RenderTexture.width, _RenderTexture.height, format, false);
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
