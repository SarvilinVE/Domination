using Domination.Abstractions;
using System;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;


namespace Domination.UserControlSystem
{
    public class TopPanelPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuGO;

        #endregion


        #region Methods

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _inputField.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            });

            _menuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _menuGO.SetActive(true);
            });
        }

        #endregion

    }
}
