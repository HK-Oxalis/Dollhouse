using UnityEngine;

public class WallScript : MonoBehaviour
{
    private bool isOpen = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        
        
    }


    public void Open_Wall()
    {
        if (isOpen)
        {
            return;
        }
        
        isOpen = true;
        
        animator.SetTrigger("Toggle_Wall");
    }


    public void Close_Wall()
    {
        if (!isOpen)
        {
            return;
        }
        
        isOpen = false;
        
        animator.SetTrigger("Toggle_Wall");
    }
}
