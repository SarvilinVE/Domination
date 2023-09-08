using Domination.Abstractions;
using System.Text;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;


namespace Domination.UserControlSystem
{
    public class GameOverScreenPresenter : MonoBehaviour
    {

        #region Fields

        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _view;

        #endregion


        #region MEthods

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                var sb = new StringBuilder($"GameOver!");
                if (result == 0)
                {
                    sb.AppendLine("Ничья!");
                }
                else
                {
                    sb.AppendLine($"Победила партия #{result}");
                }

                _view.SetActive(true);
                _text.text = sb.ToString();
                Time.timeScale = 0.0f;
            });
        }

        #endregion

    }
}
