using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootMouse : MonoBehaviour
{
    public Camera _mainCam;
    public Vector3 _mousePos;

    private void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector3 VScreen = new Vector3();


        VScreen.x = Input.mousePosition.x;
        VScreen.y = Input.mousePosition.y *1.7f;
        VScreen.z = _mainCam.transform.position.z;

        _mousePos = Camera.main.ScreenToWorldPoint(VScreen)-transform.position;
        //Debug.Log("Mouse Pos: "+_mousePos);
        //_mousePos.Normalize();


        float rotz = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg-180;

        //Debug.Log(rotz);
        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }

}
