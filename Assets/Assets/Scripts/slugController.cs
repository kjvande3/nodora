using UnityEngine;

// REF: https://forum.unity.com/threads/mini-tutorial-on-changing-sprite-on-runtime.212619/

public class slugController : MonoBehaviour {
     
	public float moveSpeed = 0.005f;


    void Update()
    {
        if (transform.position.y < 3.2f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + moveSpeed,
                transform.position.z);
        }
    }
}
