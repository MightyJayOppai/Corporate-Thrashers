using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	
	public Vector2 mouselook;
    public Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject player;


    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    void Update()
    {
        var mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (mouselook.y >= 40)
        {
            mouselook.y = 40;
        }

        if (mouselook.y <= -40)
        {
            mouselook.y = -40;
        }
        mouseMovement = Vector2.Scale(mouseMovement, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseMovement.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseMovement.y, 1f / smoothing);
        mouselook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);
        
    }
}
