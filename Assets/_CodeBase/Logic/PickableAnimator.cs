using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _CodeBase.Logic
{
  public class PickableAnimator : MonoBehaviour
  {
    [SerializeField] private bool _animateRotation = true;
    [SerializeField] private bool _animateScale = true;
    [SerializeField] private bool _animateYOffset = true;

    [Space(10)] 
    [SerializeField, ShowIf("_animateRotation")] private RotationType _rotationType = RotationType.SelfAxis;
    [SerializeField, ShowIf("_animateRotation")] private Vector3 _rotationSpeedsInDegreePerSecond;
    [SerializeField, ShowIf("_animateScale")] private float _scaleMin = 0.5f;
    [SerializeField, ShowIf("_animateScale")] private float _scaleMax = 1.5f; 
    [SerializeField, ShowIf("_animateScale")] private float _scaleCycleDuration = 5;
    [SerializeField, ShowIf("_animateYOffset")] private float _yOffsetAmplitude = 1;
    [SerializeField, ShowIf("_animateYOffset")] private float _yOffsetCycleDuration = 5;

    private Vector3 _startLocalPosition;
    private Quaternion _startLocalRotation;
    private Vector3 _startLocalScale;

    private void Start() => UpdateDefaultValues();

    private void Update()
    {
      AnimateYOffset();
      AnimateScale();
      AnimateRotation();
    }

    public void UpdateDefaultValues()
    {
      _startLocalPosition = transform.localPosition;
      _startLocalRotation = transform.localRotation;
      _startLocalScale = transform.localScale;
    }

    private void AnimateYOffset()
    {
      if (_animateYOffset == false) return;

      float offset;

      if (_yOffsetCycleDuration != 0)
        offset = Mathf.Sin(Time.time / _yOffsetCycleDuration * Mathf.PI * 2) * _yOffsetAmplitude;
      else
        offset = 0;

      transform.localPosition = _startLocalPosition + new Vector3(0, offset, 0);
    }

    private void AnimateScale()
    {
      if (_animateScale == false) return;

      float scale;

      if (_scaleCycleDuration != 0)
      {
        float scaleT = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time / _scaleCycleDuration * Mathf.PI * 2));
        scale = Mathf.Lerp(_scaleMin, _scaleMax, scaleT);
      }
      else
        scale = 1;

      transform.localScale = scale * _startLocalScale;
    }

    private void AnimateRotation()
    {
      if (_animateRotation == false) return;

      transform.Rotate(_rotationSpeedsInDegreePerSecond * Time.deltaTime,
        _rotationType == RotationType.WorldAxis ? Space.World : Space.Self);
    }

    private enum RotationType
    {
      SelfAxis,
      WorldAxis
    }
  }
}