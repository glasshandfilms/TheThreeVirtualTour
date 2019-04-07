using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{

    public Camera cam;
    public Transform vrController;
    public Color startingColor = Color.white;
    public Material material;
    public Texture2D colorWheelTex;
    public Transform cursor;
    public float texSize;
    public Color testColor;
    public Transform origin;
    public Vector2 cursorPosition;
    public bool open;
    float openTimer;

    private void Start() {
        texSize = GetComponent<BoxCollider>().size.x;
        transform.localScale = Vector3.one * (open ? 1f : .2f);
        material.color = startingColor;
    }

    private void Update() {

        //Look at camera
        transform.LookAt(cam.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (open? 1f : .2f), Time.deltaTime * 5f);
        
        if (open) {
            openTimer += Time.deltaTime;
            if (openTimer > 3.5f) {
                open = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = new Ray(vrController.position, vrController.forward); //cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,  out RaycastHit hit)) {
                var picker = hit.transform.GetComponent<ColorPicker>();
                if (picker && picker == this) {

                    openTimer = 0;
                    if (!open) {
                        open = true;
                        return;
                    }

                    cursorPosition = Random.insideUnitCircle;

                    picker.cursor.position = hit.point;

                    cursorPosition = picker.cursor.localPosition * 1.25f;

                    var newCol = colorWheelTex.GetPixel(Mathf.RoundToInt(((cursorPosition.x + 1) / 2) * colorWheelTex.width),
                        Mathf.RoundToInt(((cursorPosition.y + 1) / 2) * colorWheelTex.height));
                    if (newCol.a == 1) testColor = newCol;
                    cursor.transform.localPosition = new Vector3((texSize / 2) * cursorPosition.x, (texSize / 2) * cursorPosition.y);

                    if (material) {
                        material.color = testColor;
                    }
                }
            }
            
            
        }
    }


}
