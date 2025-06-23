using MyFolder._01.Script._02.Object.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyFolder._01.Script._02.Object.DangerZone
{
    public class DangerZoneTrack : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        [SerializeField] private float speed = 2f;
        [FormerlySerializedAs("Min")] [SerializeField] private float min;
        public void FixedUpdate()
        {
            string state = player.GetCurrentState();
            if (state == "IdleState")
            {
                transform.position += Vector3.right * (speed * Time.deltaTime);
            }
            else if(state == "DashState")
            {
                Vector3 newPos = transform.position + Vector3.left * (speed * Time.deltaTime);
                newPos.x = Mathf.Clamp(newPos.x, min, player.transform.position.x);
                transform.position =newPos;
            }
            else if (state == "DieState")
            {
                transform.position = new Vector3(min, 0, 0);
            }
        }
    }
}
