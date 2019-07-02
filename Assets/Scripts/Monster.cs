using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] float speed = 3f;
    Rigidbody2D monsterBody;
    CapsuleCollider2D monsterCollider;
    BoxCollider2D areaCollider;
    // Start is called before the first frame update
    void Start()
    {
        monsterBody = GetComponent<Rigidbody2D>();
        monsterBody.velocity = new Vector2(speed, 0f);
        monsterCollider = GetComponent<CapsuleCollider2D>();
        areaCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (right())
        {
            monsterBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            monsterBody.velocity = new Vector2(-speed, 0f);
        }
    }

    public bool right()
    {
        //Debug.Log(monsterBody.velocity.x > 0);
        return monsterBody.velocity.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(monsterBody.velocity.x)), 1f);
        if (right())
        {
            monsterBody.velocity = new Vector2(-speed, 0f);
        }
        else
        {
            monsterBody.velocity = new Vector2(speed, 0f);
        }
    }
}
