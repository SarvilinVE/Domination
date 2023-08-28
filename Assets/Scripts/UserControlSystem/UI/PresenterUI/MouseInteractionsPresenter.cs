using Domination.Abstractions;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Domination.UserControlSystem
{
    public class MouseInteractionsPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

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
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) return;

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                var hits = Physics.RaycastAll(ray);

                if (hits.Length == 0)
                {
                    return;
                }

                var selectable = hits
                    .Select(hit => hit.collider.GetComponent<ISelecatable>())
                    .Where(c => c != null)
                    .FirstOrDefault();
                _selectedObject.SetValue(selectable);
            }
            else
            {
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }

            //var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            
            //if (hits.Length == 0 )
            //{
            //    return;
            //}

            //var mainBuilding = hits
            //    .Select(hit => hit.collider.GetComponent<ISelecatable>())
            //    .Where(c => c != null)
            //    .FirstOrDefault();
            //_selectedObject.SetValue(mainBuilding);
        }

        #endregion

    }
}
