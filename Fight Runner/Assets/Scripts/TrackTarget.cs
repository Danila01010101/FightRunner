using UnityEngine;

public class TrackTarget : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _newCharacterPositions;
    private int indexOfPosition = 0;
    [SerializeField]
    private Vector3 _cameraForPlayerPos = new Vector3(-9.8f, 12, -5f);

    private Vector3 _cameraForRockPos = new Vector3(-2f, 8, -13);

    private GameObject _targetToFollow;

    private void Start()
    {
        _newCharacterPositions[1] = _targetToFollow.transform.position;
    }

    private void LateUpdate()
    {
        if (_targetToFollow.CompareTag("Player"))
        {
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, _targetToFollow.transform.position.z) + _cameraForPlayerPos, 100);
            if (_targetToFollow.GetComponent<MainCharacterController>()._isMovesOnXAxis)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_newCharacterPositions[indexOfPosition].x, _targetToFollow.transform.position.y, _targetToFollow.transform.position.z) + _cameraForPlayerPos, 3);
                Camera.main.transform.LookAt(new Vector3(_newCharacterPositions[indexOfPosition].x, _newCharacterPositions[indexOfPosition].y, _targetToFollow.transform.position.z + 10));
                //transform.position = new Vector3(_newCharacterPositions[indexOfPosition].x, _targetToFollow.transform.position.y, _targetToFollow.transform.position.z) + _cameraForPlayerPos;
            }
            else
            {
                //transform.position = new Vector3(_targetToFollow.transform.position.x, _targetToFollow.transform.position.y, _newCharacterPositions[indexOfPosition].z) + _cameraForPlayerPos;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetToFollow.transform.position.x, _targetToFollow.transform.position.y, _newCharacterPositions[indexOfPosition].z) + _cameraForPlayerPos, 3);
                Camera.main.transform.LookAt(new Vector3(_targetToFollow.transform.position.x + 10, _newCharacterPositions[indexOfPosition].y, _newCharacterPositions[indexOfPosition].z));
            }

        }
        else
        {
            transform.position = new Vector3(_targetToFollow.transform.position.x, _targetToFollow.transform.position.y, _targetToFollow.transform.position.z) + _cameraForRockPos;
            Camera.main.transform.LookAt(_targetToFollow.transform);
        }
    }

    public void ChangeTargetToTrack(GameObject target)
    {
        _targetToFollow = target;
    }

    public void ChangeCameraPosition(Vector3 position)
    {
         indexOfPosition += 1;
         _cameraForPlayerPos = position;
    }

}
