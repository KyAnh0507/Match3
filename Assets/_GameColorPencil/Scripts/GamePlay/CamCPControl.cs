using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCPControl : Singleton<CamCPControl>
{
    public Camera cam;
    public Vector3 posLevel = new Vector3(-0.5f, 3, -10);

    public void SetCameraSize()
    {
        cam.orthographicSize = Mathf.Clamp(((float)LevelManagerColorPencil.Ins.MaxNPencil()/3 + 3) / Screen.width * Screen.height, 7.5f, 30);
        float sizeRatio = cam.orthographicSize / 7.5f;
        cam.transform.position = new Vector3(
            posLevel.x,
            posLevel.y * sizeRatio,
            posLevel.z
        );
    }
}
