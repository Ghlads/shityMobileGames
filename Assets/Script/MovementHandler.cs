using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    [SerializeField]
    float speed = 0.15f;// vitesse du player
    [SerializeField]
    private Vector2 boundary; // limite que le personnage ne peut franchire sur l'axe X : x limite negative y limiter positive 

    private Control control;

    private void Start()
    {
        control = new Control();
        control.Player.Enable();
    }

    private float value = 0;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private bool Checkboundary(Vector2 boundary,float xFuturePosition)
    {
        if (xFuturePosition > boundary.x && xFuturePosition< boundary.y)
        {
            return true;
        }
        return false;
    }
    
    private void FixedUpdate()
    {
        value = control.Player.Direction.ReadValue<float>();
        Vector3 FuturePosition = this.transform.position + new Vector3(value * speed, 0, 0);
        if (Checkboundary(boundary, FuturePosition.x))
        {
            this.transform.position += new Vector3(value * speed, 0, 0);
        }
    }
}
