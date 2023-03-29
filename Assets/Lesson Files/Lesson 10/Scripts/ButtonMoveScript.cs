using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoveScript : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public int verticalForce;
    public int horizontalForce;
    public Player Player;
    public void OnPointerDown(PointerEventData eventData)
    {
        Player.AddDirectionForce(horizontalForce,verticalForce);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.AddDirectionForce(0,0);
    }
}
