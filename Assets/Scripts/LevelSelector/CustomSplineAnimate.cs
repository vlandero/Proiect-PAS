using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class CustomSplineAnimate : MonoBehaviour
{
    public SplineContainer spline;
    public Animator animator;
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;

    private float currentDistance = 0f;
    private int currentPortalIndex = 0;
    private int totalPortals = 0;
    private bool isMoving = false;
    private bool movingForward = true;

    private void Start()
    {
        totalPortals = spline.Spline.Count - 1;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPortalIndex == 0 || isMoving) return;
            currentPortalIndex--;
            isMoving = true;
            movingForward = false;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow");
            if (currentPortalIndex == totalPortals || isMoving) return;
            Debug.Log("Right Arrow");
            currentPortalIndex++;
            isMoving = true;
            movingForward = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isMoving) return;
            isMoving = true;
            animator.SetTrigger("EnterLevel");
            StartCoroutine(SwitchToLevelScene());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Scenes.MainMenu);
        }
        Vector3 targetPosition = spline.EvaluatePosition(currentDistance);
        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 targetDirection = spline.EvaluateTangent(currentDistance);

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (movingForward)
        {
            if (currentDistance >= 1f / totalPortals * currentPortalIndex)
            {
                isMoving = false;
                return;
            }
            else
            {
                float splineLength = spline.CalculateLength();
                float movement = moveSpeed * Time.deltaTime / splineLength;
                currentDistance += movement;
            }
        }
        else
        {
            if (currentDistance <= 1f / totalPortals * currentPortalIndex)
            {
                isMoving = false;
                return;
            }
            else
            {
                float splineLength = spline.CalculateLength();
                float movement = moveSpeed * Time.deltaTime / splineLength;
                currentDistance -= movement;
            }
        }
    }

    private IEnumerator SwitchToLevelScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(currentPortalIndex + 2);
    }
}