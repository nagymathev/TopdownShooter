using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    //public GameObject bulletPrefab;
	//public GameObject muzzleFlashPrefab;
	//public GameObject reloadPrefab;

	[System.Serializable]
	public struct WeaponDef
	{
        public string name;

		public GameObject bulletPrefab;
		public GameObject muzzleFlashPrefab;
		public GameObject reloadPrefab;

		public float bulletSpeed;// = 20f;

		public float spreadAngle;// = 0.0f;
		public int numBulletsFiredAtOnce;// = 1;

		public bool autoRepeat;

		public float repeatTime;// = 0.05f;

		public float reloadTime;// = 1.0f;

		public int magSize;// = 7;
		public int numMagazines;// = -1;
	}
	public WeaponDef defaultWeapon;
	public WeaponDef currentWeapon;

	//public float bulletSpeed = 20f;

	//public float spreadAngle = 0.0f;
	//public int numBulletsFiredAtOnce = 1;

	//public bool autoRepeat;

	//public float repeatTime = 0.05f;
	public float repeatTimer;

	//public float reloadTime = 1.0f;
	public float reloadTimer;

	//public int magSize = 7;
	public int numBullets = 10;
	//public int numMagazines = -1;

	private void Start()
	{
		SetWeapon(defaultWeapon);
	}

	public void SetWeapon(WeaponDef def)
    {
		currentWeapon = def;
		numBullets = currentWeapon.magSize;

		if (currentWeapon.reloadPrefab)
			Instantiate(currentWeapon.reloadPrefab, firePoint.position, firePoint.rotation);
	}

	// Update is called once per frame
	void Update()
    {
		if (reloadTimer > 0)
		{//reloading
			reloadTimer -= Time.deltaTime;
			return;
		}

		if (repeatTimer > 0)
		{
			repeatTimer -= Time.deltaTime;
			return;
		}

		if (currentWeapon.autoRepeat)
		{
			if (Input.GetButton("Fire1"))
			{
				Shoot();
			}
		} else
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Shoot();
			}
		}
    }

    void Shoot()
    {
		//if (!currentWeapon) return;

		if (numBullets == 0)
		{//empty
			if (currentWeapon.numMagazines > 0)
			{//start reload
                currentWeapon.numMagazines--;
				numBullets = currentWeapon.magSize;
				reloadTimer = currentWeapon.reloadTime;
				if (currentWeapon.reloadPrefab)
					Instantiate(currentWeapon.reloadPrefab, firePoint.position, firePoint.rotation);
				return;
			} else
			if (currentWeapon.numMagazines < 0)
			{//infinite mags (default pistol)
				reloadTimer = currentWeapon.reloadTime;
				numBullets = currentWeapon.magSize;
				if (currentWeapon.reloadPrefab)
					Instantiate(currentWeapon.reloadPrefab, firePoint.position, firePoint.rotation);
				return;
			} else
			{//reset to default pistol
			 //ToDo: how exactly?
				SetWeapon(defaultWeapon);
			}
		}

		if (currentWeapon.bulletPrefab)
		{
			for (int i = 0; i < currentWeapon.numBulletsFiredAtOnce; i++)
			{
				GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
				Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
				//rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
				Vector3 dir = firePoint.up;
				if (currentWeapon.spreadAngle > 0)
					dir = Quaternion.AngleAxis(Random.Range(-currentWeapon.spreadAngle, currentWeapon.spreadAngle), firePoint.forward) * dir;
				rb.velocity = dir  * currentWeapon.bulletSpeed;
			}
		}
		if (currentWeapon.muzzleFlashPrefab)
			Instantiate(currentWeapon.muzzleFlashPrefab, firePoint.position, firePoint.rotation);

		repeatTimer = currentWeapon.repeatTime;

		if (numBullets < 0)
		{//infinite ammo
		} else
		{
			numBullets--;
		}
	}
}
