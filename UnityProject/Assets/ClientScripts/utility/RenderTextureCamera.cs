using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureCamera : MonoBehaviour
{
    public Renderer mRenderer;
    RenderTexture _RenderTexture;
    Camera _Camera;
    public Material mAlphaMaterial;

    public int _Width = 2048;
    public int _Height = 2048;

    Texture2D _Texture;
    // Start is called before the first frame update
    void Awake()
    {
        _Camera = GetComponent<Camera>();
        _RenderTexture = new RenderTexture(_Width, _Height, 32, RenderTextureFormat.ARGB32);
        _RenderTexture.Create();

        _Camera.enabled = false;
    }
    

    public IEnumerator TakePhoto(TextureFormat format,Material material,bool alpha = false)
    {
        _Camera.enabled = true;
        if (alpha)
        {
            mAlphaMaterial.SetTexture("_MainTex", material.GetTexture("_MainTex"));
            mRenderer.material = mAlphaMaterial;
        }
        else
        {
            mRenderer.material = material;
        }

        _Camera.targetTexture = _RenderTexture;

        if (_Texture)
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
        mRenderer.material = null;
        _Camera.enabled = false;
        yield return new WaitForEndOfFrame();

    }

    public Texture2D GetTexture()
    {
        return _Texture;
    }

}
