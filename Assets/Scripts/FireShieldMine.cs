using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShieldMine : MonoBehaviour
{

    private PlayerHealthMine playerHealthMine;

    void Awake() {
        playerHealthMine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>();
    }

    void OnEnable() {
        playerHealthMine.setShielded(true);
        print("com escudo");
    }

    void OnDisable() {
        playerHealthMine.setShielded(false);
        print("sem escudo");
    }
}
