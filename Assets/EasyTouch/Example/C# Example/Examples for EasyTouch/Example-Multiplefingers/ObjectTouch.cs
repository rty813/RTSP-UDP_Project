using UnityEngine;
using System.Collections;

public class ObjectTouch : MonoBehaviour {


	private Camera cam;
	private Vector3 deltaPosition;
	
	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;	
		EasyTouch.On_SimpleTap += On_SimpleTap;	
		EasyTouch.On_LongTap += On_LongTap;	
		EasyTouch.On_DragStart += On_DragStart;	
		EasyTouch.On_Drag += On_Drag;	
		EasyTouch.On_DragEnd += On_DragEnd;
		EasyTouch.On_PinchIn += On_PinchIn;
		EasyTouch.On_PinchOut += On_PinchOut;
		
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;	
		EasyTouch.On_SimpleTap -= On_SimpleTap;	
		EasyTouch.On_LongTap -= On_LongTap;	
		EasyTouch.On_DragStart -= On_DragStart;	
		EasyTouch.On_Drag -= On_Drag;	
		EasyTouch.On_DragEnd -= On_DragEnd;
		EasyTouch.On_PinchIn -= On_PinchIn;
		EasyTouch.On_PinchOut -= On_PinchOut;		
	}
	
	void Start(){
		cam = Camera.main;
	}
	
	void FixedUpdate(){
	
		Vector2 screenPos = cam.WorldToScreenPoint( GetComponent<Rigidbody>().position);
		
		if ( screenPos.x> Screen.width ||screenPos.y<0 ||screenPos.y> Screen.height)
			Destroy( gameObject);
			
		if (screenPos.x<transform.localScale.x/2){
			GetComponent<Rigidbody>().AddForce( GetComponent<Rigidbody>().velocity *-100);
		}
	}
	
	
	
	// ****************************************************
	// Easy Touch Message
	// ****************************************************
	
	void On_TouchStart(Gesture gesture){
		
		if (gesture.pickObject == gameObject){
			GetComponent<Rigidbody>().constraints  = RigidbodyConstraints.FreezeAll;
		}
	}
	
	// Simple Touch message
	void On_SimpleTap(Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			GameObject child=null;
			
			GetComponent<Rigidbody>().constraints  = RigidbodyConstraints.None;
			
			foreach (Transform childreen in transform ){
				if (childreen.name=="ring")
					child = childreen.gameObject;
			}
			
			if (child==null){
		
				GameObject ring = Instantiate(Resources.Load("Ring01"), transform.position , Quaternion.identity) as GameObject;
				ring.transform.localScale = transform.localScale * 1.5f;
				ring.AddComponent<SlowRotate>();
				ring.GetComponent<Renderer>().material.SetColor ("_TintColor", GetComponent<Renderer>().material.GetColor("_TintColor"));
				
				ring.transform.parent = transform;
				ring.name="ring";
			
			}
			else{
				Destroy( child);
			}
		
		}
		
	}
	
	// Long Touch message
	void On_LongTap(Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			GameObject child=null;
			
			GetComponent<Rigidbody>().constraints  = RigidbodyConstraints.None;
			
			foreach ( Transform childreen in transform){
				if (childreen.name=="ring")
					child = childreen.gameObject;
			}
			
			if (child!=null){
				child.GetComponent<SlowRotate>().rotateSpeed *= 1.1f;
			}
		}
	}
	
	// Drag_Start
	void On_DragStart(Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			Vector3 position = gesture.GetTouchToWordlPoint(8);
			deltaPosition = position - GetComponent<Rigidbody>().position;
			
			GetComponent<Rigidbody>().constraints  = RigidbodyConstraints.None;
		}
	}
	
	// Drag Message => move
	void On_Drag(Gesture gesture){
			
		if (gesture.pickObject == gameObject){
			Vector3 position = gesture.GetTouchToWordlPoint(8);
			
			GetComponent<Rigidbody>().position = position - deltaPosition;
		}
		
	}
	
	// Drag end => Add force whith the delta position
	void On_DragEnd( Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			GetComponent<Rigidbody>().AddForce( gesture.deltaPosition *  gesture.swipeLength/10 );
		}
		
	}
	
	// Pinch for size
	void On_PinchIn(Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			float zoom = Time.deltaTime * gesture.deltaPinch;
		
			Vector3 scale = transform.localScale ;
			transform.localScale = new Vector3( scale.x - zoom, scale.y -zoom,1);
		}
	}
	
	void On_PinchOut(Gesture gesture){
	
		if (gesture.pickObject == gameObject){
			float zoom = Time.deltaTime * gesture.deltaPinch;
		
			Vector3 scale = transform.localScale ;
			transform.localScale = new Vector3( scale.x + zoom, scale.y +zoom,1);
		}
		
	}
}
