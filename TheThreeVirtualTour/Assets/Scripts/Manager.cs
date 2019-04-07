using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public static Manager inst;
    public Transform vrController;
    public Transform teleportArea;
    public Transform teleportIcon;
    public Transform player;
    public InteractableOpen hovering;

    private void Awake() {
        inst = this;
    }

    private void Update() {

        teleportIcon.gameObject.SetActive(false);
        Ray ray = new Ray(vrController.position, vrController.forward); //cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            var interactable = hit.transform.GetComponent<InteractableOpen>();
            if (interactable) {
                Debug.Log(interactable.transform.name);
                if (hovering != interactable) {
                    if (hovering) hovering.ExitHover();
                    interactable.OnHover();
                    hovering = interactable;
                }
                if (Input.GetButtonDown("OculusTouchpadClicked") || Input.GetButtonDown("OculusTriggerClicked")) {
                    interactable.Toggle();
                }
            } else {
                if (hovering) {
                    hovering.ExitHover();
                    hovering = null;
                }
                if (hit.transform.CompareTag("BensTeleportArea")) {
                    teleportIcon.gameObject.SetActive(true);
                    teleportIcon.position = hit.point + Vector3.up * .033f;
                    if (Input.GetButtonDown("OculusTouchpadClicked") || Input.GetButtonDown("OculusTriggerClicked")) {
                        player.position = hit.point + Vector3.up * 1.6f;
                    }
                }
            }
        } else {
            if (hovering) {
                hovering.ExitHover();
                hovering = null;
            }
        }

        

    }



}
