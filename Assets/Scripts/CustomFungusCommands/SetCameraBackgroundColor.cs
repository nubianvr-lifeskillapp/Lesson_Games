using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


[CommandInfo("Camera","Set Camera Color","Set the background Color of the camera")]

public class SetCameraBackgroundColor : Command
{

    public Color cameraColor;
    public Camera camera;

    public override void OnEnter()
    {
        camera.backgroundColor = cameraColor;
        Continue();
    }

    public override string GetSummary()
    {
        return cameraColor.ToString();
    }

    public override Color GetButtonColor()
    {
        return new Color32(216, 228, 170, 255);
    }

}
