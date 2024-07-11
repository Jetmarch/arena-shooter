using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArenaShooter.UI
{
    public class ArtefactView : ItemView, IPointerClickHandler
    {
        [SerializeField]
        private ArtefactType _type;
        public ArtefactType Type { get { return _type; } set { _type = value; } }

        private IArtefactPresenter _presenter;

        public override void Setup(IItemPresenter presenter)
        {
            var artefactPresenter = presenter as IArtefactPresenter;
            if (artefactPresenter == null)
            {
                throw new System.Exception($"Type mismatch! Presenter is not IArtefactPresenter. Actual type: {presenter.GetType().Name}");
            }
            base.Setup(presenter);
            _type = artefactPresenter.ArtefactType;
            _presenter = artefactPresenter;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _presenter.ChooseArtefact(_type);
        }
    }
}