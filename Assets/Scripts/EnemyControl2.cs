using UnityEngine;
using UnityEngine.AI;

public enum EnemyState2 {
    IDLE,
    WALK,
    RUN,
    PAUSE,
    GOBACK,
    ATTACK,
    DEATH
}

public class EnemyControl2 : MonoBehaviour {

    private float attackDistance = 1.8f;
    private float alertAttackDistance = 8f;

    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private float followDistance = 15f;
    private float enemyToPlayerDistance;

    [HideInInspector]
    public EnemyState2 enemyCurrentState = EnemyState2.IDLE;
    public EnemyState2 enemyLastState = EnemyState2.IDLE;

    private Transform playerTarget;
    private Vector3 initialPosition;
    private float moveSpeed = 2f;
    private float walkSpeed = 1f;

    private CharacterController charController;
    private Vector3 whereToMove = Vector3.zero;

    private float currentTime;
    private float waitTime;

    private Animator anim;
    private bool finishedAnimation = true;
    private bool finishedMovement = true;

    private NavMeshAgent navAgent;
    private Vector3 whereToNavigate;

    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        initialPosition = transform.position;
        whereToNavigate = transform.position;

    }

    void Update() {
        if (enemyCurrentState != EnemyState2.DEATH) {
            enemyCurrentState = setEnemyState(enemyCurrentState, enemyLastState, enemyToPlayerDistance);

            if (finishedMovement) {
                getCurrentState(enemyCurrentState);
            } else {
                if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                    finishedMovement = true;
                } else if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1") ||
                     anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2")) {
                    anim.SetInteger("Atk", 0);
                }
            }
        } else {
            anim.SetBool("", true);
            charController.enabled = false;
            navAgent.enabled = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)  {
                Destroy(gameObject, 2f); //destroi depois de 1 segundo
            }
        }
    }

    EnemyState2 setEnemyState(EnemyState2 currentState, EnemyState2 lastState, float enemyToPlayerDistance) {

        enemyToPlayerDistance = Vector3.Distance(transform.position, playerTarget.position);

        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        if(initialDistance > followDistance) {
            lastState = currentState;
            currentState = EnemyState2.GOBACK; 
        }else if(enemyToPlayerDistance <= attackDistance) {
            lastState = currentState;
            currentState = EnemyState2.ATTACK;
        }else if ( (enemyToPlayerDistance >= alertAttackDistance && lastState == EnemyState2.PAUSE) ||
            lastState == EnemyState2.ATTACK) {
            lastState = currentState;
            currentState = EnemyState2.PAUSE;
        }else if (enemyToPlayerDistance <= alertAttackDistance && enemyToPlayerDistance > attackDistance) {
            if (currentState != EnemyState2.GOBACK || lastState == EnemyState2.WALK) {
                lastState = currentState;
                currentState = EnemyState2.PAUSE;
            }
            
        }else if(enemyToPlayerDistance > alertAttackDistance && lastState != EnemyState2.GOBACK &&
            lastState != EnemyState2.PAUSE) {
            lastState = currentState;
            currentState = EnemyState2.WALK;
        }

        return currentState;
    }

    void getCurrentState(EnemyState2 currentState) {
        if(currentState == EnemyState2.RUN || currentState == EnemyState2.PAUSE) {
            if(currentState != EnemyState2.ATTACK) {
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);

                navAgent.SetDestination(targetPosition);
            }else if (currentState == EnemyState2.ATTACK) {
                anim.SetBool("Run", false);
                whereToMove.Set(0, 0, 0);
                navAgent.SetDestination(transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTarget.position-transform.position), 5f*Time.deltaTime);

                if(currentAttackTime >= waitAttackTime) {
                    int attackRange = Random.Range(1, 3); //não inclui o 3 = 1 ou 2

                    anim.SetInteger("Atk", attackRange);

                    finishedAnimation = false;
                    currentAttackTime = 0;
                } else {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            }else if (currentState == EnemyState2.GOBACK) {
                anim.SetBool("RUN", true);
                Vector3 targetPosition = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
                navAgent.SetDestination(targetPosition);
                if(Vector3.Distance(targetPosition, initialPosition) <= 3.5f) {
                    enemyLastState = currentState;
                    currentState = EnemyState2.WALK;
                }
            }else if(currentState == EnemyState2.WALK) {
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);

                if(Vector3.Distance(transform.position, whereToNavigate) <= 2f) {
                    whereToNavigate.x = Random.Range(initialPosition.x - 5f, initialPosition.x + 5f);
                    whereToNavigate.z = Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
                } else {
                    navAgent.SetDestination(whereToNavigate);
                }
            }
        } else {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
            whereToMove.Set(0   , 0, 0);
            navAgent.isStopped = true;
        }
    }
}