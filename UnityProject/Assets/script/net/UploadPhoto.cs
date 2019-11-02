﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using UnityEngine.UI;




[Serializable]
class JData
{
    public int ret;
    public string retMsg;

    [Serializable]
    public class JInfo
    {
        public string meshFile;
        public string TextureFile;
    };

    public JInfo info;
}



public class UploadPhoto : MonoBehaviour
{

    private  string url = "https://m.yujishishi.com/fac/usr/upUsrPicFor3D";

    private Texture2D usrfacephoto;
    private Texture2D Facetexture;

    public Material faematerial;

    string jstr = "{\"ret\":0,\"retMsg\":\"操作成功\",\"info\": {\"meshFile\":\"https://yjkj-0508.oss-cn-shenzhen.aliyuncs.com/FAC:553418fd01d14377bd6bc590cdcac1d5.tmp\",\"TextureFile\":\"https://yjkj-0508.oss-cn-shenzhen.aliyuncs.com/FAC:246855da2d3f4094a3db87edc53a6119.tmp\"}}";
    //string jstr;
    private string MeshUrl;
    private string TextureUrl;

    private  Mesh headmesh;
    private  Texture2D headtex;
    
   private   string localpath;


    public Text loadText;




    public void uploadImgClick()
    {

        //getjsoninfo(jstr);  

        StartCoroutine(UploadPNG());
        print(jstr);

        loadText.text = "开始上传照片";
    }


    private IEnumerator UploadPNG()
    {

        //usrfacephoto = Resources.Load("test") as Texture2D;   

        usrfacephoto = PhotoSelector.GetSelectedTexture();

        byte[] bytes = usrfacephoto.EncodeToJPG();
        print(usrfacephoto.width);

       

        WWWForm form = new WWWForm();        

        form.AddBinaryData("fileUpload", bytes, "test", "image/jpg");
        print(bytes);

        using (var w = UnityWebRequest.Post(url, form))
        {
            yield return w.SendWebRequest();
           

            if (w.isNetworkError)
            {
                   print(w.error);
                print("isNetworkError");
            }
            else if (w.isHttpError) {

                print(w.error);
                print("isHttpError");
            }
            else
            {
                print("Finished Uploading Screenshot");
                print(w.downloadHandler.text);


                string data = w.downloadHandler.text.ToString();
                print(data);

                yield return data ;
                loadText.text = "云计算完成，开始加载模型";

                //重新复制json 串
                jstr = data;

                print(jstr);
                jstr = jstr.Replace("\\\"", "\"");
                jstr = jstr.Replace("\"{\"", "{\"");
                jstr = jstr.Replace("\"}\"", "\"}");
                print(jstr);
                                                          
                getjsoninfo(jstr);

            }


        }

    }


     void  getjsoninfo(string data) {

        var jsondata = JsonUtility.FromJson<JData>(data);
        var ret = jsondata.ret.ToString();
        var retMsg = jsondata.retMsg.ToString();


        MeshUrl = jsondata.info.meshFile;
        Debug.Log(MeshUrl);

        string meshname = "face";       
        StartCoroutine(LoadAndSaveAsset(meshname, MeshUrl, ".obj"));
        


        TextureUrl = jsondata.info.TextureFile;
        Debug.Log(TextureUrl);

        string Texname = "facetex";      
        StartCoroutine(LoadAndSaveAsset(Texname, TextureUrl, ".jpg"));  
      
        
    }






    IEnumerator LoadAndSaveAsset(string name, string url, string filetype)
    {

        localpath = Application.persistentDataPath + "/model/" ;
        string progress = null;

        Debug.Log(url);
        Debug.Log("开始下载模型。");
        WWW w = new WWW(url);
        while (!w.isDone)
        {
            progress = (((int)(w.progress * 100)) % 100) + "%";

            //提示字符
            if (filetype == ".obj") { loadText.text = "下载模型中" + progress; } else { }            

            yield return null;
        }
        yield return w;
        if (w.isDone)
        {

            //提示字符
            if (filetype == ".obj")  { loadText.text = "保存模型中";  }  else {}    

            byte[] model = w.bytes;
            int length = model.Length;

            //文件流信息  
            Stream sw;

            DirectoryInfo t = new DirectoryInfo(localpath);
            if (!t.Exists)
            {
                //如果此文件夹不存在则创建  
                t.Create();
            }
            FileInfo j = new FileInfo(localpath + name + filetype);
            if (!j.Exists)
            {
                //如果此文件不存在则创建  
                sw = j.Create();
            }
            else
            {
                //如果此文件存在则打开  
                sw = j.OpenWrite();
            }
            sw.Write(model, 0, length);
            //关闭流  
            sw.Close();
            //销毁流  
            sw.Dispose();


            //写文件后加载模型      


        }
         yield return null;

        if (filetype == ".obj")
        {

            loadText.text = "下载完毕，显示模型到场景 ";

            GameObject face = RuntimeLoadObj.RutimeLoadObj(localpath  + name + filetype);
            Debug.Log(face);

            if (face.transform.childCount!= 0)
            {
                face.GetComponentInChildren<MeshRenderer>().material = faematerial;

                //关闭拍照页，进入主页面
                GameObject.Find("Stage").GetComponent<ActiveScene>().closeFirstpage();
            }
            else {


            }



        }

        else 
        {
            Facetexture = w.texture;

            Debug.Log(localpath +  name);

            Debug.Log(Facetexture);

            if (Facetexture != null)
            {
                faematerial.mainTexture= Facetexture;
            }
            else
            {


            }

           


        }





    }



}




















