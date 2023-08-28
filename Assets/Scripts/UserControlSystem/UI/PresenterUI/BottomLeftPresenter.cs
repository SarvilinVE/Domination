using Domination.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private SelectableValue _selectedValue;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _selectedValue.OnSelected += OnSelected;
        }

        private void OnSelected(ISelecatable selected)
        {
            _selectedImage.enabled = selected != null;
            _healthSlider.gameObject.SetActive(selected != null);
            _text.enabled = selected != null;

            if (selected != null)
            {
                _selectedImage.sprite = selected.Icon;
                _text.text = $"{selected.Health} / {selected.MaxHeath}";
                _healthSlider.minValue = 0.0f;
                _healthSlider.maxValue = selected.MaxHeath;
                _healthSlider.value = selected.Health;
                var color = Color.Lerp(Color.red, Color.green, selected.Health / selected.MaxHeath);
                _sliderBackground.color = color * 0.5f;
                _sliderFillImage.color = color;
            }
        }

        #endregion

    }
}
