using UnityEngine;

namespace _CodeBase.PlayerCode.Data
{
  [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "Settings/PlayerMovement")]
  public class PlayerMovementSettings : ScriptableObject
  {
    public Vector3 MoveSpeed;
    public float MaxMovePerTimeX;
    public float ClampXPerUnit;
  }
}