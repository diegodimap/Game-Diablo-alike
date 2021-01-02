using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{

    public GameObject groundImpact, kick, fireTornado, fireShield;
    public GameObject groundImpactPrefab, kickPrefab, fireTornadoPrefab, fireShieldPrefab, healPrefab, thunderPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GroundImpact() {
        Instantiate(groundImpactPrefab, groundImpact.transform.position, Quaternion.identity);
    }

    void Kick() {
        Instantiate(kickPrefab, kick.transform.position, Quaternion.identity);
    }

    void FireTornado() {
        Instantiate(fireShieldPrefab, fireTornado.transform.position, Quaternion.identity);
    }

    void FireShield() {
        GameObject fireObj = Instantiate(fireShieldPrefab, fireShield.transform.position, Quaternion.identity) as GameObject;
        fireObj.transform.SetParent(transform);
    }

    void Heal() {
        Vector3 temp = transform.position;
        temp.y += 2f;
        GameObject heal = Instantiate(healPrefab, temp, Quaternion.identity) as GameObject;
        heal.transform.SetParent(transform);
    }

    void ThunderAttack() { 
        for (int i =0; i < 8; i++) {
            Vector3 pos = Vector3.zero;

            if (i == 0) {
                pos = new Vector3(transform.position.x - 4f, transform.position.y + 2, transform.position.z);
            } else if (i == 1) {
                pos = new Vector3(transform.position.x + 4f, transform.position.y + 2, transform.position.z);
            } else if (i == 2) {
                pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z -4);
            } else if (i == 3) {
                pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z +4);
            } else if (i == 4) {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2, transform.position.z + 2.5f);
            } else if (i == 5) {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2, transform.position.z + 2.5f);
            } else if (i == 6) {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2, transform.position.z - 2.5f);
            } else if (i == 7) {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2, transform.position.z + 2.5f);
            }

            Instantiate(groundImpactPrefab, pos, Quaternion.identity);
        }
        
    }
}
