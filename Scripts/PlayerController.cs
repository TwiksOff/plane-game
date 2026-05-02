using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    new Camera camera;
    [SerializeField]
    Plane plane;
    [SerializeField]
    PlaneHUD planeHUD;

    Vector3 controlInput;
    PlaneCamera planeCamera;
    AIController aiController;

    void Start() {
        planeCamera = GetComponent<PlaneCamera>();
        planeCamera.ToggleCameraAngle();
        SetPlane(plane);    //SetPlane if var is set in inspector
    }

    void SetPlane(Plane plane) {
        this.plane = plane;
        aiController = plane.GetComponent<AIController>();

        if (planeHUD != null) {
            planeHUD.SetPlane(plane);
            planeHUD.SetCamera(camera);
        }

        planeCamera.SetPlane(plane);
    }
    public void OnToggleHelp(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (context.phase == InputActionPhase.Performed) {
            planeHUD.ToggleHelpDialogs();
        }
    }

    public void SetThrottleInput(InputAction.CallbackContext context) {
        if (plane == null) return;
        if (aiController.enabled) return;

        

        float raw = context.ReadValue<float>();


        float converted = ((-raw + 1f) / 2f); // on inverse la valeur du slider (entre -1 et 1 à 1 et -1 ) puis on passe à une valeur entre 0 et 1 


        plane.SetThrottleInput(converted);
    }


    public void OnRollPitchInput(InputAction.CallbackContext context) {
        if (plane == null) return;

        var input = context.ReadValue<Vector2>();
        controlInput = new Vector3(input.y, controlInput.y, -input.x);
    }

    public void OnYawInput(InputAction.CallbackContext context) {
        if (plane == null) return;

        var input = context.ReadValue<float>();
        controlInput = new Vector3(controlInput.x, input, controlInput.z);
    }

    public void OnCameraInput(InputAction.CallbackContext context) {
        if (plane == null) return;

        var input = context.ReadValue<Vector2>();
        planeCamera.SetInput(input);
    }

    public void OnFlapsInput(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (context.phase == InputActionPhase.Performed) {
            plane.ToggleFlaps();
        }
    }

    public void OnFireMissile(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (context.phase == InputActionPhase.Performed) {
            plane.TryFireMissile();
        }
    }

    public void OnFireCannon(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (context.phase == InputActionPhase.Started) {
        
            plane.SetCannonInput(true);
        } else if (context.phase == InputActionPhase.Canceled) {
            plane.SetCannonInput(false);
        }
    }

    public void OnToggleAI(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (aiController != null) {
            aiController.enabled = !aiController.enabled;
        }
    }

    public void OnChangeCameraAngle(InputAction.CallbackContext context) {
        if (plane == null) return;

        if (context.phase == InputActionPhase.Performed) {
            planeCamera.ToggleCameraAngle();
        }
    }

    void Update() {
        if (plane == null) return;
        if (aiController.enabled) return;

        plane.SetControlInput(controlInput);
    }
}
