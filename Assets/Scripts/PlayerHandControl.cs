using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandControl : MonoBehaviour
{
    private PlayerStats stats;
    private GameObject playerHand;
    private GameObject playerHandFlip;

    private Transform playerHead;
    private Transform playerBody;
    private Transform playerLeg1;
    private Transform playerLeg2;

    [Header("Player Hand Index")]
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 handFlip;
    [SerializeField] private Vector3 bodyFlip;
    [SerializeField] private float angle;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        playerHand = transform.Find("Player Hand").gameObject;
        playerHandFlip = transform.Find("Player Hand/Player Hand Flip").gameObject;
        playerHead = transform.Find("Player Head");
        playerBody = transform.Find("Player Body");
        playerLeg1 = transform.Find("Player Leg 1");
        playerLeg2 = transform.Find("Player Leg 2");
    }

    // Update is called once per frame
    void Update()
    {
        HandFollowCursor();
    }

    public bool canFollow = true;

    void HandFollowCursor()
    {
        if (!canFollow) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerHand.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle <= 90 && angle >= -90)
        {
            handFlip = new Vector3(1, 1, 1);
            bodyFlip = new Vector3(1, 1, 1);
        }
        else
        {
            handFlip = new Vector3(1, -1, 1);
            bodyFlip = new Vector3(-1, 1, 1);
        }
        playerHandFlip.transform.localScale = handFlip;
        playerHead.localScale = bodyFlip;
        playerBody.localScale = bodyFlip;
        playerLeg1.localScale = bodyFlip;
        playerLeg2.localScale = bodyFlip;
    }

    public void SetCanFollow()
    {
        if (canFollow)
        {
            canFollow = false;
        }
        else
        {
            canFollow = true;
        }
    }
}
