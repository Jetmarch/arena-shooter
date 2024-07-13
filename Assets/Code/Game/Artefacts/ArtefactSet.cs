namespace ArenaShooter.Artefacts
{
    public class ArtefactSet
    {
        private ArtefactType _primaryArtefact;

        public ArtefactType PrimaryArtefact { get { return _primaryArtefact; } set { _primaryArtefact = value; } }

        public ArtefactSet()
        {
            _primaryArtefact = ArtefactType.BulletsPassThrough;
        }
    }
}