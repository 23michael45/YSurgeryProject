using UnityEngine;

using System.Collections;

using System.IO;

using System.Collections.Generic;

using System;



public class SaveFile : MonoBehaviour
{
       
    string localpath;
    public Material facematerial;

    private void Start()
    {
        
        string url = "https://yjkj-0508.oss-cn-shenzhen.aliyuncs.com/FAC:553418fd01d14377bd6bc590cdcac1d5.tmp";

        string name = "face";
        string filetype = ".obj";
        print(localpath);
        StartCoroutine(LoadAndSaveAsset(name,url, filetype));       
    }
       

    IEnumerator LoadAndSaveAsset(string name,string url,string filetype)
    {

        localpath = Application.persistentDataPath + "/model/" + name;
        string progress = null;       

        Debug.Log(url);
        Debug.Log("开始下载模型。");
        WWW w = new WWW(url);
        while (!w.isDone)
        {
            progress = (((int)(w.progress * 100)) % 100) + "%";
           // loadText.text = "下载模型中" + progress;
            yield return null;
        }
        yield return w;
        if (w.isDone)
        {
           // loadText.text = "保存模型中";
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
            FileInfo j = new FileInfo(localpath + "/" + name+ filetype);
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

        Debug.Log(name + filetype);



       RuntimeLoadObj.RutimeLoadObj(localpath + "/" + name + filetype);




    }


















}