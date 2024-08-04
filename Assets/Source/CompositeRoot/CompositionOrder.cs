using System.Collections.Generic;
using UnityEngine;

namespace Stanislav.Backrooms.CompositeRoot
{
    internal class CompositionOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _order;

        private void Awake()
        {
            foreach (var compositionRoot in _order)
            {
                compositionRoot.Compose();
                compositionRoot.enabled = true;
            }
        }
    }
}
