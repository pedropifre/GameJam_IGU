using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootMouse : MonoBehaviour
{
    private Camera _mainCam;
    private Vector3 _mousePos;
    public float pointMidleAngle = 1.67f;
    private bool _isOn = false;

    private void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector3 VScreen = new Vector3();


        VScreen.x = Input.mousePosition.x;
        VScreen.y = Input.mousePosition.y *pointMidleAngle;
        VScreen.z = _mainCam.transform.position.z;

        _mousePos = Camera.main.ScreenToWorldPoint(VScreen)-transform.position;
        //Debug.Log("Mouse Pos: "+_mousePos);
        //_mousePos.Normalize();


        float rotz = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;

        //Debug.Log(rotz);
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        
    }

}
