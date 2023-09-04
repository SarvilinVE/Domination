using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Domination.UserControlSystem
{
    public class MenuPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _backButton.OnClickAsObservable().Subscribe(_ =>
            {
                gameObject.SetActive(false);
            });

            _exitButton.OnClickAsObservable().Subscribe(_ =>
            {
                Application.Quit();
            });
        }

        #endregion

    }
}
