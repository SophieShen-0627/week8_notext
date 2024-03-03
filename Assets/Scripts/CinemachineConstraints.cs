using UnityEngine;
using Cinemachine;

public class CinemachineConstraints : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public float minOrthoSize = 5f; // 设置最小正交大小
    public float minYPosition = 0f; // 设置最小Y位置

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // 检查并调整正交大小
        if (virtualCamera.m_Lens.OrthographicSize < minOrthoSize)
        {
            virtualCamera.m_Lens.OrthographicSize = minOrthoSize;
        }

        // 检查并调整相机的Y位置
        var cameraPosition = virtualCamera.transform.position;
        if (cameraPosition.y < minYPosition)
        {
            virtualCamera.transform.position = new Vector3(cameraPosition.x, minYPosition, cameraPosition.z);
        }
    }
}
