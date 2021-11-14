using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text weaponName;
    public Text ammoText;

    public Shooting shootingScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponName.text = shootingScript.currentWeapon.name;
        ammoText.text = shootingScript.numBullets.ToString() + "/" + shootingScript.currentWeapon.numMagazines.ToString();
    }
}
