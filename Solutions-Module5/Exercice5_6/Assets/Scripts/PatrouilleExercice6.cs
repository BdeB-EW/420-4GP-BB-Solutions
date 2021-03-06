using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrouilleExercice6 : MonoBehaviour
{

    /// <summary>
    /// Les points de la patrouille
    /// </summary>
    [SerializeField] private Transform[] pointsPatrouille;

    private EtatMouvement mouvement;
    private EtatPatrouille patrouille;
    private EtatPoursuite poursuite;

    internal EtatPatrouille Patrouille
    {
        get { return patrouille; }
    }


    // Start is called before the first frame update
    void Start()
    {
        patrouille = new EtatPatrouille(gameObject, pointsPatrouille, GameObject.Find("Joueur"));
        mouvement = patrouille;
        patrouille.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        mouvement.Handle();    
    }

    internal void ChangerEtat(EtatMouvement nouvelEtat)
    {
        mouvement.Leave();
        mouvement = nouvelEtat;
        mouvement.Enter();
    }
}
