using System;
using Source.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

namespace Source.Scripts.Enviroment.Truck
{
    public class TruckView : MonoBehaviour
    {
        [SerializeField] [SerializeInterface(typeof(ITruckView))] private GameObject _track;
        [SerializeField] private LookAtConstraint _lookAtConstraint;
        [SerializeField] private TMP_Text _value;

        private ITruckView _trackView;

        private void Awake()
        {
            _lookAtConstraint.AddSource(new ConstraintSource { sourceTransform = Camera.main.transform, weight = 1 });

            _trackView = _track.GetComponent<ITruckView>();
        }

        private void OnValidate()
        {
            if (_lookAtConstraint == null)
                throw new ArgumentNullException(nameof(_lookAtConstraint));

            if (_track == null)
                throw new ArgumentNullException(nameof(_track));
        }

        private void OnEnable()
        {
            _trackView.ValueChanged += ChangeValue;
        }

        private void OnDisable()
        {
            _trackView.ValueChanged -= ChangeValue;
        }

        private void ChangeValue(int curent, int max)
        {
            _value.text = $"{curent}/{max}";
        }
    }
}
