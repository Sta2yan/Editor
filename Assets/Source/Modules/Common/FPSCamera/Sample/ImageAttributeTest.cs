using Source.Libraries.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Stanislav.FPSCamera.Sample
{
    public class ImageAttributeTest : MonoBehaviour
    {
        [SerializeField] private Image _image2;
        [SerializeField, VisualizeImage] private Image _image;
        [SerializeField] private Image _image3;
        [SerializeField] private GameObject _gameObject;
    }
}