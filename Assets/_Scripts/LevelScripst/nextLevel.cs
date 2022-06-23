using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    public static Action nextRoomEnter;
    public static Action winLevel;

    [SerializeField] private Transform _playerStartPosition;

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Tags.player))
        {
            nextRoomEnter?.Invoke();
            other.gameObject.transform.position = _playerStartPosition.position;

            if (LevelController.instance.carrentRoomIndex == LevelController.instance.roomList.Count - 1)
            {
                winLevel?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}
