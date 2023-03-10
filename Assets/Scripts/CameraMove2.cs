

using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Runtime.InteropServices;




public class CameraMove2 : MonoBehaviour
{
    public GameObject player;
    public float sensivity = 5f;
    public float smoothTime = 0.1f;
    public float speedCam;
    public float speed =5f;
    public float minDistance;
    public float maxDistance;
    public float speedScroll;
    public bool changePlayer = true;
    public float headMinY =-40f; 
    public float headMaxY=40f;
    public float camTop = 3f;
    public float targetRotation = 60f;
    private bool isRotation = false;
    private float dirRotation = 0f;
    private float _x;
    private float _y;
    private float _xRotCurrent;
    private float _yRotCurrent;
    private float _currentVelosityX;
    private float _currentVelosityY;
    bool _isActive;
    private float _distance;
    
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    void Update()
    {
        PersonCamera_1_eye();
        MovePlayer_Forvard();
    }
    
    void PersonCamera_1_eye()
    {
        Vector2 _ScreenSize = new Vector2(Screen.width,Screen.height);
        Vector2 _ScreenPosition = Screen.mainWindowPosition;
        Vector2 _MousePosition = Input.mousePosition;
        
        
        
        if (changePlayer)
        {
            print("changePlayer = "+changePlayer);
            
            Vector3 newPosition = player.transform.position;
            float rotationX = player.transform.localEulerAngles.y;
            
            newPosition.y = camTop;

            
            
            transform.position = newPosition;
            changePlayer = false;
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            
            
            SetCursorPos((int) _ScreenPosition.x + Screen.width/2, (int) _ScreenPosition.y + Screen.height/2);
        }

        


        if (_MousePosition.x > 0 && _MousePosition.y > 0 && _MousePosition.x<_ScreenSize.x && _MousePosition.y<_ScreenSize.y)
        {
            Vector2 _centerScreen = new Vector2(_ScreenSize.x/2,_ScreenSize.y/2);
            float _dist = Vector2.Distance(_MousePosition, _centerScreen);

            float _rotation_y = 0f;
            
            if (Mathf.Abs(_MousePosition.x-_centerScreen.x) > 30)
            {
                _rotation_y = 40f; 
                if (_MousePosition.x < _centerScreen.x)
                {
                    _rotation_y = -_rotation_y;
                }
                player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
            }
            Vector3 _camAngle = player.transform.eulerAngles;
            float _headAngle = (((_MousePosition.y) / _ScreenSize.y) * (headMaxY - headMinY)) - headMaxY;

            _camAngle.x = -_headAngle;
            transform.localEulerAngles = _camAngle;
            
        }

        
        
    }

    void MovePlayer_Forvard()
    {

        
        if (Input.GetKey(KeyCode.W))
        {
            //player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            player.GetComponent<Rigidbody>().velocity = player.transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 m_Input = new Vector3(transform.position.x + 2f, 0, transform.position.z + 2f);
            player.GetComponent<Rigidbody>().MovePosition(transform.position + m_Input * Time.deltaTime * 2f);
        }

        if (Input.GetAxisRaw("Horizontal")!=0f && !isRotation) dirRotation = Input.GetAxisRaw("Horizontal");
        
        if (dirRotation!=0f || isRotation)
        {

            Rigidbody rb = player.GetComponent<Rigidbody>();
            
            float turn = dirRotation * 60f * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);

            isRotation = true;
            
            print(dirRotation+" : "+player.transform.eulerAngles.y+" : "+targetRotation);
            if (player.transform.eulerAngles.y == targetRotation)
            {
                isRotation = false;
                dirRotation = 0f;

            }
        }
        
    
        
        
        
        transform.position = new Vector3(player.transform.position.x, camTop, player.transform.position.z-2);
        
        
    }

    void PersonCamera_3_eye()
    {
        _x = Input.GetAxis("Mouse X")*speedCam*10;
        _y= Input.GetAxis("Mouse Y")*-speedCam*10;

        if (Input.GetMouseButtonDown(1))
        {
            _isActive=true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _isActive=false;
        }

        if (_isActive)
        {
            transform.RotateAround(player.transform.position,transform.up,_x*Time.deltaTime);
            transform.RotateAround(player.transform.position,transform.right,_y*Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(player.transform.position-transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0 );
           
        }
        // приближение удаление
        if (Input.GetAxis("Mouse ScrollWheel") !=0 )
        {
            _distance = Vector3.Distance(transform.position, player.transform.position);
            print("distance = "+_distance+" : min="+minDistance);
            if (Input.GetAxis("Mouse ScrollWheel")>0 && _distance > minDistance )
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speedScroll);
            }

            if (Input.GetAxis("Mouse ScrollWheel")<0 && _distance < maxDistance )
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -speedScroll);
            }
        }
    }



}
