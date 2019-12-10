using UnityEngine;  
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FreeView : MonoBehaviour
{
    static FreeView _inst;
    public static FreeView Inst()
    {
        return _inst;
    }

    public   Slider CameraSlider;	
	public GameObject stage;
    public GameObject targetobject;
    public Transform Target;  //观察目标 

    public float Target_z;
    public float humanHigh=0;

    public float Distance = 830f;  	//观察距离  
    private float MaxDistance = 1500;
    private float MinDistance = 600f;  //鼠标缩放距离最值
    public  float ZoomSpeed = 100f;  //鼠标缩放速率 
    

    private float SpeedX=10;  
	private float SpeedY=100;  //旋转速度  

    private float  MinLimitY = -20F;
    private float  MaxLimitY = 40F;  //角度限制  

	private float mX = 0.0F;  
	private float mY = 0.0F;    //旋转角度  
    
	//private float viewtarget;    //目标点位置
	 
	public bool isNeedDamping=false;  //是否启用差值 
    public float Damping=10F;  //速度  

	private Quaternion mRotation;  	//存储角度的四元数  
	private Quaternion stageRotation;
	
 	private enum MouseButton  	//定义鼠标按键枚举 
		{  
		MouseButton_Left=0,  	//鼠标左键 
		MouseButton_Right=1,  	//鼠标右键  
		MouseButton_Midle=2  	//鼠标中键  
		}  
	
	//private float MoveSpeed=20.0F; //相机移动速度   
	private Vector3 mScreenPoint; 	//屏幕坐标   
	private Vector3 mOffset;  	//坐标偏移  

	private Vector2 mPos;  //当前手势  


	
    void Awake()
    {
        _inst = this;
        		
    }
    


    public void OnCameraHeadBtnClk() {
       
        CameraSlider.value = humanHigh;
        Distance = 700f;
        ZoomSpeed = 100f;
        MinDistance = 400f;
        MaxDistance = 5000F;
        Vector3 mPosition = mRotation * new Vector3(0.0F, 0F, -Distance) + Target.position;  //重新计算位置 
    }


    public void OnCamraHalfBtnClk() {

        CameraSlider.value = humanHigh - 450f ;
        Distance = 3200;
        ZoomSpeed = 120f;

        MinDistance = 900f;
        MaxDistance = 6000F;
    }

    public void OnCamraAllBtnClk()
    {

        CameraSlider.value = humanHigh-850f;
        Distance = 6000;
        ZoomSpeed = 160F;
        MinDistance = 900F;
        MaxDistance = 7000f;
    }




#if UNITY_EDITOR
//#if UNITY_ANDRIOD


    void Start ()   
		{    
      

       CameraSlider.maxValue = humanHigh+200;
        Target = targetobject.transform;

		mX=transform.eulerAngles.x;  
		mY=transform.eulerAngles.y;  //初始化旋转角度  
		//Vector3 mPosition =  new Vector3(-0.0F, 0.9F, -Distance-1F) + Target.position; 
		//transform.position = mPosition;
		
        mRotation = Quaternion.Euler(mX, 180, 0);

        Vector3 mPosition = mRotation * new Vector3(0.0F, 0F, -Distance) + Target.position;  //重新计算位置 

        float target_x = targetobject.gameObject.transform.position.x;

        targetobject.gameObject.transform.position = new Vector3(target_x, CameraSlider.value , Target_z);
       // OnCamraAllBtnClk();
    }  



	public  void Chposition(){
        float positionnew = CameraSlider.value;
        float target_x = targetobject.gameObject.transform.position.x;
        targetobject.gameObject.transform.position = new Vector3(target_x, positionnew, Target_z);
      //  print(positionnew);
	}


	void LateUpdate ()   
	{  

		if(Target!=null && Input.GetMouseButton((int)MouseButton.MouseButton_Right))  //鼠标中键旋转  
		{



            if (EventSystem.current.IsPointerOverGameObject())

            {
               // Debug.Log("当前触摸在UI上");
            }

            else
            {
               // Debug.Log("当前没有触摸在UI上");

                mX += Input.GetAxis("Mouse Y") * SpeedX * 0.1F;
                mY += Input.GetAxis("Mouse X") * SpeedY * 0.1F;  //获取鼠标输入 

                mX = ClampAngle(mX, MinLimitY, MaxLimitY);      //范围限制  

                mRotation = Quaternion.Euler(mX, 180, 0);   //计算旋转  
                stageRotation = Quaternion.Euler(0, 180 + mY, 0);



                if (isNeedDamping)
                {  //根据是否插值采取不同的角度计算方式  
                    transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);
                }
                else
                {
                    transform.rotation = mRotation;
                    //print(mRotation);
                    stage.gameObject.transform.rotation = stageRotation;

                }


            }
              

        }  
		


		Distance-=Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;  //鼠标滚轮缩放 
		Distance=Mathf.Clamp(Distance,MinDistance,MaxDistance);

        
       Vector3 mPosition =  mRotation * new Vector3(0.0F, 0F, -Distance) + Target.position;  //重新计算位置  
       // Vector3 mPosition = new Vector3(0.0F, 0F, -Distance) + Target.position;

        //设置相机的位置  
        if (isNeedDamping){  
			transform.position = Vector3.Lerp(transform.position,mPosition, Time.deltaTime*Damping);   
		}else{  
			transform.position = mPosition;  
		}  
		
	}  
	
	
	//角度限制  
	private float  ClampAngle (float angle,float min,float max)   
	{  
		if (angle < -360) angle += 360;  
		if (angle >  360) angle -= 360;  
		return Mathf.Clamp (angle, min, max);  
	}
#else
	void Start ()   
	{  
   

     CameraSlider.maxValue = humanHigh+20 ;
     Target = targetobject.transform;

    Input.multiTouchEnabled=true;  //允许多点触控  

	mX=Target.eulerAngles.x;  	//初始化旋转  
	mY=Target.eulerAngles.y;



        stageRotation = Quaternion.Euler(0, mY, 0);

        transform.position = transform.rotation * new Vector3(0f, 0, -Distance) + targetobject.gameObject.transform.position;  //重新计算位置  

     float target_x = targetobject.gameObject.transform.position.x;
        targetobject.gameObject.transform.position = new Vector3(target_x, CameraSlider.value, Target_z);     

  

    }  

	public  void Chposition(){
    float positionnew = CameraSlider.value;
 float target_x = targetobject.gameObject.transform.position.x;
	targetobject.gameObject.transform.position = new Vector3(target_x,positionnew, Target_z);
	}


    void Update ()   
	{  
	if(!Target) return;  

	    if(Input.touchCount==1)  //单点触控 
	    {  
	        if(Input.touches[0].phase==TouchPhase.Moved )//手指处于移动状态  
	        {

               	if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))

                {
    
                }
                else
                {    
    
                    mX += Input.GetAxis("Mouse Y") * SpeedX * 0.03F;
                    mY -= Input.GetAxis("Mouse X") * SpeedY * 0.03F;
                    mX = ClampAngle(mX, MinLimitY, MaxLimitY);
    
                 }


	        }  
	     }  


	if(Input.touchCount>1) //多点触控 
	{  
	if(Input.touches[0].phase==TouchPhase.Moved || Input.touches[1].phase==TouchPhase.Moved) //两只手指都处于移动状态   
	        {
                if (Input.touches[0].position.y >= Screen.height / 5 || Input.touches[1].position.y >= Screen.height / 5)
                {

                    Vector2 mDir = Input.touches[1].position - Input.touches[0].position;  //计算移动方向  


                    if (mDir.sqrMagnitude > mPos.sqrMagnitude)
                    {
                        //根据向量的大小判断当前手势是放大还是缩小  
                        Distance -= ZoomSpeed/4f;
                    }
                    else
                    {
                        Distance += ZoomSpeed/4f;
                    }



                    Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);  //限制距离  
                    mPos = mDir;      //更新当前手势  

                }

                else { }
            }  

	}  

	//计算相机的角度和位置  
	transform.rotation=Quaternion.Euler(new Vector3(12f+mX,180,0)); 
	stageRotation =Quaternion.Euler (0,mY,0);

	stage.gameObject.transform.rotation =stageRotation ;
	transform.position=transform.rotation * new Vector3(0f,0,-Distance)+targetobject.gameObject.transform.position ;  
	}  

	//角度限制  
	private float ClampAngle (float angle,float min,float max)   
	{  
	if (angle < -360) angle += 360;  
	if (angle >  360) angle -= 360;  
	return Mathf.Clamp (angle, min, max);  
	}  
	
    

#endif


}