using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	double CurrentLowestY;

	public Vector3 CurrentLowestPosition;

	public float DistantBetweenPlatform = 5;

	public GameObject GrassPrefab;
	public GameObject HorizontalMovingGrassPrefab;

	private GameObject BottomPlatForm;
	private Vector3 BottomPlatformSize;

	public int InitialNumberOfGrass = 50;

	private float ScreenHeight;
	private float ScreenWidth;
	private float MostLeftX;
	private float MostRightX;

	private List<GameObject> PlatformList;

	System.Random RandomGenerator = new System.Random();

	private const int NumberOfPlatformToAddOrRemove = 10;

	private int Level = 1;

	public GameObject MenuCanvas;

	void Start()
	{
		// Getting the canvas
		MenuCanvas = GameObject.Find("Canvas");
		//MenuCanvas.SetActive(false);

		// Getting the screen size
		Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		ScreenHeight = 2f * camera.orthographicSize;
		ScreenWidth = ScreenHeight * camera.aspect;
		MostLeftX = -ScreenWidth / 2;
		MostRightX = -MostLeftX;

		// Get the first plat form
		BottomPlatForm = GameObject.Find("FirstGrass");
		CurrentLowestPosition = GameObject.Find("FirstGrass").transform.position;

		// Getting the platform size
		BottomPlatformSize = BottomPlatForm.GetComponent<SpriteRenderer>().bounds.size;

		PlatformList = new List<GameObject>();

		InitializeFirstFewScence();
	}

	public void GameOver()
	{
		MenuCanvas.SetActive(false);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void AddScene()
	{
		AddPlatforms();
		RemovePlatforms();
	}

	public void IncreaseLevel()
	{
		Level++;
	}

	void InitializeFirstFewScence()
	{
		System.Random randomGenerator = new System.Random();
		int selectedGrassType = 0;
		for(int i = 0; i < InitialNumberOfGrass; i++)
		{
			selectedGrassType = randomGenerator.Next(0, 2);
			PlacePlatform((PlatformType)selectedGrassType);
		}
	}
		
	void PlacePlatform(PlatformType type)
	{
		GameObject newPlatfrom;
		switch (type)
		{
			case PlatformType.Grass:
				newPlatfrom = Instantiate(GrassPrefab, transform);
				break;
			case PlatformType.HorizontalMovingGrass:
				newPlatfrom = Instantiate(HorizontalMovingGrassPrefab, transform);
				break;
			default:
				newPlatfrom = Instantiate(GrassPrefab, transform);
				break;
		}

		float newPlatformHalfWidth = newPlatfrom.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		CurrentLowestPosition = newPlatfrom.transform.position = GetRandomPosition(MostLeftX + newPlatformHalfWidth, MostRightX - newPlatformHalfWidth);

		BottomPlatForm = newPlatfrom;
		PlatformList.Add(BottomPlatForm);
	}

	private Vector3 GetRandomPosition(float mostLeftX, float mostRightX, float awayDistantFromPreviousPlatform = 0)
	{
		if(awayDistantFromPreviousPlatform == 0)
		{
			// Precision lost when casting to int from float, time 100 to reserve that precision
			int newMostLeftXInInt = (int)(mostLeftX * 100);
			int newMostRightXInInt = (int)(mostRightX * 100);

			float xPosition = ((float)(RandomGenerator.Next(newMostLeftXInInt, newMostRightXInInt)) / 100);

			if(BottomPlatForm.transform.position.x * xPosition > 0 )
			{
				xPosition = -xPosition;
			}

			return new Vector3(xPosition, BottomPlatForm.transform.position.y - DistantBetweenPlatform);
		}

		return new Vector3();
	}

	private void AddPlatforms()
	{
		System.Random randomGenerator = new System.Random();
		int selectedGrassType = 0;
		for (int i = 0; i < NumberOfPlatformToAddOrRemove; i++)
		{
			selectedGrassType = randomGenerator.Next(0, 2);
			PlacePlatform((PlatformType)selectedGrassType);
		}
	}

	private void RemovePlatforms()
	{
		for(int i = 0; i < NumberOfPlatformToAddOrRemove; i++)
		{
			Destroy(PlatformList[i]);
		}
		PlatformList.RemoveRange(0, NumberOfPlatformToAddOrRemove);

	}

	enum PlatformType	
	{
		Grass = 0,
		HorizontalMovingGrass = 1
	}
}
