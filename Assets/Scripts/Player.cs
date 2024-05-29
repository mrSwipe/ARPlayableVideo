using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private MeshRenderer buttonMeshRenderer;
    [SerializeField] private Material buttonPressedMaterial;

    private List<Material> buttonMaterials = new();
    private List<Material> buttonPressedMaterials;

    private bool isPlaying;

    private void Awake()
    {
        Debug.Assert(videoPlayer is not null, "Error! VideoPlayer is null");
        Debug.Assert(buttonMeshRenderer is not null, "Error! MeshRenderer is null");
        Debug.Assert(buttonPressedMaterial is not null, "Error! PressedMaterial is null");
        
        videoPlayer.gameObject.SetActive(false);
        buttonMeshRenderer.GetMaterials(buttonMaterials);
        buttonPressedMaterials = new List<Material> { buttonPressedMaterial };
    }

    public void StartPlay()
    {
        if (isPlaying)
        {
            Debug.Log("Video already playing!");
            return;
        }
        buttonMeshRenderer.SetMaterials(buttonPressedMaterials);
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        isPlaying = true;
    }

    private void EndReached(VideoPlayer source)
    {
        buttonMeshRenderer.SetMaterials(buttonMaterials);
        videoPlayer.loopPointReached -= EndReached;
        isPlaying = false;
    }
}
