using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class WorldGenerator : MonoBehaviour {

	public Terrain terrain;

	/// <summary>
	/// Random noise in the terrain won't exceed this proportion of the terrain height (multiplied by noise scale).
	/// </summary>
	public float maxNoiseProportion = 0.6f;
	/// <summary>
	/// An additional scale factor for the noise.
	/// </summary>
	public float noiseScale = 0.08f;

	/// <summary>
	/// Scale for the perlin noise map.
	/// </summary>
	public float scale;

	/// <summary>
	/// Number of layers of different materials on the world.
	/// </summary>
	public int layers = 7;

	public GameObject[] additionalProps;

	private int[] layerOrder;

	/// <summary>
	/// RNG Seed, set to 0 to use the default.
	/// </summary>
	public int seed;

	public int mountainWidth = 20;

	private float perlinXOffset = 0.0f;
	private float perlinYOffset = 0.0f;

	private int mountainTextureIndex = 7;

	// Use this for initialization
	void Start () {
        seed = NetworkManager.getSeed();

		if (seed == 0)
			seed = Random.seed;

		int originalSeed = seed; // Store so we can set it back later
		Random.seed = seed;
		perlinXOffset = Random.value * 1000.0f;
		perlinYOffset = Random.value * 1000.0f;

		// Shuffle the layer order
		layerOrder = new int[layers];
		for (int i = 0; i < layers; i++)
			layerOrder[i] = (3 * i + (int)perlinXOffset) % layers;

		float[,] heightMap = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight];
		float[,,] alphaMaps = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapLayers];

		List<TreeInstance> trees = new List<TreeInstance>();
		
		for (int y = 0; y < terrain.terrainData.alphamapHeight; y++) {
			for (int x = 0; x < terrain.terrainData.alphamapWidth; x++) {

				// Height
				heightMap[x, y] = generateHeight(x, y);

				// Alpha layers (textures)
				int activeLayer = getActiveLayer(x);
				float positionInLayer = (float)(x - activeLayer * (float)terrain.terrainData.alphamapWidth / (float)layers) / ((float)terrain.terrainData.alphamapWidth / (float)layers);
				
				float xCoord = perlinXOffset + ((float)x / (float)terrain.terrainData.alphamapWidth) * scale;
				float yCoord = perlinYOffset + ((float)y / (float)terrain.terrainData.alphamapHeight) * scale / 1.5f;

				if (positionInLayer < 0.25f && activeLayer > 0) {
					// Interpolate with previous layer
					float param = positionInLayer / 0.25f;

					alphaMaps[x, y, layerOrder[activeLayer]] = param + Mathf.PerlinNoise (xCoord, yCoord) * (1.0f - param);
					alphaMaps[x, y, layerOrder[activeLayer - 1]] = 1.0f - alphaMaps[x, y, layerOrder[activeLayer]];

				} else if (positionInLayer > 0.3f && activeLayer < layers - 1) {
					// Add perlin noise towards next layer
					float param = (positionInLayer - 0.3f) / 0.7f;
					alphaMaps[x, y, layerOrder[activeLayer + 1]] = Mathf.PerlinNoise (xCoord, yCoord) * (1.0f - Mathf.Exp (-(param*param) / (0.5f * 0.7f * 0.7f)));
					alphaMaps[x, y, layerOrder[activeLayer]] = 1.0f - alphaMaps[x, y, layerOrder[activeLayer + 1]];
				} else {
					alphaMaps[x, y, layerOrder[activeLayer]] = 1.0f;
				}


				// Textures for the mountains
				if (y < mountainWidth*3 || y > terrain.terrainData.alphamapHeight - mountainWidth*3) {
					float param;
					if (y < mountainWidth*3)
						param = (float)y;
					else
						param = (float)(terrain.terrainData.alphamapHeight - y);
					
					float alpha = (1.0f - param / (mountainWidth*3));
					alphaMaps[x, y, mountainTextureIndex] = alpha;
					for (int i = 0; i < terrain.terrainData.alphamapLayers; i++) {
						if (i == mountainTextureIndex)
							continue;
						alphaMaps[x, y, i] *= (1 - alpha);
					}
				}
			}
		}

		terrain.terrainData.SetHeights(0, 0, heightMap);
		terrain.terrainData.SetAlphamaps(0, 0, alphaMaps);
		
		// Generate trees
		int numTrees = 1000 + (int)(Random.value * 600.0f);
		float treeX = Random.value;
		float treeY = Random.value;

		for (int i = 0; i < numTrees; i++) {
			float dx = Random.value * 0.1f;
			float dy = Random.value * 0.1f;

			if (Random.value > 0.5)
				treeX += dx;
			else
				treeX -= dx;

			if (Random.value > 0.5)
				treeY += dy;
			else
				treeY -= dy;

			if (treeX > 1.0f)
				treeX -= 1.0f;
			if (treeY > 1.0f)
				treeY -= 1.0f;

			if (treeX < 0)
				treeX += 1.0f;
			if (treeY < 0)
				treeY += 1.0f;

			if ((treeY < (float)(mountainWidth*2) / (float)terrain.terrainData.alphamapHeight ||
			     treeY > 1.0f - (float)(mountainWidth*2) / (float)terrain.terrainData.alphamapHeight))
				treeY = Random.value;

			TreeInstance tree = new TreeInstance();
			tree.color = Color.white;
			tree.heightScale = 0.75f + Random.value * 0.5f;
			tree.widthScale = 0.75f + Random.value * 0.5f;
			tree.lightmapColor = Color.white;
			tree.position = new Vector3(treeY, heightMap[(int)(treeX * terrain.terrainData.alphamapWidth), (int)(treeY * terrain.terrainData.alphamapHeight)], treeX);

			int activeLayer = (int)(treeX * (float)layers);
			float positionInLayer = treeX - (float)activeLayer / (float)layers;

			// Trees are mostly the same in the layer but may also be random
			if (Random.value > positionInLayer * 3) {
				tree.prototypeIndex = layerOrder[activeLayer] % terrain.terrainData.treePrototypes.Length;
			} else {
				tree.prototypeIndex = ((int)(Random.value * 100.0f)) % terrain.terrainData.treePrototypes.Length;
			}

			GameObject treeObject = Object.Instantiate(terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab, Vector3.zero, Quaternion.identity) as GameObject;
			treeObject.transform.position = new Vector3(tree.position.x * terrain.terrainData.size.x - terrain.terrainData.size.x / 2.0f,
			                                            tree.position.y * terrain.terrainData.size.y,
			                                            tree.position.z * terrain.terrainData.size.z - terrain.terrainData.size.z / 2.0f);
			//treeObject.transform.localScale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);

			Quaternion rot = treeObject.transform.rotation;
			rot.eulerAngles = new Vector3(0.0f, Random.value * 360.0f, 0.0f);
			treeObject.transform.rotation = rot;
			treeObject.transform.parent = terrain.transform;

			//trees.Add(tree);
		}

		// Generate props
		for (int i = 0; i < additionalProps.Length; i++) {
			int numProps = (int)(20 + Random.value * 50f);
			for (int j = 0; j < numProps; j++) {
				GameObject prop = Object.Instantiate(additionalProps[i]) as GameObject;
				prop.transform.position = new Vector3(300f + Random.value * (terrain.terrainData.size.x - 300f) - terrain.terrainData.size.x / 2.0f,
				                                      0,
				                                      300f + Random.value * (terrain.terrainData.size.z - 300f) - terrain.terrainData.size.z / 2.0f);
				Quaternion rot = prop.transform.rotation;
				rot.eulerAngles = new Vector3(0.0f, Random.value * 360.0f, 0.0f);
				prop.transform.rotation = rot;
				prop.transform.parent = terrain.transform;
			}
		}


		// Generate grass
		for (int i = 0; i < terrain.terrainData.detailPrototypes.Length; i++) {
			int[,] detailLayer = new int[terrain.terrainData.detailWidth, terrain.terrainData.detailHeight];

			for (int x = 0; x < terrain.terrainData.detailWidth; x++) {
				for (int y = 0; y < terrain.terrainData.detailHeight; y++) {
					detailLayer[x, y] = (int)(2.0f * Mathf.PerlinNoise(perlinXOffset + 0.5f * (float)x / (float)terrain.terrainData.detailWidth, perlinYOffset + 0.5f * (float)y / (float)terrain.terrainData.detailHeight));
				}
			}

			terrain.terrainData.SetDetailLayer(0, 0, i, detailLayer);
		}
		
		terrain.terrainData.treeInstances = trees.ToArray();
		terrain.Flush();

		Random.seed = originalSeed;
	}

	private int getActiveLayer(int x) {
		return (int)(((float)x / (float)terrain.terrainData.alphamapWidth) * (float)layers);
	}

	/// <summary>
	/// Given an x and y coordinate, generates a float [0.0 - 1.0] which is the height of that position in the height map.
	/// </summary>
	/// <returns>The height.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	float generateHeight(int x, int y) {
		
		float xCoord = perlinXOffset + ((float)x / (float)terrain.terrainData.alphamapWidth) * scale;
		float yCoord = perlinYOffset + ((float)y / (float)terrain.terrainData.alphamapHeight) * scale;

		if (y < mountainWidth*3 || y > terrain.terrainData.alphamapHeight - mountainWidth*3) {
			float param;
			if (y < mountainWidth*3)
				param = (float)y;
			else
				param = (float)(terrain.terrainData.alphamapHeight - y);

			float alpha = (1.0f - param / (mountainWidth*3));
			float tmp = (float)mountainWidth + 100.0f * Mathf.PerlinNoise(xCoord*3.0f,yCoord*3.0f);

			return alpha * (0.5f * Mathf.Exp(-(param*param) / (float)(0.5f * tmp * tmp))
				+ Mathf.PerlinNoise(xCoord, yCoord) + Mathf.PerlinNoise (xCoord * 3.0f, yCoord * 3.0f)) + 
				(1.0f - alpha) * 0.0f; //(Mathf.Min (Mathf.PerlinNoise(xCoord, yCoord), maxNoiseProportion) * noiseScale);
		}

		return 0.0f * Mathf.Min (Mathf.PerlinNoise(xCoord, yCoord), maxNoiseProportion) * noiseScale;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
