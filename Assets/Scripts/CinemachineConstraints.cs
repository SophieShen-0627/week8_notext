using UnityEngine;
using Cinemachine;

public class CinemachineConstraints : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public float minOrthoSize = 5f; // ������С������С
    public float minYPosition = 0f; // ������СYλ��

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // ��鲢����������С
        if (virtualCamera.m_Lens.OrthographicSize < minOrthoSize)
        {
            virtualCamera.m_Lens.OrthographicSize = minOrthoSize;
        }

        // ��鲢���������Yλ��
        var cameraPosition = virtualCamera.transform.position;
        if (cameraPosition.y < minYPosition)
        {
            virtualCamera.transform.position = new Vector3(cameraPosition.x, minYPosition, cameraPosition.z);
        }
    }
}
