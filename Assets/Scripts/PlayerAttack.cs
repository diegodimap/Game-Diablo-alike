using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public Image fillWaitImage1;
    public Image fillWaitImage2;
    public Image fillWaitImage3;
    public Image fillWaitImage4;
    public Image fillWaitImage5;
    public Image fillWaitImage6;

    private int[] fadeImages = new int[] {0,0,0,0,0,0};
    private Animator anim;
    private bool canAttack;

    private PlayerMove playerMove;

    private void Awake() {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand")) {
            canAttack = true;
        } else {
            canAttack = false;
        }


        checkToFade();
        checkInput();
    } 

    void checkInput() {

        bool test = false;

        if (anim.GetInteger("Atk") == 0) {
            playerMove.setFinishedMovement(false);

            

            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand")) {
                playerMove.setFinishedMovement(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerMove.setTargetPosition(transform.position);

            test = true;
            //print("apertou 1");
            //print(playerMove.getFinishedMovement());
            //print(canAttack);
            //print(fadeImages[0]);
            //anim.SetInteger("Atk", 1);

            if (playerMove.getFinishedMovement() && fadeImages[0] != 1 && canAttack) {
                fadeImages[0] = 1;
                anim.SetInteger("Atk", 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            playerMove.setTargetPosition(transform.position);

            test = true;

            if (playerMove.getFinishedMovement() && fadeImages[1] != 1 && canAttack) {
                fadeImages[1] = 1;
                anim.SetInteger("Atk", 2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            playerMove.setTargetPosition(transform.position);

            test = true;

            if (playerMove.getFinishedMovement() && fadeImages[2] != 1 && canAttack) {
                fadeImages[2] = 1;
                anim.SetInteger("Atk", 3); //fire tornado é chamado na ANIMAÇÃO através de um evento 
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            playerMove.setTargetPosition(transform.position);

            test = true;

            if (playerMove.getFinishedMovement() && fadeImages[3] != 1 && canAttack) {
                fadeImages[3] = 1;
                anim.SetInteger("Atk", 4);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            playerMove.setTargetPosition(transform.position);

            test = true;

            if (playerMove.getFinishedMovement() && fadeImages[4] != 1 && canAttack) {
                fadeImages[4] = 1;
                anim.SetInteger("Atk", 5);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            playerMove.setTargetPosition(transform.position);

            test = true;

            if (playerMove.getFinishedMovement() && fadeImages[5] != 1 && canAttack) {
                fadeImages[5] = 1;
                anim.SetInteger("Atk", 6);
            }
        }

        //não apertou nenhum dos 6 ataques
        if(test == false) {
            anim.SetInteger("Atk", 0);
        }

        //faz o personagem olhar para onde o mouse está enquanto segura SPACE
        if (Input.GetKey(KeyCode.Space)) {
            Vector3 targetPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast (ray, out hit)) {
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 15f * Time.deltaTime);
        }
    }

    void checkToFade() {
        if (fadeImages[0] == 1) {
            if(fadeAndWait(fillWaitImage1, 1)) {
                fadeImages[0] = 0;
            }
        }

        if (fadeImages[1] == 1) {
            if (fadeAndWait(fillWaitImage2, 0.8f)) {
                fadeImages[1] = 0;
            }
        }

        if (fadeImages[2] == 1) {
            if (fadeAndWait(fillWaitImage3, 0.6f)) {
                fadeImages[2] = 0;
            }
        }

        if (fadeImages[3] == 1) {
            if (fadeAndWait(fillWaitImage4, 0.5f)) {
                fadeImages[3] = 0;
            }
        }

        if (fadeImages[4] == 1) {
            if (fadeAndWait(fillWaitImage5, 0.4f)) {
                fadeImages[4] = 0;
            }
        }

        if (fadeImages[5] == 1) {
            if (fadeAndWait(fillWaitImage6, 0.2f)) {
                fadeImages[5] = 0;
            }
        }
    }

    bool fadeAndWait(Image fadeImg, float fadeTime) {
        bool faded = false;

        if (fadeImg == null) {
            return false;
        } else {
            if (!fadeImg.gameObject.activeInHierarchy) {
                fadeImg.gameObject.SetActive(true);
                fadeImg.fillAmount = 1f;
            }
        }

        fadeImg.fillAmount -= fadeTime*Time.deltaTime;

        if(fadeImg.fillAmount <= 0) {
            fadeImg.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }
}
