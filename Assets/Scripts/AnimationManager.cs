using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    public void UpdateAnimatorValues(float HorizonalMovement, float VerticalMovement, bool isSprinting)
    {
        //Animation Snapping logic
        float SnappedHorizontal;
        float SnappedVertical;

        #region Snapped Horizontal
        if (HorizonalMovement > 0 && HorizonalMovement < 0.55f)
        {
            SnappedHorizontal = 0.5f;
        }
        else if(HorizonalMovement > 0.55f)
        {
            SnappedHorizontal = 1;
        }
        else if(HorizonalMovement < 0 && HorizonalMovement > -0.55f)
        {
            SnappedHorizontal = -0.5f;
        }
        else if(HorizonalMovement < -0.55f)
        {
            SnappedHorizontal = -1;
        }
        else
        {
            SnappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (VerticalMovement > 0 && VerticalMovement < 0.55f)
        {
            SnappedVertical = 0.5f;
        }
        else if (VerticalMovement > 0.55f)
        {
            SnappedVertical = 1;
        }
        else if (VerticalMovement < 0 && VerticalMovement > -0.55f)
        {
            SnappedVertical = -0.5f;
        }
        else if (VerticalMovement < -0.55f)
        {
            SnappedVertical = -1;
        }
        else
        {
            SnappedVertical = 0;
        }
        #endregion

        if(isSprinting)
        {
            SnappedHorizontal = HorizonalMovement;
            SnappedVertical = 2;
        }

        animator.SetFloat(horizontal, SnappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, SnappedVertical, 0.1f, Time.deltaTime);
    }
}
