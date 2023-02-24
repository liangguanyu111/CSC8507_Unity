using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTPS : MonoBehaviour
{
    float yaw;
    float pitch;

    public float mouseMoveSpeed = 2;
    public Transform playerTransform;
    public Vector3 startOffset;
    public Vector3 aimOffset;
    private Vector3 offset;
    private void Awake()
    {
        offset = playerTransform.transform.position - transform.position;

    }

    private void Update()
    {
        var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
        var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

        //Vector3 offset = playerTransform.transform.position - this.transform.position;

       

        transform.RotateAround(playerTransform.transform.position, Vector3.up, mouse_x * 5);
        transform.RotateAround(playerTransform.transform.position, transform.right, mouse_y * 5);

        Vector3 dir = (playerTransform.transform.position - transform.position).normalized;

        dir.y = 0;

       
        
        //Aim
        if(Input.GetMouseButton(1))
        {
            transform.position = playerTransform.transform.position + (aimOffset.x * playerTransform.right) +aimOffset.y * Vector3.up + aimOffset.z * playerTransform.forward - transform.forward * offset.magnitude;
        }
        else
        {
            transform.position = playerTransform.transform.position + (startOffset.x * playerTransform.right) + startOffset.y * Vector3.up + startOffset.z * playerTransform.forward - transform.forward * offset.magnitude;
        }

    }



}
