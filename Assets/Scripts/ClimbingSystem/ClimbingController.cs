using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    public EnvironmentChecker ec;
    public PlayerScript playerScript;

    public ClimbingPoint currentClimbPoint;

    public float InOutValue;
    public float UpDownValue;
    public float LeftRightValue;

    public ParkourActionUI parkouractionUi;

    private void Awake()
    {
        ec = GetComponent<EnvironmentChecker>();
        parkouractionUi = FindObjectOfType<ParkourActionUI>();
    }
    private void Update()
    {
        if (!playerScript.playerHanging)
        {
            if (!playerScript.playerInAction)
            {
                if (ec.CheckClimbing(transform.forward, out RaycastHit climbInfo))
                {
                    //UI표시용 클라이밍 준비 JUMP누르기전에도 보여줘야함
                }
            }
            if (Input.GetButton("Jump") && !playerScript.playerInAction)
            {
                if (ec.CheckClimbing(transform.forward, out RaycastHit climbInfo))
                {
                    currentClimbPoint = climbInfo.transform.GetComponent<ClimbingPoint>();

                    if (currentClimbPoint != null)
                    {
                        playerScript.SetControl(false);
                        InOutValue = -0.23f;
                        UpDownValue = -0.09f;
                        LeftRightValue = 0.15f;
                        // Debug.Log("Climb Point Found");
                        StartCoroutine(ClimbToLedge("IdleToClimb", climbInfo.transform, 0.40f, 54f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                }
            }

            if (!playerScript.playerInAction)
            {
                if (ec.CheckDropClimbPoint(out RaycastHit DropHit))
                {
                    //UI표시용 클라임 DropHang Leave누르기전에도 보여줘야함
                }
            }
            if (Input.GetButton("Leave") && !playerScript.playerInAction)
            {
                if (ec.CheckDropClimbPoint(out RaycastHit DropHit))
                {
                    currentClimbPoint = GetNearestClimbingPoint(DropHit.transform, DropHit.point);

                    if (currentClimbPoint != null)
                    {
                        playerScript.SetControl(false);
                        InOutValue = 0.1f;
                        UpDownValue = -0.44f;
                        LeftRightValue = 0.25f;
                        StartCoroutine(ClimbToLedge("DropToFreehang", currentClimbPoint.transform, 0.41f, 0.54f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                }
            }
        }
        else
        {
            parkouractionUi.BasicActionClear();
            parkouractionUi.SubActionClear();
            //[튜토리얼조작] Leave키를 눌러서 벽에서 벗어날수있다는 UI
            parkouractionUi.JumpwallAction.SetActive(true);
            //[튜토리얼조작] space키와 상하좌우 조합을 눌러라는 UI
            parkouractionUi.ClimbJumpAction.SetActive(true);

            //leave climb point
            if (Input.GetButton("Leave") && !playerScript.playerInAction)
            {
                StartCoroutine(JumpFromWall());
                return;
            }

            float horizontal = Mathf.Round(Input.GetAxisRaw("Horizontal"));
            float vertical = Mathf.Round(Input.GetAxisRaw("Vertical"));

            var inputDirection = new Vector2(horizontal, vertical);

            if (playerScript.playerInAction || inputDirection == Vector2.zero) return;

            //climb to top
            if (currentClimbPoint && currentClimbPoint.MountPoint && inputDirection.y == 1)
            {
                StartCoroutine(ClimbToTop());
                return;
            }

            //Ledge to Ledge parkour actions 

            var neighbour = currentClimbPoint != null ? currentClimbPoint.GetNeighbour(inputDirection) : null;
            if (neighbour == null) return;
    
            if (neighbour.connectionType == ConnectionType.Jump && Input.GetButton("Jump"))
            {
                currentClimbPoint = neighbour.climbingPoint;
               
                Debug.Log("ClimbJump action>>");
               
                if (currentClimbPoint != null)
                {
                    if (neighbour.pointDirection.y == 1)
                    {
                        Debug.Log("ClimbJump ClimbUp>>");
                        InOutValue = 0.1f;
                        UpDownValue = 0.05f;
                        LeftRightValue = 0.25f;
                        StartCoroutine(ClimbToLedge("ClimbUp", currentClimbPoint.transform, 0.34f, 0.64f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                    else if (neighbour.pointDirection.y == -1)
                    {
                        Debug.Log("ClimbJump ClimbDown>>");
                        InOutValue = 0.2f;
                        UpDownValue = 0.05f;
                        LeftRightValue = 0.25f;
                        StartCoroutine(ClimbToLedge("ClimbDown", currentClimbPoint.transform, 0.31f, 0.68f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                    else if (neighbour.pointDirection.x == 1)
                    {
                        Debug.Log("ClimbJump ClimbRight>>");
                        StartCoroutine(ClimbToLedge("ClimbRight", currentClimbPoint.transform, 0.20f, 0.51f));
                    }
                    else if (neighbour.pointDirection.x == -1)
                    {
                        Debug.Log("ClimbJump ClimbLeft>>");
                        InOutValue = 0.1f;
                        UpDownValue = 0.04f;
                        LeftRightValue = 0.25f;

                        StartCoroutine(ClimbToLedge("ClimbLeft", currentClimbPoint.transform, 0.20f, 0.51f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                }
            }
            else if (neighbour.connectionType == ConnectionType.Move)
            {
                currentClimbPoint = neighbour.climbingPoint;

                Debug.Log("ClimbMove action>>");

                if (currentClimbPoint != null)
                {
                    if (neighbour.pointDirection.x == 1)
                    {
                        Debug.Log("ClimbMove ShimmyRight>>");
                        InOutValue = 0.2f;
                        UpDownValue = 0.03f;
                        LeftRightValue = 0.25f;

                        StartCoroutine(ClimbToLedge("ShimmyRight", currentClimbPoint.transform, 0f, 0.30f, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                    else if (neighbour.pointDirection.x == -1)
                    {
                        Debug.Log("ClimbMove ShimmyLeft>>");
                        InOutValue = 0.2f;
                        UpDownValue = 0.03f;
                        LeftRightValue = 0.25f;

                        StartCoroutine(ClimbToLedge("ShimmyLeft", currentClimbPoint.transform, 0f, 0.30f, AvatarTarget.LeftHand, playerHandOffset: new Vector3(InOutValue, UpDownValue, LeftRightValue)));
                    }
                }
            }
        }
    }

    IEnumerator ClimbToLedge(string animationName, Transform ledgePoint, float compareStartTime, float compareEndTime,
        AvatarTarget hand = AvatarTarget.RightHand, Vector3? playerHandOffset = null)
    {
        var compareParams = new CompareTargetParameter()
        {
            position = SetHandPosition(ledgePoint, hand, playerHandOffset),
            bodyPart = hand,
            positionWeight = Vector3.one,
            StartTime = compareStartTime,
            endTime = compareEndTime
        };

        var requiredRot = Quaternion.LookRotation(-ledgePoint.forward);

        Debug.Log("ClimbToLedge PerformActionName>>" + animationName);
        yield return playerScript.PerformAction(animationName, compareParams, requiredRot, true);

        playerScript.playerHanging = true;
    }

    Vector3 SetHandPosition(Transform ledge, AvatarTarget hand, Vector3? playerhandOffset)
    {
        var offsetValue = (playerhandOffset != null) ? playerhandOffset.Value : new Vector3(InOutValue, UpDownValue, LeftRightValue);

        var handDirection = (hand == AvatarTarget.RightHand) ? ledge.right : -ledge.right;
        return ledge.position + ledge.forward * InOutValue + Vector3.up * UpDownValue - handDirection * LeftRightValue;
    }

    IEnumerator JumpFromWall()
    {
        playerScript.playerHanging = false;
        yield return playerScript.PerformAction("JumpFromWall");
        playerScript.ResetRequiredRotation();
        playerScript.SetControl(true);
    }

    IEnumerator ClimbToTop()
    {
        playerScript.playerHanging = false;
        yield return playerScript.PerformAction("ClimbToTop");

        playerScript.EnableCC(true);
        yield return new WaitForSeconds(0.5f);

        playerScript.ResetRequiredRotation();
        playerScript.SetControl(true);
    }

    ClimbingPoint GetNearestClimbingPoint(Transform dropClimbPoint, Vector3 hitPoint)
    {
        var points = dropClimbPoint.GetComponentsInChildren<ClimbingPoint>();

        ClimbingPoint nearestPoint = null;

        float nearestPointDistance = Mathf.Infinity;

        foreach (var point in points)
        {
            float distance = Vector3.Distance(point.transform.position, hitPoint);

            if (distance < nearestPointDistance)
            {
                nearestPoint = point;
                nearestPointDistance = distance;
            }
        }

        //Debug.Log("dropClimbPoint:" + dropClimbPoint.transform.name + ",GetNearestClimbingPoint" + nearestPoint);
        return nearestPoint;
    }
}