using UnityEngine;
using System.Collections;
using RTS;
	
public class UserInput : MonoBehaviour {
	private Player player;
	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent < Player >();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.human){
			MoveCamera();
			RotateCamera();
		}
	}
	
	//movecamera
	private void MoveCamera(){
		float xpos = Input.mousePosition.x;
		float ypos = Input.mousePosition.y;
		Vector3 movement = new Vector3(0,0,0);
		//горизонтальное перемещение камеры
		if (xpos >= 0 && xpos < ResourceManager.ScrollWigth)
		{
			movement.x -=ResourceManager.ScrollSpeed;
		}
		else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWigth){
			movement.x += ResourceManager.ScrollSpeed;
		}
		
		//вертикальное перемещение камеры
	
		if (ypos >=0 && ypos < ResourceManager.ScrollWigth){
			movement.z -= ResourceManager.ScrollSpeed;
		}
		else if(ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWigth){
			movement.z += ResourceManager.ScrollSpeed;
		}
		
		movement = Camera.mainCamera.transform.TransformDirection(movement);
		movement.y = 0;
		
		movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
		
		//визначення положення камери, базуючись на отриманих даних
		Vector3 origin = Camera.mainCamera.transform.position;
		Vector3 destination = origin;
		destination.x +=movement.x;
		destination.y +=movement.y;
		destination.z +=movement.z;
		
		//установка лимита перемещений
		if(destination.y > ResourceManager.MaxCameraHeight){
			destination.y = ResourceManager.MaxCameraHeight;
		}
		else if (destination.y < ResourceManager.MinCameraHeight){
			destination.y  = ResourceManager.MinCameraHeight;
		}
	}
	
	private void RotateCamera(){
		Vector3 origin = Camera.mainCamera.transform.eulerAngles;
		Vector3 destination = origin;
		//крутить при нажатом альте и правой кнопки
		if ((Input.GetKey(KeyCode.LeftAlt)|| Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)){
			destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
			destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
		}
		
		if (destination != origin){
			Camera.mainCamera.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
		}
	}
}
