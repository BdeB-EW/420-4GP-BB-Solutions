using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de d?placer un joueur avec un CharacterController
/// Le saut et la chute sont impl?ment?s
/// </summary>
public class MouvementJoueur : MonoBehaviour
{
    /// <summary>
    /// Le CharacterController du joueur
    /// </summary>
    private CharacterController characterController;

    /// <summary>
    /// La vitesse du d?placement
    /// </summary>
    private float vitesse;

    /// <summary>
    /// Une simulation de la gravit?
    /// </summary>
    private float gravite;

    /// <summary>
    /// Une simulation de saut vers le haut
    /// </summary>
    private float impulsion;
    
    /// <summary>
    /// Le facteur d'acc?l?ration pour la course
    /// </summary>
    private float facteurCourse;

    /// <summary>
    /// La v?locity est utilis?e pour simuler un effet de gravit?
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    /// La position initiale du joueur
    /// </summary>
    private Vector3 positionInitiale;

    /// <summary>
    /// La rotation initiale du joueur
    /// </summary>
    private Quaternion rotationInitiale;

    /// <summary>
    /// Le Game Manager qui est un singleton
    /// </summary>
    private GameManager gameManager;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravite = -9.8f;
        impulsion = 1.0f;
        positionInitiale = transform.position;
        rotationInitiale = transform.rotation;
        velocity = Vector3.zero;
        vitesse = 15.0f;
        gameManager = GameManager.Instance;
    }


    /// <summary>
    /// Le code est inspir? de l'exemple trouv? sur https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    /// </summary>
    void Update()
    {
        bool groundedPlayer = characterController.isGrounded;

        vitesse = gameManager.Vitesse;
        facteurCourse = gameManager.FacteurCourse;

        // Si on est sur le sol, on ne doit pas descendre
        if (groundedPlayer && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // D?placement selon les axes
        float horizontal = Input.GetAxis("Horizontal") * vitesse * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * vitesse * Time.deltaTime;
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = transform.TransformDirection(direction);
        characterController.Move(direction);

        if (Input.GetButton("Jump"))
        {
            if (velocity.y == 0)
            {
                velocity.y += Mathf.Sqrt(impulsion * -3.0f * gravite);
            }
        }

        velocity.y += gravite* Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


        // On lance ici le projectile
        if (Input.GetButtonDown("Fire1"))
        {
            // On sp?cifie le point de d?part et la force ? appliquer au projectile
            GetComponent<LancementProjectile>().LancerProjectile(transform.position + transform.forward * 2,
                transform.forward * 1000 + Vector3.up * 100);
        }
    }

    /// <summary>
    /// M?thode qui replace le joueur dans sa position et sa rotation initiale
    /// </summary>
    internal void ReplacerJoueur()
    {
        characterController.enabled = false;
        transform.position = positionInitiale;
        transform.rotation = rotationInitiale;
        characterController.enabled = true;
    }
}
