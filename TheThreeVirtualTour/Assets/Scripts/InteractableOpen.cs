using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOpen : MonoBehaviour
{

    public bool open;
    public float openSpeed = 1f;
    public Vector3 openAngleOffset;
    public Material hoverMaterial;
    Vector3 startingAngle;
    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    List<Material> originalMaterials = new List<Material>();

    private void Start() {
        startingAngle = transform.localEulerAngles;
        foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>()) {
            meshRenderers.Add(mr);
            originalMaterials.Add(mr.material);
        }
    }

    private void Update() {
        
        if (open) {
            transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, startingAngle + openAngleOffset, Time.deltaTime * openSpeed);
        } else {
            transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, startingAngle, Time.deltaTime * openSpeed);
        }

    }

    private void OnMouseDown() {
        open = !open;
    }

    private void OnMouseEnter() {
        if (hoverMaterial) {
            foreach (MeshRenderer mr in meshRenderers) {
                mr.material = hoverMaterial;
            }
        }
    }

    private void OnMouseExit() {
        if (hoverMaterial) {
            int i = 0;
            foreach (MeshRenderer mr in meshRenderers) {
                mr.material = originalMaterials[i];
                i++;
            }
        }
    }


}
