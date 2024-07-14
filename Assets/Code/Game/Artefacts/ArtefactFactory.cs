using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Artefacts
{
    public class ArtefactFactory : MonoBehaviour
    {
        [Serializable]
        public struct ArtefactFactoryData
        {
            public ArtefactType ArtefactType;
            public GameObject ArtefactPrefab;
        }

        [SerializeField]
        private Transform _artefactsParent;

        [SerializeField]
        private List<ArtefactFactoryData> _artefacts;

        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public void CreateArtefact(ArtefactType type)
        {
            var artefactPrefab = _artefacts.Find(x => x.ArtefactType == type).ArtefactPrefab;
            if (artefactPrefab == null)
            {
                throw new Exception($"ArtefactFactory:  type {type} does not contain artefact prefab!");
            }

            var createdArtefact = _container.InstantiatePrefab(artefactPrefab, _artefactsParent);
        }
    }
}