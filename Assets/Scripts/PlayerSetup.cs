using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    //All components in Unity are derived from Behaviour class
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer) //calling various methods if we are not the local player
        {

            DisableComponents();
            AssignRemoteLayer();

        }
        else 
        {
            //disabing the scene camera for local player once we enter the game 
            sceneCamera = Camera.main; 
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        RegisterPlayer();
	}

    void RegisterPlayer()
    {
        string _id = "Player " + GetComponent<NetworkIdentity>().netId; //takes the unique identity of the player from NetworkIdentity script
        transform.name = _id;
    }

    void AssignRemoteLayer() //this function changes the tag for remote players who are connected to the host
    {
        //layer name is stored under gameObject component
        //gameObject.Layer wants an int value, and LayerMask.NameToLayer converts string name of the layer to an int
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents() //this function disables various scripts and components so that we control only the local player and not remote players
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable() //re-enabling camera once we disconnect to display the HUD
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
