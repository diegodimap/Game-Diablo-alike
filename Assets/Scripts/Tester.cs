using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            animator.SetBool("walk", true);
        } else {
            animator.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.S)) {
            animator.SetFloat("run", 1);
        } else {
            animator.SetFloat("run", 0);
        }
    }
}
