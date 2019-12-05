package com.yuji.face;

import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

public class UnityEmbededActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        RelativeLayout layout = new RelativeLayout(this);
        setContentView(layout);

        RelativeLayout.LayoutParams mview_params = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT);

        mview_params.width=600;
        mview_params.height=600;
        mview_params.addRule(RelativeLayout.CENTER_IN_PARENT);

        layout.addView(mUnityPlayer.getView(),mview_params);
    }
    @Override
    public boolean onKeyDown(int keycode,KeyEvent event)
    {
        if(keycode == KeyEvent.KEYCODE_BACK)
        {
            mUnityPlayer.quit();
            Log.e("key","key");
        }
        return super.onKeyDown(keycode,event);
    }
}
