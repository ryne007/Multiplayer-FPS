using UnityEngine;

[System.Serializable] //this makes the PlayerWeapon class serializable which allows us to change the defined values in the inspector
public class PlayerWeapon{

    public string name = "AK-47";
    public float damage = 20f;
    public float range = 200f;

}
