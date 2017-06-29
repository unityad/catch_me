using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private PlayerMoveJoystick player;

    void Awake()
    {
        player = GameObject.Find("Sperm_1").GetComponent<PlayerMoveJoystick>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "left")
        {
            player.SetMoveLeft(true);
            //Debug.Log ("left");
        }
        else
        {
            player.SetMoveLeft(false);
            //Debug.Log("right");
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        player.StopMoving();
    }
}




































