package com.yuji.face;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.os.Environment;
import android.support.constraint.ConstraintLayout;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.Toast;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Paths;

public class UnityEmbededActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_unity_embeded);

        ConstraintLayout layout =(ConstraintLayout)findViewById(R.id.ViewPlaceLayout);
        layout.addView(mUnityPlayer.getView());


        Button calculateBtn = (Button)findViewById(R.id.CalcuatePolyBtn);
        calculateBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Log.i("CalcuateBtn","Click CalcuateBtn");
                try {
                    InputStream is=getAssets().open("Model/obama53149.obj");

                    byte[] hdObjData=new byte[is.available()];
                    is.read(hdObjData);
                    is.close();

                    String retJsonStr = "{\"ret\":0,\"retMsg\":\"操作成功\",\"info\":\"{\\\"meshFile\\\":\\\"https://yjkj-0508.oss-cn-shenzhen.aliyuncs.com/FAC:796846f41e7443c09318310f06d02c87.mesh\\\",\\\"calcRet\\\":\\\"{\\\\n\\\\t\\\\\\\"hsv_offset\\\\\\\": {\\\\n\\\\t\\\\t\\\\\\\"h\\\\\\\": 0.0,\\\\n\\\\t\\\\t\\\\\\\"s\\\\\\\": 35.45960236825087,\\\\n\\\\t\\\\t\\\\\\\"v\\\\\\\": -35.737425387350388\\\\n\\\\t}\\\\n}\\\",\\\"TextureFile\\\":\\\"https://yjkj-0508.oss-cn-shenzhen.aliyuncs.com/FAC:646e7a4f055a492abac118bf696e2363.jpg\\\"}\"}";

                    String roleJson = YSurgeryUnityInterface.instance.CalculateLowPolyFace(hdObjData,0,180,75,retJsonStr);
                    writeToFile(roleJson,"obama53149_role.json");

                } catch (IOException e) {
                    e.printStackTrace();
                }


                Toast.makeText(UnityEmbededActivity.this,"CalculateLowPolyFace Finish",Toast.LENGTH_LONG).show();
            }
        });


        Button loadPolyBtn = (Button)findViewById(R.id.LoadRoleBtn);
        loadPolyBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("LoadRoleBtn","Click LoadRoleBtn");
                try {
//                    InputStream is=getAssets().open("Model/obama53149_role.json");
//
//                    int size = is.available();
//                    byte[] buffer = new byte[size];
//                    is.read(buffer);
//                    is.close();
//
//                    // byte buffer into a string
//                    String roleJson = new String(buffer);

                    String roleJson = ReadFromFile("obama53149_role.json");

                    InputStream is=getAssets().open("Model/obamaTexture.jpg");

                    byte[] textureData=new byte[is.available()];
                    is.read(textureData);
                    is.close();

                    boolean ret = YSurgeryUnityInterface.instance.LoadLowPolyFace(roleJson,textureData);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        });



        Button saveDeformBtn = (Button)findViewById(R.id.SaveDeformBtn);
        saveDeformBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("SaveDeformBtn","Click SaveDeformBtn");
                String deformJson = YSurgeryUnityInterface.instance.SaveDeform();
                writeToFile(deformJson,"obama53149_deform.json");

                Toast.makeText(UnityEmbededActivity.this,"SaveDeform Finish",Toast.LENGTH_LONG).show();
            }
        });

        Button loadDeformBtn = (Button)findViewById(R.id.LoadDeformBtn);
        loadDeformBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("LoadDeformBtn","Click LoadDeformBtn");
//                try {
//                    InputStream is=getAssets().open("Model/obama53149_deform.json");
//
//                    int size = is.available();
//                    byte[] buffer = new byte[size];
//                    is.read(buffer);
//                    is.close();
//
//                    // byte buffer into a string
//                    String defromJson = new String(buffer);

                    String defromJson = ReadFromFile("obama53149_deform.json");

                    YSurgeryUnityInterface.instance.LoadDeform(defromJson);
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
            }
        });


        Button enterEditBtn = (Button)findViewById(R.id.EnterEditBtn);
        enterEditBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("EnterEditBtn","Click EnterEditBtn");
                YSurgeryUnityInterface.instance.EnterEditMode();
            }
        });

        Button exitEditBtn = (Button)findViewById(R.id.ExitEditBtn);
        exitEditBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("ExitEditBtn","Click ExitEditBtn");
                YSurgeryUnityInterface.instance.ExitEditMode();
            }
        });


       final Activity activity = this;

        Button fullScreenBtn = (Button)findViewById(R.id.FullScreenBtn);
        fullScreenBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("FullScreenBtn","Click FullScreenBtn");
                YSurgeryUnityInterface.instance.FullScreen(activity,true);
            }
        });

        Button notFullScreenBtn = (Button)findViewById(R.id.NotFullScreenBtn);
        notFullScreenBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Log.i("NotFullScreenBtn","Click NotFullScreenBtn");
                YSurgeryUnityInterface.instance.FullScreen(activity,false);
            }
        });
    }
    @Override
    public boolean onKeyDown(int keycode,KeyEvent event)
    {
        if(keycode == KeyEvent.KEYCODE_BACK)
        {
            mUnityPlayer.quit();

            Intent intent = new Intent(UnityEmbededActivity.this,MainActivity.class);
            startActivity(intent);
        }
        return super.onKeyDown(keycode,event);
    }


    private void writeToFile(String content,String path) {
        try {
            File file = new File(Environment.getExternalStorageDirectory() + "/" + path);

            if (!file.exists()) {
                file.createNewFile();
            }
            FileWriter writer = new FileWriter(file);
            writer.append(content);
            writer.flush();
            writer.close();
        } catch (IOException e) {
        }
    }

    private String ReadFromFile(String path) {
        try {

            String fullpath = Environment.getExternalStorageDirectory() + "/" + path;
//            File file = new File(path);

//            if (!file.exists()) {
//                return "";
//            }
            String content = new String(Files.readAllBytes(Paths.get(fullpath)));
            return content;

        } catch (IOException e) {
        }
        return "";
    }
}
