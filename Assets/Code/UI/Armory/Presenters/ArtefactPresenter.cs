using ArenaShooter.Artefacts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        private TextMeshProUGUI _descriptionLabel;

        public ArtefactType ArtefactType => _artefactConfig.ArtefactType;
        public Sprite Sprite => _artefactConfig.Sprite;
        public string Name => _artefactConfig.Name;
        public string Description => _artefactConfig.Description;
        public bool IsChoosed => _isChoosed;

        public ArtefactPresenter(ArtefactConfig artefactConfig, ArtefactView view, ArtefactSet artefactSet, IItemContainerPresenter itemContainerPresenter, TextMeshProUGUI descriptionLabel)
        {
            _artefactConfig = artefactConfig;
            _view = view;
            _artefactSet = artefactSet;
            _itemContainerPresenter = itemContainerPresenter;
            _descriptionLabel = descriptionLabel;
        }

        public void ChooseArtefact(ArtefactType artefactType)
        {
            _artefactSet.PrimaryArtefact = artefactType;
            _itemContainerPresenter.ClearSelectedItem();
            _view.SelectItemAnimation();
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
                ChooseArtefact(_artefactConfig.ArtefactType);
            }
        }

        public void ShowDescription()
        {
            _descriptionLabel.text = Description;
        }

        public void HideDescription()
        {
            _descriptionLabel.text = string.Empty;
        }
    }
}