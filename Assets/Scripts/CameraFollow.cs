using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float followHeight = 8;
    public float followDistance = 6;

    private Transform player;

    private float targetHeight;
    private float currentHeight;
    private float currentRotation;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnEnable() {

    }

    void Start() {

    }

    void Update() {
        targetHeight = player.position.y + followHeight;
        currentRotation = transform.eulerAngles.y;
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, 0.9f * Time.deltaTime);
        Quaternion euler = Quaternion.Euler(0, currentRotation, 0);
        Vector3 targetPosition = player.position - (euler * Vector3.forward) * followDistance;

        targetPosition.y = currentHeight;
        transform.position = targetPosition;
        transform.LookAt(player);
    }
}
