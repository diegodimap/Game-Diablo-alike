using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator animator;
    private CharacterController charController;
    private CollisionFlags collisonFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool canMove;
    private bool finishedMovement = true;

    private Vector3 target_pos = Vector3.zero;
    private Vector3 player_move = Vector3.zero;

    private float playerToPointDistance;

    private float gravity = 9.8f;
    private float height;

    public bool isAlive = true; 

    private void Awake() {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    void movePlayer() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider is TerrainCollider) {
                    playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                    if (playerToPointDistance >= 1) {
                        canMove = true;
                        target_pos = hit.point;
                    }
                }
            }
        }

        if (canMove) {
            animator.SetFloat("Walk", 1.0f);
            Vector3 targetTemp = new Vector3(target_pos.x, transform.position.y, target_pos.z); //o y não muda
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTemp - transform.position), 15*Time.deltaTime);

            player_move = transform.forward * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, target_pos) <= 0.5f) {
                canMove = false;
            }
        } else {
            player_move.Set(0,0,0);
            animator.SetFloat("Walk", 0);
        }
        //checkMovementFinished();
    }

    bool isGrounded() {
        if (collisonFlags == CollisionFlags.CollidedBelow) {
            return true;
        } else {
            return false;
        }
    }

    void calculateHeight() {
        if (isGrounded()) {
            height = 0;
        } else {
            height -= gravity * Time.deltaTime;
        }
    }

    void checkMovementFinished() {
        if (!finishedMovement) {
            if (!animator.IsInTransition(0) && 
                !animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) {
                finishedMovement = true;
            }
        } else {
            movePlayer();
            player_move.y = height * Time.deltaTime;
            collisonFlags = charController.Move(player_move);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float health = gameObject.GetComponent<PlayerHealthMine>().health;
        if (health > 0) { 
            movePlayer();
            charController.Move(player_move);
        }
    }

    public bool getFinishedMovement() {
        return finishedMovement;
    }

    public void setFinishedMovement(bool f) {
        finishedMovement = f;
    }

    public Vector3 getTargetPosition() {
        return target_pos;
    }

    public void setTargetPosition(Vector3 t) {
        target_pos = t;
    }

}
