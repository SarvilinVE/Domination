using Domination.Abstractions;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Domination.UserControlSystem
{
    public class BottomLeftPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Image _selectedImage;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private Image _sliderFillImage;
        //[SerializeField] private SelectableValue _selectedValue;
        [Inject] private IObservable<ISelecatable> _selectedValues;

        #endregion


        #region UnityMethods

        private void Start()
        {
            //_selectedValue.OnNewValue += OnSelected;
            _selectedValues.Subscribe(OnSelected);
        }

        private void OnSelected(ISelecatable selected)
        {
            _selectedImage.enabled = selected != null;
            _healthSlider.gameObject.SetActive(selected != null);
            _text.enabled = selected != null;

            if (selected != null)
            {
                _selectedImage.sprite = selected.Icon;
                _text.text = $"{selected.Health} / {selected.MaxHealth}";
                _healthSlider.minValue = 0.0f;
                _healthSlider.maxValue = selected.MaxHealth;
                _healthSlider.value = selected.Health;
                var color = Color.Lerp(Color.red, Color.green, selected.Health / selected.MaxHealth);
                _sliderBackground.color = color * 0.5f;
                _sliderFillImage.color = color;
            }
        }

        #endregion

    }
}
