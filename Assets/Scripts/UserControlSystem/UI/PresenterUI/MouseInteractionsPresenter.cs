using Domination.Abstractions;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Domination.Utils;


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


        #region Methods

        [Inject]
        private void Init()
        {
            _groundPlane = new Plane(_groundTransform.up, 0.0f);

            var nonBlockedByUiFramesStream = Observable.EveryUpdate()
                .Where(_ => !_eventSystem.IsPointerOverGameObject());

            var leftClicksStream = nonBlockedByUiFramesStream
                .Where(_ => Input.GetMouseButtonDown(0));

            var rightClicksStream = nonBlockedByUiFramesStream
                .Where(_ => Input.GetMouseButtonDown(1));

            var lmbRays = leftClicksStream
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var rmbRays = rightClicksStream
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

            var lmbHitsStream = lmbRays
                .Select(ray => Physics.RaycastAll(ray));
            var rmbHitsStream = rmbRays
                .Select(ray => (ray, Physics.RaycastAll(ray)));

            lmbHitsStream.Subscribe(hits =>
            {
                if (WeHit<ISelecatable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            });

            rmbHitsStream.Subscribe((ray, hits) =>
            {
                if (WeHit<IAttackable>(hits, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            });
        }

        #endregion


        #region UnityMethods

        //private void Start()
        //{
        //    _groundPlane = new Plane(_groundTransform.up, 0.0f);
        //}

        //private void Update()
        //{
        //    if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1)) return;

        //    if (_eventSystem.IsPointerOverGameObject())
        //    {
        //        return;
        //    }

        //    var ray = _camera.ScreenPointToRay(Input.mousePosition);
        //    var hits = Physics.RaycastAll(ray);

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        if (WeHit<ISelecatable>(hits, out var selectable))
        //        {
        //            _selectedObject.SetValue(selectable);
        //        }
        //    }
        //    else
        //    {
        //        if (WeHit<IAttackable>(hits, out var attackable))
        //        {
        //            _attackablesRMB.SetValue(attackable);
        //        }
        //        else if (_groundPlane.Raycast(ray, out var enter))
        //        {
        //            _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
        //        }
        //    }
        //}

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
