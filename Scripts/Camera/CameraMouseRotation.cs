using System;
using UnityEngine;

public class CameraMouseRotation : MonoBehaviour
{
	 public float sensitivity = 2f;

     private void Start()
     {
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
     }

     void Update()
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
    
            // Rotate the camera based on mouse input
            RotateCamera(mouseX, mouseY);
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnlockCursor();
            }
        }
    
        void RotateCamera(float mouseX, float mouseY)
        {
            // Adjust the rotation based on mouse input
            Vector3 rotation = transform.eulerAngles;
            rotation.y += mouseX * sensitivity;
            rotation.x -= mouseY * sensitivity;
    
            // Clamp the vertical rotation to avoid flipping
            rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
    
            // Apply the new rotation
            transform.eulerAngles = rotation;
        }
        
        void UnlockCursor()
        {
            // Unlock the cursor and show it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
}
