using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	double CurrentLowestY;

	public Vector3 CurrentLowestPosition;

	// For finding y position
	private float SmallestDistantBetweenPlatform = 1.5f;
	private float BiggestDistantBetweenPlatform = 4;

	// For finding x position
	public float MaxLandingDistant = 1.5f;

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

		//Debug.Log("MostLeftX" + MostLeftX);
		//Debug.Log("MostRightX" + MostRightX);

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
			selectedGrassType = randomGenerator.Next(1, 3);
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
				CurrentLowestPosition = GetRandomGrassPosition(newPlatfrom);
				break;
			case PlatformType.HorizontalMovingGrass:
				newPlatfrom = Instantiate(HorizontalMovingGrassPrefab, transform);
				CurrentLowestPosition = GetRandomAnyPosition(newPlatfrom);
				break;
			default:
				newPlatfrom = Instantiate(GrassPrefab, transform);
				CurrentLowestPosition = GetRandomAnyPosition(newPlatfrom);
				break;
		}

		newPlatfrom.transform.position = CurrentLowestPosition;
		BottomPlatForm = newPlatfrom;
		PlatformList.Add(BottomPlatForm);
	}

	private Vector3 GetRandomAnyPosition(GameObject newPlatFormObject)
	{
		float newPlatformHalfWidth = newPlatFormObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		var xPosition = GetRandomNumberBetweenTwoFloat(MostLeftX + newPlatformHalfWidth, MostRightX - newPlatformHalfWidth);
		var yPosition = BottomPlatForm.transform.position.y - GetVerticalDistantBetweenPlatform();
		return new Vector3(xPosition, yPosition);
	}

	private Vector3 GetRandomGrassPosition(GameObject newPlatFormObject)
	{
		float xPosition;
		float newPlatformHalfWidth = newPlatFormObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

		if (BottomPlatForm.tag == "Grass")
		{
			float previousPlaformHalfWidth = BottomPlatForm.GetComponent<SpriteRenderer>().bounds.size.x / 2;

			// Left side
			float landingXPosition = BottomPlatForm.transform.position.x - previousPlaformHalfWidth - MaxLandingDistant;
			float mostLeftXPosition = landingXPosition - newPlatformHalfWidth;
			float mostRightXPosition = landingXPosition + newPlatformHalfWidth;
			float leftXPosition = GetRandomNumberBetweenTwoFloat(mostLeftXPosition, mostRightXPosition);

			// Right side
			landingXPosition = BottomPlatForm.transform.position.x + previousPlaformHalfWidth + MaxLandingDistant;
			mostLeftXPosition = landingXPosition - newPlatformHalfWidth;
			mostRightXPosition = landingXPosition + newPlatformHalfWidth;
			float rightXPosition = GetRandomNumberBetweenTwoFloat(mostLeftXPosition, mostRightXPosition);

			if (leftXPosition - newPlatformHalfWidth > MostLeftX && rightXPosition + newPlatformHalfWidth < MostRightX)
			{
				xPosition = (RandomLeftOrRight()) ? leftXPosition : rightXPosition;
			}
			else if (leftXPosition - newPlatformHalfWidth < MostLeftX)
			{
				xPosition = rightXPosition;
			}
			else
			{
				xPosition = leftXPosition;
			}
		}
		else
		{
			xPosition = GetRandomNumberBetweenTwoFloat(MostLeftX + newPlatformHalfWidth, MostRightX - newPlatformHalfWidth);
		}

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
		return randomNumber;
	}

	// True is left. false is right
	private bool RandomLeftOrRight()
	{
		if(RandomGenerator.Next(0, 2) == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
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
