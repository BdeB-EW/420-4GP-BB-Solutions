using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EtatPatrouille : EtatMouvement
{
    private Transform[] objectifs;
    private int indiceObjectifs;
    private bool aller;
    
    public EtatPatrouille(GameObject sujet, Transform[] lesObjectifs, GameObject joueur) : base(sujet, joueur)
    {

        objectifs = lesObjectifs;
        indiceObjectifs = 0;
    }

    public override void Enter()
    {
        Animations.SetBool("Run", true);
    }

    public override void Handle()
    {
        Vector3 positionActuelle = Sujet.transform.position;

        if (AgentMouvement.remainingDistance <= AgentMouvement.stoppingDistance)
        {
            if (aller)
            {
                indiceObjectifs++;
            }
            else
            {
                indiceObjectifs--;
            }

            if (indiceObjectifs == objectifs.Length)
            {
                aller = false;
            }

            if (indiceObjectifs < 0)
            {
                aller = true;
            }

            if (indiceObjectifs >= 0 && indiceObjectifs < objectifs.Length)
            {
                Vector3 nouvelleDestination = objectifs[indiceObjectifs].position;
                AgentMouvement.SetDestination(nouvelleDestination);
            }
        }
    }

    public override void Leave()
    {
        Animations.SetBool("Run", false);
    }
}
