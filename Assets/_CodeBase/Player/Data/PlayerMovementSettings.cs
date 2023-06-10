using UnityEngine;

namespace _CodeBase.Player.Data
{
  [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "Settings/PlayerMovement")]
  public class PlayerMovementSettings : ScriptableObject
  {
    public Vector3 MoveSpeed;
    public float ClampXPerUnit;
  }
}