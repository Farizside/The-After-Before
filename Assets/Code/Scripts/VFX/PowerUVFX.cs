using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUVFX : MonoBehaviour
{
    public GameObject VFXPowerUPPrefab;
    public Transform VFXPowerUPPosition;

    private GameObject spawnedVFX;

    public void PlayPUVFX()
    {
        if (VFXPowerUPPrefab != null && VFXPowerUPPosition != null)
        {
            spawnedVFX = Instantiate(VFXPowerUPPrefab, VFXPowerUPPosition.position, VFXPowerUPPosition.rotation);
            spawnedVFX.transform.parent = VFXPowerUPPosition;
            Debug.Log("VFX spawned at: " + VFXPowerUPPosition.position);
        }
        else
        {
            Debug.LogError("VFX Prefab atau VFX Position tidak diatur dengan using System.Collections;\r\nusing System.Collections.Generic;\r\nusing UnityEngine;\r\n\r\npublic class PowerUVFX : MonoBehaviour\r\n{\r\n    public GameObject VFXPowerUPPrefab;\r\n    public Transform VFXPowerUPPosition;\r\n\r\n    private GameObject spawnedVFX;\r\n\r\n    public void PlayVFX()\r\n    {\r\n        if (VFXPowerUPPrefab != null && VFXPowerUPPosition != null)\r\n        {\r\n            spawnedVFX = Instantiate(VFXPowerUPPrefab, VFXPowerUPPosition.position, VFXPowerUPPosition.rotation);\r\n            spawnedVFX.transform.parent = VFXPowerUPPosition;\r\n            Debug.Log(\"VFX spawned at: \" + VFXPowerUPPosition.position);\r\n        }\r\n        else\r\n        {\r\n            Debug.LogError(\"VFX Prefab atau VFX Position tidak diatur dengan benar.\");\r\n        }\r\n    }\r\n\r\n    public void StopVFX()\r\n    {\r\n        if (spawnedVFX != null)\r\n        {\r\n            Destroy(spawnedVFX);\r\n            spawnedVFX = null;\r\n            Debug.Log(\"VFX stopped.\");\r\n        }\r\n        else\r\n        {\r\n            Debug.LogWarning(\"No VFX currently playing to stop.\");\r\n        }\r\n    }\r\n}\r\nbenar.");
        }
    }

    public void StopPUVFX()
    {
        if (spawnedVFX != null)
        {
            Destroy(spawnedVFX);
            spawnedVFX = null;
            Debug.Log("VFX stopped.");
        }
        else
        {
            Debug.LogWarning("No VFX currently playing to stop.");
        }
    }
}
