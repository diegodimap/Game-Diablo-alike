using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{

    public Texture2D cursorTexture;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero; 

    public GameObject mousePoint;
    private GameObject instantiatedMouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider is TerrainCollider) {
                    Vector3 temp = hit.point;
                    temp.y = 0.5f;
                    if(instantiatedMouse == null) {
                        instantiatedMouse = Instantiate(mousePoint)  as GameObject;
                        instantiatedMouse.transform.position = temp;
                    } else {
                        Destroy(instantiatedMouse);
                    }
                }
            }
        }
    }
}
