using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpPower;
    [SerializeField] private PlayerSkillController skillController;
    private Rigidbody2D rigidbody;
    private Player player;
    float xScale;
    float yScale;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        skillController = GetComponent<PlayerSkillController>();
        player = GetComponent<Player>();
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        if (skillController.IsExecuteSkill)
            return;

        float x = Input.GetAxisRaw("Horizontal");

        rigidbody.velocity = new Vector2(x * player.GetStatus().Speed, rigidbody.velocity.y);
        


        if (x > 0)
        {
            transform.localScale = new Vector3(xScale, yScale, 0);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(xScale * -1, yScale, 0);
        }
    }
}
