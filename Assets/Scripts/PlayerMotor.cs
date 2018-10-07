using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

// We are using PlayerController to change velocity vector in this script, and then use that vector in a FixedUpdate loop to move the Player
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero; //initializing velocity vector to 0
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;




    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets a movement vector
    public void Move (Vector3 _velocity) 
    {
        velocity = _velocity; //input the values passed to the velocity vector of this script
    }

    //Gets a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation; //input the values passed to the rotation vector of this script
    }

    //Gets camera rotation vector
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation; //input the values passed to the rotation vector of this script
    }

  

    void PerformMovement()
    {
        if (velocity != Vector3.zero) //if we WANT to move
        {
            //MovePosition takes in the position we want to move to
            //It does all the physics check and collision check, easier to use than AddForce method
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime); //moves the player by ckecking the current position and adding velocity vector to it
            
        }
        
    }

    void PerformRotation()
    {
        //Euler is what we know as x,y and z
        //Unity understands Quaternion and transform.rotation is in that form, but Quaternion.Euler converts it into x,y and z
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
     
   

}
