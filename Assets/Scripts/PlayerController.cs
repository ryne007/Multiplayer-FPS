using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] //shows speed variable in Inspector even if it is private data type
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    

    private PlayerMotor motor;

    //Every local var starts with _

    void Start()
    {
        motor = GetComponent<PlayerMotor>(); //just like Student s = new Student(), we are initializing motor object
    }

    void Update()
    {
        //calculate movement velocity as a 3D ector
        //GetAxisRaw gives comlete control over axis so we can perform the smoothness easily 
        //Horizontal and Vertical go between -1 and 1, and is multiplied with tranform.directionName to give the vector a value
        float _xMov = Input.GetAxisRaw("Horizontal"); //Edit -> Project Settings -> Input -> Axes. The axes names are Horizontal and vertical
        float _zMov = Input.GetAxisRaw("Vertical");

        //transform.right is local direction to right and takes into consideration our current rotation i.e which way we are facing, and not relative to the world
        //
        Vector3 _movHorizontal = transform.right * _xMov; //(0,0,0) by default i.e when not moving, (1,0,0) when moving right, (-1,0,0) when moving left
        Vector3 _movVertical = transform.forward * _zMov; //(0,0,0) by default i.e when not moving, (0,0,1) when moving forward, (0,0,-1) when moving backward

        //Using above code to get local Velocity vector i,e FINAL MOVEMENT VECTOR
        //normalized means that the length of vector shouldn't atter and the total combined length should be 1
        //That means we won't get varying speed and will always get a Vector with length of 1
        //which means that combined value only acts as a direction, which is multiplied by speed
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Applying Movement
        motor.Move(_velocity); // passing velocity by calling Motor func in PlayerMotor using motor object

        //Calculate rotation as a 3D vector but only for turning around
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply turning rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector 
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot,0f, 0f) * lookSensitivity;

        //Apply camera turning rotation
        motor.RotateCamera(_cameraRotation);

        //adding jump functionality
        /*Vector3 _jump = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _jump = Vector3.up * jumpForce; //vector3.up has default value of (0,1,0)
        }
        motor.ApplyJump(_jump);*/
    }

}
