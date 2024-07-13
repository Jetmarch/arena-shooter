using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class ArtefactViewContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _artefactViewPrefab;

        [SerializeField]
        private List<ArtefactView> _artefactViews = new List<ArtefactView>();

        public ArtefactView CreateArtefactView()
        {
            var artefactView = Instantiate(_artefactViewPrefab, transform).GetComponent<ArtefactView>();
            if (artefactView == null)
            {
                throw new System.Exception($"{_artefactViewPrefab.name} prefab does not contain ArtefactView component!");
            }
            _artefactViews.Add(artefactView);
            return artefactView;
        }
    }
}