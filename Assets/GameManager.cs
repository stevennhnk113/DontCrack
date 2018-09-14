using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	double CurrentLowestY;

	Vector3 CurrentLowestPosition;

	public float DistantBetweenPlatform = 5;

	public GameObject FlatGrassPrefab;
	public GameObject MovingFlatGrassPrefab;

	void Start()
	{
		CurrentLowestPosition = GameObject.Find("FirstFlatGrass").transform.position;
		//PlacePlatform(PlatformType.FlatGrass);
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

	}
		
	void PlacePlatform(PlatformType type)
	{
		GameObject grass = GameObject.Find("Grass");
		Vector3 size = grass.GetComponent<SpriteRenderer>().bounds.size;

		Debug.Log(size);

		Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		float height = 2f * camera.orthographicSize;
		float width = height * camera.aspect;

		GameObject newPlatfrom = Instantiate(MovingFlatGrassPrefab, transform);
		newPlatfrom.transform.position = new Vector3((width / 2) - size.x, CurrentLowestPosition.y - DistantBetweenPlatform);
	}

	enum PlatformType
	{
		FlatGrass = 0,
		MovingFlatGrass = 1
	}
}
