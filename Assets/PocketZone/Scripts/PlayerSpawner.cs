using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] HudView _hudView;
    [SerializeField] CameraFollower _camera;

    private void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        var playerTransform = Instantiate(_playerPrefab, transform.position, Quaternion.identity).transform;
        _camera.SetTarget(playerTransform);
        _hudView.SetPlayerTransform(playerTransform);
    }
}
