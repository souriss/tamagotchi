using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Platform_Generator : MonoBehaviour
{
    [SerializeField] private GameObject Platform_Green;
    private GameObject Platform;

    [SerializeField] private float Current_Y = 0;
    float Offset;
    Vector3 Top_Left;
    [SerializeField] private int platformsGenerated = 1;
    private bool additionalPlatformsGenerated = false;

    private List<GameObject> generatedPlatforms = new List<GameObject>();
    private Transform deadPointTransform;

    [SerializeField] private StartGame startGame;

    void Start()
    {
        Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Offset = 1.2f;

        DeadPoint deadPoint = FindObjectOfType<DeadPoint>();
        if (deadPoint != null)
        {
            deadPointTransform = deadPoint.transform;
        }
        else
        {
            Debug.LogError("DeadPoint not found!");
        }
    }

    private void Update()
    {
        RemovePlatformsBelowTarget();

        if (additionalPlatformsGenerated)
        {
            int additionalPlatforms = 5;
            Generate_Platform(additionalPlatforms);
            additionalPlatformsGenerated = false;
            platformsGenerated = 1;
        }
    }

    public void StartPlatformGeneration(int numPlatforms)
    {
        Generate_Platform(numPlatforms);
    }

    public void Generate_Platform(int Num)
    {
        for (int i = 0; i < Num; i++)
        {
            float Dist_X = Random.Range(Top_Left.x + Offset, -Top_Left.x - Offset);
            float Dist_Y = Random.Range(2f, 4f);

            Current_Y += Dist_Y;
            Vector3 Platform_Pos = new Vector3(Dist_X, Current_Y, 0);

            Platform = Instantiate(Platform_Green, Platform_Pos, Quaternion.identity);

            generatedPlatforms.Add(Platform);
        }
    }

    private void RemovePlatformsBelowTarget()
    {
        if (deadPointTransform == null)
        {
            Debug.LogError("DeadPoint transform is null!");
            return;
        }

        float targetY = deadPointTransform.position.y;

        for (int i = generatedPlatforms.Count - 1; i >= 0; i--)
        {
            if (generatedPlatforms[i].transform.position.y < targetY)
            {
                Destroy(generatedPlatforms[i]);
                generatedPlatforms.RemoveAt(i);
            }
        }
    }

    public void IncreasePlatformCount()
    {
        if (startGame.GameStarted()) 
        { 
            platformsGenerated++;

            if (platformsGenerated >= 7 && !additionalPlatformsGenerated)
            {
                additionalPlatformsGenerated = true;
            }
        }
    }
}
