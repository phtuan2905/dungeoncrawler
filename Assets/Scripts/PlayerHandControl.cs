using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandControl : MonoBehaviour
{
    private PlayerStats stats;
    private GameObject playerHand;
    private GameObject playerHandFlip;

    private SpriteRenderer playerHead;
    private SpriteRenderer playerBody;
    private SpriteRenderer playerLeg1;
    private SpriteRenderer playerLeg2;

    [Header("Player Hand Index")]
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 handFlip;
    [SerializeField] private float angle;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        playerHand = transform.Find("Player Hand").gameObject;
        playerHandFlip = transform.Find("Player Hand/Player Hand Flip").gameObject;
        playerHead = transform.Find("Player Head").GetComponent<SpriteRenderer>();
        playerBody = transform.Find("Player Body").GetComponent<SpriteRenderer>();
        playerLeg1 = transform.Find("Player Leg 1").GetComponent<SpriteRenderer>();
        playerLeg2 = transform.Find("Player Leg 2").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandFollowCursor();
    }

    bool canFollow = true;

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
            playerHead.flipX = false;
            playerBody.flipX = false;
            playerLeg1.flipX = false;
            playerLeg2.flipX = true;
        }
        else
        {
            handFlip = new Vector3(1, -1, 1);
            playerHead.flipX = true;
            playerBody.flipX = true;
            playerLeg1.flipX = true;
            playerLeg2.flipX = false;
        }
        playerHandFlip.transform.localScale = handFlip;
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
