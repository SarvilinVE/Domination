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

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            
            if (hits.Length == 0 )
            {
                return;
            }

            var mainBuilding = hits
                .Select(hit => hit.collider.GetComponent<ISelecatable>())
                .Where(c => c != null)
                .FirstOrDefault();
            _selectedObject.SetValue(mainBuilding);
        }

        #endregion

    }
}
