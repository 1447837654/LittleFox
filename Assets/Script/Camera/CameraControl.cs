using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // Íæ¼Òtransform
    private Vector3 playerPosition; // Íæ¼ÒÎ»ÖÃ

    private void Start()
    {
        playerPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition.x = player.position.x;
        playerPosition.y = 0f;
        playerPosition.z = -10f;

        this.transform.position = playerPosition;
    }
}
