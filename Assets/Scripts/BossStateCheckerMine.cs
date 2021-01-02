using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_State2 {
    NONE,
    IDLE,
    PAUSE,
    ATTACK,
    DEATH
}

public class BossStateCheckerMine : MonoBehaviour
{

    private Transform playerTarget;
    private Boss_State2 bossState = Boss_State2.NONE;
    private float distanceToTarget;

    private EnemyHealthMine bossHealth;

    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GameObject.Find("CapsuleBoss").GetComponent<EnemyHealthMine>();

    }

    // Update is called once per frame
    void Update()
    {
        setState();
    }

    void setState() {
        distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

        if(bossState != Boss_State2.DEATH) {
            if(distanceToTarget > 3 && distanceToTarget <= 15) {
                bossState = Boss_State2.PAUSE;
            }else if(distanceToTarget > 15) {
                bossState = Boss_State2.IDLE;
            }else if(distanceToTarget <= 3) {
                bossState = Boss_State2.ATTACK;
            } else {
                bossState = Boss_State2.NONE;
            }

            if(bossHealth.health <= 0){
                bossState = Boss_State2.DEATH;
            }
        }
    }

    public Boss_State2 getBossState() {
        return this.bossState;
    }

    public void setBossState(Boss_State2 state) {
        this.bossState = state;
    }


}
