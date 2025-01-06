using UnityEngine;

public class rabbit : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 按下W键时设置isForwardPressed为true，否则为false
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isForwardPressed", true);
        }
        else
        {
            animator.SetBool("isForwardPressed", false);
        }
        
     
        
    }
}