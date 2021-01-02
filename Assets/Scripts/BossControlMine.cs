using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossControlMine : MonoBehaviour
{
    private Transform playerTarget;
    private BossStateCheckerMine bossStateChecker;
    private NavMeshAgent navAgent;
    private Animator anim;

    private bool finishedAttacking = true;
    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossStateCheckerMine>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (finishedAttacking) {
            getStateControl();
        } else {
            anim.SetInteger("Atk", 0);
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                finishedAttacking = true;
            }
        }
    }

    void getStateControl() {

        //navAgent.SetDestination(playerTarget.position);
        //navAgent.Move(playerTarget.forward * Time.deltaTime);

        if (bossStateChecker.getBossState() == Boss_State2.DEATH) {
            anim.SetBool("Death", true);
            Destroy(gameObject, 3);
        } else {
            if(bossStateChecker.getBossState() == Boss_State2.PAUSE) {
                navAgent.isStopped = false;
                anim.SetBool("Run", true);
                navAgent.SetDestination(playerTarget.position);
                navAgent.Move(playerTarget.forward * Time.deltaTime);

            } else if(bossStateChecker.getBossState() == Boss_State2.ATTACK) {
                anim.SetBool("Run", false);
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);

                if(currentAttackTime >= waitAttackTime) {
                    int atkRange = Random.Range(3, 5);
                    anim.SetInteger("Atk", atkRange);

                    currentAttackTime = 0f;

                    finishedAttacking = false;
                } else {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            } else {
                anim.SetBool("Run", false);
                navAgent.isStopped = true;

            }
        }
    }
}
