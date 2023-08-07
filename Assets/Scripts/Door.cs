using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    TilemapCollider2D tilemapCollider;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerDoor");
        if (tilemapCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) { Debug.Log("DoorTrigger"); }
    }
}
