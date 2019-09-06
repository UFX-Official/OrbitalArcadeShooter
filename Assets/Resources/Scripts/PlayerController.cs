using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    private Vector3 moveDir;
    
    public GameObject body;

    Weapon weapon;

    float angle = 0;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;   
        
        Aiming();
        Shooting();
   
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }

    public bool isLeft(Vector3 a, Vector3 b, Vector3 c)
    {
        //Debug.Log("A: " + a + "\nB: " + b + "\nC: " + c);

        //  a2b3 - a3b2, a3b1 - a1b3, a2b1 - a1b2
        

        //Debug.Log(((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)));

        return ((b.x - a.x) * (c.z - a.z) - (b.z - a.z) * (c.x - a.x)) > 0;
    }

    private void Aiming()
    { 
        Vector3 playerPos;
        playerPos = Camera.main.WorldToScreenPoint(transform.position);
      
        //get the vector representing the mouse's position relative to the point...
        Vector2 v = Input.mousePosition - playerPos;

        //use atan2 to get the angle; Atan2 returns radians
        float angleRadians = Mathf.Atan2(v.y, v.x);        

        //convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        //angleDegrees will be in the range (-180,180].
        //I like normalizing to [0,360) myself, but this is optional..
        if (angleDegrees < 0)
            angleDegrees += 360;

        angle = angleDegrees - 90;
        Vector3 rotation = new Vector3(0, -angle, 0);

        body.transform.localRotation = Quaternion.Euler(rotation);
    }

    private void Shooting()
    {
        if (weapon.GetTriggerType() == TriggerType.automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                weapon.Shoot();
            }
        }

        else if (weapon.GetTriggerType() == TriggerType.semi)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weapon.Shoot();
            }
        }        
    }   

    public Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        return new Vector3((a.y * b.z) - (a.z * b.y), (a.x * b.z) - (a.z * b.x), (a.x * b.y) - (a.y * b.x));
    }

    public Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

}
