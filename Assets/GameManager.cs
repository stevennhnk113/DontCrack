using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	double CurrentLowestY;

	Vector3 CurrentLowestPosition;

	public float DistantBetweenPlatform = 5;

	public GameObject GrassPrefab;
	public GameObject HorizontalMovingGrassPrefab;

	private GameObject BottomPlatForm;
	private Vector3 BottomPlatformSize;

	public int InitialNumberOfGrass = 10;

	private float ScreenHeight;
	private float ScreenWidth;
	private float MostLeftX;
	private float MostRightX;

	private List<GameObject> PlatformList;

	System.Random RandomGenerator = new System.Random();

	private const int NumberOfPlatformToAddOrRemove = 10;

	void Start()
	{
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

		newPlatfrom.transform.position = GetRandomPosition();

		BottomPlatForm = newPlatfrom;
		PlatformList.Add(BottomPlatForm);
	}

	private Vector3 GetRandomPosition(float awayDistantFromPreviousPlatform = 0)
	{
		if(awayDistantFromPreviousPlatform == 0)
		{
			// Precision lost when casting to int from float, time 100 to reserve that precision
			int newMostLeftXInInt = (int)(MostLeftX * 100);
			int newMostRightXInInt = (int)(MostRightX * 100);

			float xPosition = ((float)(RandomGenerator.Next(newMostLeftXInInt, newMostRightXInInt)) / 100);

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
		PlatformList.RemoveRange(0, NumberOfPlatformToAddOrRemove);
	}

	enum PlatformType	
	{
		Grass = 0,
		HorizontalMovingGrass = 1
	}
}
