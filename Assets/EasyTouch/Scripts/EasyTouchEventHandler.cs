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
        if (move.joystickName == "New joystick")  
        {  
             print("Move End");
        }  
    }  
  
  
    //移动摇杆中  
    void OnJoystickMove(MovingJoystick move)  
    {  

		if (move.joystickName == "New joystick"){
        	//获取摇杆中心偏移的坐标  
	        float joyPositionX = move.joystickAxis.x;  
    	    float joyPositionY = move.joystickAxis.y;  
			print(joyPositionX + " . " + joyPositionY);
            int x = (int)(550 + joyPositionX * 200) / 10 * 10;
            int y = (int)(550 + joyPositionY * 250) / 10 * 10;
            string strX = "1 " + x.ToString();
            string strY = "2 " + y.ToString();

            MedaiPlayerSampleGUI.str_show = strX + "; " + strY;
            gameObject.GetComponent<UdpServer>().SocketSend(strX);
            gameObject.GetComponent<UdpServer>().SocketSend(strY);
		}
    }  
}
