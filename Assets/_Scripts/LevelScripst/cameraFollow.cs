using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _palyerTarget;
    [SerializeField] private float _centerCalibration;

    // Start is called before the first frame update

    private void OnEnable()
    {
        NextLevel.nextRoomEnter += StabView;
        
    }


    private void OnDisable()
    {
        NextLevel.nextRoomEnter -= StabView;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        
        if (_palyerTarget != null)
        {
            var playerPosition = new Vector3(_palyerTarget.transform.position.x, transform.position.y, _palyerTarget.transform.position.z + _centerCalibration);
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * 2);
        }
           
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            _palyerTarget = other.gameObject;
    }


    private void StabView()
    {
        transform.position = new Vector3(0, 35, -80);
    }
}
