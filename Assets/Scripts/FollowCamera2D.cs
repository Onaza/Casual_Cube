using UnityEngine;

public class FollowCamera2D : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        cameraTransform.position = new Vector3(target.position.x, target.position.y, -12.0f);
    }
}
