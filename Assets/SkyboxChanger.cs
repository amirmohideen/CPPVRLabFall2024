using UnityEngine;
using System.Collections.Generic;

public class ControllerInputs : MonoBehaviour
{
    // Reference to the Input Action Asset, which contains the input bindings for the VR controller
    private VRInputActions controls;

    // List of skybox materials to cycle through
    [SerializeField] private List<Material> skyboxMaterialList = new List<Material>();

    // Current index of the skybox material in the list
    private int index = 0;

    private void Awake()
    {
        // Initialize the input action asset
        controls = new VRInputActions();

        // Assign callbacks for the right controller's primary button
        controls.RightController.RightPrimaryButton.performed += ctx => OnRightPrimaryButtonPressed();
        controls.RightController.RightPrimaryButton.canceled += ctx => OnRightPrimaryButtonReleased();

        // Assign callbacks for the right controller's secondary button
        controls.RightController.RightSecondaryButton.performed += ctx => OnRightSecondaryButtonPressed();
        controls.RightController.RightSecondaryButton.canceled += ctx => OnRightSecondaryButtonReleased();
    }

    private void OnEnable()
    {
        // Enable the input action map for the right controller
        controls.RightController.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action map for the right controller to avoid unnecessary processing when this script is disabled
        controls.RightController.Disable();
    }

    // Callback for when the primary button on the right controller is pressed
    private void OnRightPrimaryButtonPressed() => increaseIndex();

    // Callback for when the primary button on the right controller is released
    private void OnRightPrimaryButtonReleased() => Debug.Log("Right Primary Button Released");

    // Callback for when the secondary button on the right controller is pressed
    private void OnRightSecondaryButtonPressed() => decreaseIndex();

    // Callback for when the secondary button on the right controller is released
    private void OnRightSecondaryButtonReleased() => Debug.Log("Right Secondary Button Pressed");

    // Increments the index to point to the next skybox material and updates the skybox
    void increaseIndex()
    {
        index++; // Move to the next skybox material in the list
        replaceSkyboxSettingswithSkyboxMaterial(); // Apply the new skybox material
    }

    // Decrements the index to point to the previous skybox material and updates the skybox
    void decreaseIndex()
    {
        index--; // Move to the previous skybox material in the list
        replaceSkyboxSettingswithSkyboxMaterial(); // Apply the new skybox material
    }

    // Updates the RenderSettings.skybox with the current material from the list
    void replaceSkyboxSettingswithSkyboxMaterial()
    {
        // Ensure the index loops back to the start if it exceeds the list size
        if (index >= skyboxMaterialList.Count)
            index = 0;

        // Ensure the index loops back to the end if it goes below zero
        else if (index < 0)
            index = skyboxMaterialList.Count - 1;

        // Apply the skybox material at the current index
        RenderSettings.skybox = skyboxMaterialList[index];
    }
}
