using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMine : MonoBehaviour
{

    public float damageAmount = 10f;
    private Transform playerTarget;
    private Animator anim;
    private bool finishedAttack = true;
    private float damageDistance = 4f;
    private PlayerHealthMine playerHealthMine;

    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        playerHealthMine = playerTarget.GetComponent<PlayerHealthMine>();
    }
    void Update()
    {
        if (finishedAttack) {
            dealDamage(checkIfIsAttacking());
        } else {
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                finishedAttack = true;
            }
        } 
    }

    bool checkIfIsAttacking() {
        bool isAttacking = false;

        if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Atk1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Atk2")) {
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) {
                isAttacking = true;
                finishedAttack = false;
            }
        }

        return isAttacking;
    }

    void dealDamage(bool isAttacking) {
        if (isAttacking) {
            if(Vector3.Distance(transform.position, playerTarget.position) <= damageDistance) {
                playerHealthMine.takeDamage(damageAmount);
            }
        }
    }
}
