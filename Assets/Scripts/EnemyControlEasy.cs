using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlEasy : MonoBehaviour
{

    public Transform[] walkPoints;
    private int walkIndex = 0;
    private Transform playerTarget;
    private Animator anim;
    private NavMeshAgent navAgent;
    private float walkDistance = 8f;
    private float attackDistance = 3f;
    private float currentAttackTime = 0f;
    private float waitAttackTime = 1f;
    private Vector3 nextDestination;
    public bool isAlive = true;


    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        // navAgent.SetDestination(playerTarget.position);
        // navAgent.Move(playerTarget.forward * Time.deltaTime);

        if (isAlive) {
            float distance = Vector3.Distance(transform.position, playerTarget.position);
            if (distance > walkDistance) {
                if (navAgent.remainingDistance <= 1.5f) {
                    navAgent.isStopped = false;
                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                    anim.SetInteger("Atk", 0);

                    nextDestination = walkPoints[walkIndex].position;
                    navAgent.SetDestination(playerTarget.position);
                    navAgent.Move(playerTarget.forward * Time.deltaTime);

                    //walkIndex++;

                    if (walkIndex == walkPoints.Length - 1) {
                        walkIndex = 0;
                    } else {
                        walkIndex++;
                    }

                }
            } else {
                if (distance > attackDistance) {
                    navAgent.isStopped = false;
                    anim.SetBool("Run", true);
                    anim.SetBool("Walk", false);
                    anim.SetInteger("Atk", 0);

                    navAgent.SetDestination(playerTarget.position);
                    navAgent.Move(playerTarget.forward * Time.deltaTime);
                } else {
                    navAgent.isStopped = true;
                    anim.SetBool("Run", false);
                    anim.SetBool("Walk", false);

                    Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);

                    if (currentAttackTime >= waitAttackTime) {
                        int atkRange = Random.Range(1, 3);
                        anim.SetInteger("Atk", atkRange);
                        currentAttackTime = 0f;
                    } else {
                        anim.SetInteger("Atk", 0);
                        currentAttackTime += Time.deltaTime;
                    }

                }
            }
        }
    }
}
