using UnityEngine;

public class MinimapIconFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float height = 50f;

    private void LateUpdate()
    {
        if (!target) return;
        
        transform.position = new Vector3(target.position.x, height, target.position.z);
        
        transform.rotation = Quaternion.Euler(90f, target.rotation.eulerAngles.y, 0f);
    }
}