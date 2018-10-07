using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    //This script is used to Raycast the shoot mechanism (meaning defining the ray/path of the bullets)
    private const string Player_tag = "Player";

    public PlayerWeapon weapon;
    
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask; //This allows us to control what we hit. For eg, we can define using Layermask to hit players and objects, but not ourselves and invisible triggerboxes
    
    void Start()
    {
        //Raycasting will be from centre of the camera hence existence of the camera is checked
        if (cam == null)
        {
            Debug.Log("Player Shoot: No camera referened");
            this.enabled = false;
        }
    }

    void Update()
    {
        //Fire1 is located under Edit->Project Settings->Input..from here we reference that input button is mouse0
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client] //this makes Shoot() to be called only on client and not on server i.e, it makes Shoot() a local method
    //Shoot function is where Raycast is defined
    void Shoot()
    {
        //Raycast takes in a lot of arguments. Here we define where the ray starts, its direction, variable where this info is stored, Layermask object and weapon range
        //we wil get info about what we hit by using the _hit variable
        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == Player_tag) //checking if the object that is hit has the tag name Player (which is stored in the string var declared above)
            {
                CmdPlayerIsShot(_hit.collider.name); //passing the name of the player to the server method CmdPlayerIsShot
            }
        }
    }

    [Command]//methods defined under this are called only on server
    //defining this method under server make it possible to keep record of the hits made by other player that spawns (when we build the game)
    void CmdPlayerIsShot(string _playerId)
    {
        Debug.Log(_playerId + " has been shot");
    }

}
