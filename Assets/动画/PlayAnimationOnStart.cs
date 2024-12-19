using UnityEngine;

public class PlayAnimationOnStart : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 1, 0); // 定义旋转速度

    void Update()
    {
        // 在每一帧按照指定速度旋转物体
        transform.Rotate(rotationSpeed);
    }
}