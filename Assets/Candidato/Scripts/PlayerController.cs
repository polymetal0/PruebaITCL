using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public GameObject targetDest;

    [SerializeField] private Transform movePosTx;

    [SerializeField] public GameObject pausePanel;

    [SerializeField] public GameObject pickupObjects;
    [SerializeField] public GameObject goal;

    private NavMeshAgent player;
    private Animator playerAnim;

    private CameraController cam;

    public static PlayingState state;
    public enum PlayingState
    {
        //Menu,
        Playing,
        Pause
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();

        state = PlayingState.Playing;

        cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // All objects picked up
        if (pickupObjects.transform.childCount == 0 && !goal.activeSelf)
        {
            goal.SetActive(true);
            StartCoroutine(cam.Focus(goal.transform));
        }
    }

    private void FixedUpdate()
    {
        if (player.velocity != Vector3.zero)
        {
            playerAnim.SetFloat("Speed", player.speed);
        }
        else
        {
            playerAnim.SetFloat("Speed", 0);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (state == PlayingState.Playing && context.performed)
        {

#if UNITY_ANDROID || UNITY_IOS
           Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.position.ReadValue());
#else
           Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

#endif

            RaycastHit raycast;
            NavMeshHit hit;

            if (Physics.Raycast(ray, out raycast)
                && NavMesh.SamplePosition(raycast.point, out hit, 1f, NavMesh.AllAreas)
                && context.performed)
            {
                movePosTx.position = hit.position;
                player.SetDestination(hit.position);
            }
        }


    }

    public void Pause()
    {
        state = PlayingState.Pause;

        player.SetDestination(player.transform.position);

        pausePanel.SetActive(true);

    }
    
    public void Resume()
    {
        state = PlayingState.Playing;

        player.SetDestination(movePosTx.position);
        
        pausePanel.SetActive(false);

    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
