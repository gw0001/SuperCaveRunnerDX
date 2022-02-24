using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerController _player;
    private ScreenInfo _screenInfo;

    private Vector3 _origin;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _screenInfo = GameObject.FindObjectOfType<ScreenInfo>();

        _origin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cameraPosition = transform.position;




        if(_player.AboveHead > _screenInfo.TopEdge)
        {
            float difference = _player.AboveHead - _screenInfo.TopEdge;

            cameraPosition.y = difference;
        }
        else
        {
            cameraPosition.y = _origin.y;
        }


        transform.position = cameraPosition;

    }
}
