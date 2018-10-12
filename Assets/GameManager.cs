using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	double CurrentLowestY;

	public Vector3 CurrentLowestPosition;

	// For finding y position
	private float SmallestDistantBetweenPlatform = 1.5;
	private float BiggestDistantBetweenPlatform = 4;

	// For finding x position
	public float MaxLandingDistant = 1;

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

	public GameObject RestartButton;

	public bool IsInitializeScence;

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

		if(IsInitializeScence)
		{
			InitializeFirstFewScence();
		}
	}

	public void GameOver()
	{
		RestartButton.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}

	public void EndGame()
	{
		Application.Quit();
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
		
	void PlacePlatform(PlatformType platformType)
	{
		GameObject newPlatfrom;
		switch (platformType)
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
		CurrentLowestPosition = newPlatfrom.transform.position = GetRandomPosition(MostLeftX + newPlatformHalfWidth, MostRightX - newPlatformHalfWidth, newPlatfrom);

		BottomPlatForm = newPlatfrom;
		PlatformList.Add(BottomPlatForm);
	}

	private Vector3 GetRandomPosition(float mostLeftX, float mostRightX, GameObject newPlatFormObject)
	{
		//float previousPlaformHalfWidth = BottomPlatForm.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		//float newPlafromHalfWidth = newPlatFormObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		//float leftLandingXPosition = BottomPlatForm.transform.position.x - previousPlaformHalfWidth - MaxLandingDistant;
		//float rightLandingXPosition = BottomPlatForm.transform.position.x + previousPlaformHalfWidth - MaxLandingDistant;

		//// Left side
		//float mostRightXPositionOnLeftSide = leftLandingXPosition - newPlafromHalfWidth;
		//float mostLeftXPositionOnLeftSide = leftLandingXPosition + newPlafromHalfWidth;

		//// Right side
		//float mostRightXPositionOnRightSide = leftLandingXPosition - newPlafromHalfWidth;
		//float mostLeftXPositionOnRIghtSide = leftLandingXPosition + newPlafromHalfWidth;

		// Precision lost when casting to int from float, time 100 to reserve that precision
		int newMostLeftXInInt = (int)(mostLeftX * 100);
		int newMostRightXInInt = (int)(mostRightX * 100);

		float xPosition = ((float)(RandomGenerator.Next(newMostLeftXInInt, newMostRightXInInt)) / 100);

		if (BottomPlatForm.transform.position.x * xPosition > 0)
		{
			xPosition = -xPosition;
		}

		//var platformType = newPlatFormObject.GetComponent("Platform")
		//switch (platformType)
		//{
		//	case PlatformType.Grass:
		//}

		var newYPosition = BottomPlatForm.transform.position.y - GetVerticalDistantBetweenPlatform();
		return new Vector3(xPosition, newYPosition);
	}

	private float GetVerticalDistantBetweenPlatform()
	{
		return GetRandomNumberBetweenTwoFloat(SmallestDistantBetweenPlatform, BiggestDistantBetweenPlatform);
	}

	private float GetRandomNumberBetweenTwoFloat(float lowerLimitFloat, float upperLimitFloat)
	{
		const float preciseNumber = 100;
		int lowerLimitInt = (int)(lowerLimitFloat * preciseNumber);
		int upperLimitInt = (int)(upperLimitFloat * preciseNumber);

		float randomNumber = ((float)(RandomGenerator.Next(lowerLimitInt, upperLimitInt)) / preciseNumber);
		Debug.Log(randomNumber);
		return randomNumber;
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
}
