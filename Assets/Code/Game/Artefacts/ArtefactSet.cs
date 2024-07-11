using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Artefacts
{
    public class ArtefactSet
    {
        private ArtefactType primaryArtefact;

        public ArtefactType PrimaryArtefact { get { return primaryArtefact; } set { primaryArtefact = value; } }
    }
}