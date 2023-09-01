using Domination.Abstractions;
using GluonGui.Dialog;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

namespace Domination.UserControlSystem
{
    public class MouseInteractionsPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        [SerializeField] private AttackableValue _attackablesRMB;

        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0.0f);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1)) return;

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            if (Input.GetMouseButtonUp(0))
            {
                if (WeHit<ISelecatable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            }
            else
            {
                if (WeHit<IAttackable>(hits, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }
        }

        private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }

            result = hits
                .Select(hit => hit.collider.GetComponent<T>())
                .Where(c => c != null)
                .FirstOrDefault();
            return result != default;
        }

        #endregion

    }
}
