using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSelector : MonoBehaviour
{

    public Button m_OpenCameraBtn;
    public Button m_OpenGalleryBtn;

    public RawImage m_RawImage;
    public Button okbutton;
    public Button rotatebutton;
    public Text loadText;


    private string filePathpic="";

    static Texture2D m_PickedTexture;
    // Start is called before the first frame update
    void Start()
    {

        NativeCamera.RequestPermission();
        NativeGallery.RequestPermission();


        m_OpenCameraBtn.onClick.AddListener(OnOpenCamera);
        m_OpenGalleryBtn.onClick.AddListener(OnOpenGallery);
    }
    void OnOpenCamera()
    {
        NativeCamera.TakePicture(OnTakePhoto);
    }
    void OnTakePhoto(string filePath)
    {

       
            LoadImageFromFile(filePath);
       
    }

    void OnOpenGallery()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var filePath = StandaloneFileBrowser.OpenFilePanel("Title", "", "jpg", false);
        //需要加上出错处理

       Debug.Log(filePath.Length);

        if (filePath.Length != 0)
        {
            LoadImageFromFile(filePath[0]);
        }
        else {
            loadText.text = "文件不存在";

        }

#else

        //需要从安卓传输路径到imageload

        NativeGallery.GetImageFromGallery(OnPickImage);



#endif
    }

    void OnPickImage(string filePath)
    {
		LoadImageFromFile(filePath);
    }
    void LoadImageFromFile(string filePath)

         {
        
        byte[] fileData;

        if (File.Exists(filePath))
        {
            //显示图片
            fileData = File.ReadAllBytes(filePath);
            m_PickedTexture = new Texture2D(2, 2);
            m_PickedTexture.LoadImage(fileData); //..this will auto-resize the texture dimensions.

            //图片显示框的宽度等比缩放
            float y = m_RawImage.transform.GetComponent<RectTransform>().sizeDelta.y;
            float x = m_RawImage.transform.GetComponent<RectTransform>().sizeDelta.x;
            float x2 = y * m_PickedTexture.width / m_PickedTexture.height;
            Vector2 resizerect = new Vector2(x2,y);            
             m_RawImage.transform.GetComponent<RectTransform>().sizeDelta = resizerect;


             m_RawImage.texture = m_PickedTexture;
            

            okbutton.interactable = true;
            rotatebutton.interactable = true;
            loadText.text = "图片上传成功，请确定";
        }
    }

    public static Texture2D GetSelectedTexture()
    {
        return m_PickedTexture;
    }



    public void imagerotate()
    {
          
        
            Texture2D texture = m_PickedTexture;
            int width = texture.width;  //图片原本的宽度
            int height = texture.height;  //图片原本的高度
            Texture2D newTexture = new Texture2D(height, width);

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    Color color = texture.GetPixel(i, j);
                    newTexture.SetPixel(j, width - 1 - i, color);
                }
            }
            newTexture.Apply();
            m_PickedTexture = newTexture;



        float y = m_RawImage.transform.GetComponent<RectTransform>().sizeDelta.y;
        float x = m_RawImage.transform.GetComponent<RectTransform>().sizeDelta.x;
        float x2 = y * m_PickedTexture.width / m_PickedTexture.height;
        Vector2 resizerect = new Vector2(x2, y);
        m_RawImage.transform.GetComponent<RectTransform>().sizeDelta = resizerect;

        m_RawImage.texture = m_PickedTexture;

    }
















}
