using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandControl : MonoBehaviour
{
    private GameObject playerHand;
    private GameObject playerHandFlip;

    [Header("Player Hand Index")]
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 handFlip;
    [SerializeField] private float angle;
    void Start()
    {
        playerHand = transform.Find("Player Hand").gameObject;
        playerHandFlip = transform.Find("Player Hand/Player Hand Flip").gameObject;
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
        }
        else
        {
            handFlip = new Vector3(1, -1, 1);
        }
        playerHandFlip.transform.localScale = handFlip;
    }

    void SetCanFollow()
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
