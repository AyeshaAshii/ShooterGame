using UnityEngine;

public class MiniMap : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] float followSpeed = 10f;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target .position , followSpeed*Time.deltaTime);
    }
}
