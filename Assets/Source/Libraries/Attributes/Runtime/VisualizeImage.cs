using System;
using UnityEngine;

namespace Source.Libraries.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class VisualizeImage : PropertyAttribute
    {
        public Texture2D Texture2D;

        public VisualizeImage()
        {
            // Хотел сделать так, чтобы картинку мы получали у поля Image к которому мы используем этот атрибут, но не получилось
            Texture2D = Texture2D.whiteTexture;
        }
    }
}