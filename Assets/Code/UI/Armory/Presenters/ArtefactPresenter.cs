using ArenaShooter.Artefacts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class ArtefactPresenter : IArtefactPresenter, IInitializable
    {
        private ArtefactConfig _artefactConfig;
        private ArtefactView _view;
        private ArtefactSet _artefactSet;

        public ArtefactType ArtefactType => _artefactConfig.ArtefactType;
        public Sprite Sprite => _artefactConfig.Sprite;
        public string Name => _artefactConfig.Name;
        public string Description => _artefactConfig.Description;

        public ArtefactPresenter(ArtefactConfig artefactConfig, ArtefactView view, ArtefactSet artefactSet)
        {
            _artefactConfig = artefactConfig;
            _view = view;
            _artefactSet = artefactSet;
        }

        public void ChooseArtefact(ArtefactType artefactType)
        {
            _artefactSet.PrimaryArtefact = artefactType;
            Debug.Log(_artefactSet.PrimaryArtefact);
        }

        public void Initialize()
        {
            _view.Setup(this);
        }
    }
}