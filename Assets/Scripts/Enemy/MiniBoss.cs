using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    GameObject weaponUpgradeChest;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        weaponUpgradeChest = (Resources.Load("WeaponUpgradeChest") as GameObject);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (!player.instantiatedWeapon.GetComponent<Bow>().weaponTier.Equals(WeaponTierEnum.FOUR))
        {
            Instantiate(weaponUpgradeChest, transform.position, Quaternion.identity);
        }
    }
}
