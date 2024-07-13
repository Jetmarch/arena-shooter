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
        private bool _isChoosed;
        private IItemContainerPresenter _itemContainerPresenter;

        public ArtefactType ArtefactType => _artefactConfig.ArtefactType;
        public Sprite Sprite => _artefactConfig.Sprite;
        public string Name => _artefactConfig.Name;
        public string Description => _artefactConfig.Description;
        public bool IsChoosed => _isChoosed;

        public ArtefactPresenter(ArtefactConfig artefactConfig, ArtefactView view, ArtefactSet artefactSet, IItemContainerPresenter itemContainerPresenter)
        {
            _artefactConfig = artefactConfig;
            _view = view;
            _artefactSet = artefactSet;
            _itemContainerPresenter = itemContainerPresenter;
        }

        public void ChooseArtefact(ArtefactType artefactType)
        {
            _artefactSet.PrimaryArtefact = artefactType;
            _itemContainerPresenter.ClearSelectedItem();
        }

        public void Initialize()
        {
            SetChooseState();
            _view.Setup(this);
        }

        private void SetChooseState()
        {
            if(_artefactSet.PrimaryArtefact == _artefactConfig.ArtefactType)
            {
                _isChoosed = true;
            }
            else
            {
                _isChoosed = false;
            }
        }
    }
}