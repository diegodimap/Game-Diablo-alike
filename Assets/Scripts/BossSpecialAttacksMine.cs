using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttacksMine : MonoBehaviour
{

    public GameObject bossFire;
    public GameObject bossMagic;

    private Transform playerTarget;

    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void BossFireTornado() {
        Instantiate(bossFire, playerTarget.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        playerTarget.GetComponent<PlayerHealthMine>().takeDamage(50);
        print("player leveou 50 de dano do boss");
    }

    void BossSpell() {
        Vector3 temp = playerTarget.position;
        temp.y = 1.5f;
        Instantiate(bossMagic, temp, Quaternion.identity);
        playerTarget.GetComponent<PlayerHealthMine>().takeDamage(50);
        print("player leveou 50 de dano do boss");
    }
}
