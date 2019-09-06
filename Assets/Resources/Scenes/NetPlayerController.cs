using UnityEngine;

public class NetPlayerController : Bolt.EntityBehaviour<ICustomeCubeState>
{
    public float  moveSpeed = 15;
    private Vector3 movement = Vector3.zero;

    public GameObject body;


    Weapon weapon;

    float angle = 0;

    // void Start()
    public override void Attached()
    {       
        state.SetTransforms(state.CubeTransform, gameObject.transform);
        state.SetTransforms(state.BodyTransform, body.transform);
        state.IsPaused = false;


        weapon = GetComponentInChildren<Weapon>();
    }
    
    // void Update() -- called only on owners computer
    public override void SimulateOwner()
    {     
        if (!state.IsPaused)
        {
            // Movement
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.z = Input.GetAxisRaw("Vertical");

                if (movement != Vector3.zero)
                {
                    transform.position = transform.position + transform.TransformDirection(movement.normalized) * moveSpeed * BoltNetwork.FrameDeltaTime;
                }
            }

            // Aiming
            {
                Vector3 playerPos;
                playerPos = Camera.main.WorldToScreenPoint(transform.position);

                Vector2 v = Input.mousePosition - playerPos;
                float angleRads = Mathf.Atan2(v.y, v.x);
                float angleDeg = angleRads * Mathf.Rad2Deg;

                if (angleDeg < 0)
                    angleDeg += 360;

                angle = angleDeg - 90;
                Vector3 rotation = new Vector3(0, -angle, 0);

                body.transform.localRotation = Quaternion.Euler(rotation);
            }

            // Shooting 
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
        }
        
        // Pause
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                state.IsPaused = !state.IsPaused;
            }
        }
        

    }
}
