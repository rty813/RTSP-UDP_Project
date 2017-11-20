using UnityEngine;
using System.Collections;

public class EasyTouchEventHandler : MonoBehaviour {
	void OnEnable(){
		EasyJoystick.On_JoystickMove += OnJoystickMove;
		EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
	}

	//移动摇杆结束  
    void OnJoystickMoveEnd(MovingJoystick move)  
    {  
        //停止时，角色恢复idle  
        if (move.joystickName == "MainJoystick")  
        {  
             print("Move End");
        }  
    }  
  
  
    //移动摇杆中  
    void OnJoystickMove(MovingJoystick move)  
    {  

		if (move.joystickName == "MainJoystick"){
        	//获取摇杆中心偏移的坐标  
	        float joyPositionX = move.joystickAxis.x;  
    	    float joyPositionY = move.joystickAxis.y;  
			print(joyPositionX + " . " + joyPositionY);
            gameObject.GetComponent<UdpServer>().SocketSend(joyPositionX.ToString()+","+joyPositionY.ToString());
            MedaiPlayerSampleGUI.str_show = joyPositionX.ToString()+","+joyPositionY.ToString();
			// gameObject.GetComponent<UdpServer>().SocketSend("hello world");
		}
    }  
}
