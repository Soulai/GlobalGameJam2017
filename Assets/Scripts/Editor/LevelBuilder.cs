using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.SceneManagement;

public class LevelBuilder : EditorWindow 
{
	public GameObject levelBuilderDataPrefab;

	private LevelBuilderData levelBuilderData;
	private Transform levelParent;
	private Texture2D levelTexture;
	/*private List<Color[][]> patchTextureArrays;
	private List<int> pivotIndexes;*/
	private Dictionary<Color, LevelBuilderObjectTypeColor> pixelTypes;
	//private Dictionary<WallPatchPixelType, Color> wallPatchPixelTypes;

	[MenuItem ("Window/Level Builder")]
	private static void Init () 
	{
		// Get existing open window or if none, make a new one:
		LevelBuilder levelBuilder = (LevelBuilder)EditorWindow.GetWindow(typeof (LevelBuilder));
		levelBuilder.Show();
	}

	void OnGUI () 
	{
		GUILayout.Label("Settings", EditorStyles.boldLabel);
		levelTexture = (Texture2D)EditorGUILayout.ObjectField("LevelTexture", levelTexture, typeof(Texture2D), false);
		if (GUILayout.Button("Generate level"))
		{
			GenerateLevel();
		}
	}

	private void GenerateLevel()
	{
		levelBuilderData = levelBuilderDataPrefab.GetComponent<LevelBuilderData>();

		pixelTypes = new Dictionary<Color, LevelBuilderObjectTypeColor>();
		foreach (LevelBuilderObjectTypeColor objectTypeColor in levelBuilderData.objectTypeColors)
		{
			pixelTypes.Add(objectTypeColor.color, objectTypeColor);
		}
		/*wallPatchPixelTypes = new Dictionary<WallPatchPixelType, Color>();
		foreach (WallPatchPixelTypeColor pixelTypeColor in levelBuilderData.patchPixelTypeColors)
		{
			wallPatchPixelTypes.Add(pixelTypeColor.patchPixelType, pixelTypeColor.color);
		}


		FindPivotIndexes();
		GeneratePatchTextureArrays();*/



		GameObject levelParentGameObject = new GameObject("Level");
		levelParent = levelParentGameObject.transform;

		Color[][] levelPixels = GetTexturePixelsDoubleArray(levelTexture);
		bool[][] occupiedPixels = new bool[levelPixels.Length][];
		for (int y = 0; y < levelTexture.height; y++)
		{
			occupiedPixels[y] = new bool[levelTexture.width];
		}

		for (int y = 0; y < levelTexture.height; y++)
		{
			for (int x = 0; x < levelTexture.height; x++)
			{
				TryUsePixel(x, y, levelPixels, occupiedPixels);
			}
		}
	}

	private void TryUsePixel(int x, int y, Color[][] levelPixels, bool[][] occupiedPixels)
	{
		const int blockSize = 3;

		Color levelPixel = levelPixels [y][x];
		if (!pixelTypes.ContainsKey(levelPixel))
		{
			Debug.LogError("Pixel not supported: "+levelPixel);
		}
		LevelBuilderObjectTypeColor objectTypeColor = pixelTypes[levelPixel];
		if (objectTypeColor.objectType != LevelBuilderObjectTypes.Empty)
		{
			occupiedPixels[y][x] = true;
			Transform prefabTransform = objectTypeColor.prefab.transform;
			Vector3 prefabPosition = new Vector3(x * blockSize + 1, 
												 objectTypeColor.prefab.transform.position.y, 
												 y * blockSize + 1);
			InstantiatePrefab(objectTypeColor.prefab, prefabPosition);
			return;
		}
	}

	private void InstantiatePrefab(GameObject prefab, Vector3 position)
	{
		GameObject gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, EditorSceneManager.GetActiveScene());
		gameObject.transform.parent = levelParent;
		gameObject.transform.position = position;
		gameObject.transform.rotation = prefab.transform.rotation;
	}

	/*private void TryCreateWall(int x, int y, Color[][] levelPixels, bool[][] occupiedPixels)
	{
		for (int i = 0; i < levelBuilderData.wallPatches.Count; i++)
		{
			Color[][] patchPixels = patchTextureArrays[i];
			int pivotPatchIndex = pivotIndexes [i];
			TryMatchPatch(levelBuilderData.wallPatches[i], patchPixels, levelPixels, pivotPatchIndex, x, y, occupiedPixels);
		}
	}

	private void FindPivotIndexes()
	{
		pivotIndexes = new List<int>();
		foreach (WallPatchPrefab wallPatchPrefab in levelBuilderData.wallPatches)
		{
			int pivotIndex = FindPivotIndex(wallPatchPrefab);
			pivotIndexes.Add(pivotIndex);
		}
	}

	private int FindPivotIndex(WallPatchPrefab wallPatchPrefab)
	{
		Texture2D patch = wallPatchPrefab.patch;
		Color[] pixels = patch.GetPixels();
		for (int i = 0; i < pixels.Length; i++)
		{
			if (pixels[i] == wallPatchPixelTypes[WallPatchPixelType.Pivot])
			{
				return i;
			}
		}
		Debug.LogError("Pivot pixel not found!");
		return -1;
	}

	private void GeneratePatchTextureArrays()
	{
		patchTextureArrays = new List<Color[][]>();
		foreach (WallPatchPrefab wallPatchPrefab in levelBuilderData.wallPatches)
		{
			Color[][] patchTextureArray = GetTexturePixelsDoubleArray(wallPatchPrefab.patch);
			patchTextureArrays.Add(patchTextureArray);
		}
	}

	private int GetPixelX(int index, int width, int height)
	{
		if (index / width > height || index < 0)
		{
			return -1;
		}
		return index % width;
	}

	private int GetPixelY(int index, int width, int height)
	{
		if (index / width > height || index < 0)
		{
			return -1;
		}
		return index / width;
	}

	private int GetPixelIndex(int x, int y, int width, int height)
	{
		if (x < 0 || y < 0 || x >= width || y >= height)
		{
			return -1;
		}
		return y * width + x;
	}*/

	public Color[][] GetTexturePixelsDoubleArray(Texture2D texture)
	{
		Color[][] pixels = new Color[texture.height][];
		for (int y = 0; y < texture.height; y++)
		{
			pixels[y] = new Color[texture.width];
			for (int x = 0; x < texture.width; x++)
			{
				pixels [y][x] = texture.GetPixel(x, y);
			}
		}
		return pixels;
	}

	/*private bool IsRectInsideTexture(int topLeftLevelX, int topLeftLevelY, int bottomRightLevelX, int bottomRightLevelY, 
									 int width, int height)
	{
		if (topLeftLevelX < 0 || topLeftLevelY < 0)
		{
			return false;
		}

		if (bottomRightLevelX >= width || bottomRightLevelY >= height)
		{
			return false;
		}

		return true;
	}

	private void TryMatchPatch(WallPatchPrefab wallPatchPrefab, Color[][] patchPixels, Color[][] levelPixels, 
							   int pivotPatchIndex, int pivotLevelX, int pivotLevelY, bool[][] occupiedPixels)
	{
		Texture2D patch = wallPatchPrefab.patch;
		int pivotPatchX = GetPixelX(pivotPatchIndex, patch.width, patch.height);
		int pivotPatchY = GetPixelY(pivotPatchIndex, patch.width, patch.height);
		int patchWidth = patchPixels[0].Length;
		int patchHeight = patchPixels.Length;

		int topLeftLevelX = pivotLevelX - pivotPatchX;
		int topLeftLevelY = pivotLevelY - pivotPatchY;
		int bottomRightLevelX = pivotLevelX + (patch.width - pivotPatchX - 1);
		int bottomRightLevelY = pivotLevelY + (patch.height - pivotPatchY - 1);
		int levelWidth = levelPixels[0].Length;
		int levelHeight = levelPixels.Length;

		if (!IsRectInsideTexture(topLeftLevelX, topLeftLevelY, bottomRightLevelX, bottomRightLevelY, levelWidth, levelHeight))
		{
			return;
		}

		for (int y = 0, levelY = topLeftLevelY; y < patchHeight; y++, levelY++)
		{
			for (int x = 0, levelX = topLeftLevelX; x < patchWidth; x++, levelX++)
			{
				if (levelY == pivotLevelY && levelX == pivotLevelX)
				{
					continue;
				}

				Color patchPixel = patchPixels[y][x];
				if (patchPixel == wallPatchPixelTypes[WallPatchPixelType.Ignore])
				{
					continue;
				}

				Color levelPixel = levelPixels [levelY][levelX];
				bool isOccupied = occupiedPixels[levelY][levelX];
				if (levelPixel != patchPixel || isOccupied)
				{
					return;
				}
			}
		}

		for (int levelY = topLeftLevelY; levelY < patchHeight; levelY++)
		{
			for (int levelX = topLeftLevelX; levelX < patchWidth; levelX++)
			{
				Color levelPixel = levelPixels [levelY][levelX];
				if (levelPixel == wallPatchPixelTypes[WallPatchPixelType.Wall])
				{
					occupiedPixels[levelY][levelX] = true;
				}
			}
		}

		Vector3 wallPosition = new Vector3(pivotLevelX, 0f, pivotLevelY);
		GameObject.Instantiate(wallPatchPrefab.prefab, wallPosition, Quaternion.identity, levelParent);
	}*/
}