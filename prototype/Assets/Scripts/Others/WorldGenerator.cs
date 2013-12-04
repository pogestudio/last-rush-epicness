using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	/// <summary>
	/// RNG Seed.
	/// </summary>
	public int seed;

	public int mountainWidth = 20;

	private float perlinXOffset = 0.0f;
	private float perlinYOffset = 0.0f;

	private int mountainTextureIndex = 7;

	// Use this for initialization
	void Start () {
		Random.seed = seed;
		perlinXOffset = Random.value * 1000.0f;
		perlinYOffset = Random.value * 1000.0f;

		float[,] heightMap = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight];
		float[,,] alphaMaps = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapLayers];

		List<TreeInstance> trees = new List<TreeInstance>();
		
		for (int y = 0; y < terrain.terrainData.alphamapHeight; y++) {
			for (int x = 0; x < terrain.terrainData.alphamapWidth; x++) {

				// Height
				heightMap[x, y] = generateHeight(x, y);

				// Alpha layers (textures)
				int activeLayer = (int)(((float)x / (float)terrain.terrainData.alphamapWidth) * (float)layers);
				float positionInLayer = (float)(x - activeLayer * (float)terrain.terrainData.alphamapWidth / (float)layers) / ((float)terrain.terrainData.alphamapWidth / (float)layers);
				if (activeLayer == 0) {
					// First layer starts at 0 height and is a single alpha layer
					alphaMaps[x, y, activeLayer] = 1.0f;
					heightMap[x, y] = heightMap[x, y] * positionInLayer;
				} else {
					alphaMaps[x, y, activeLayer] = positionInLayer;
					alphaMaps[x, y, activeLayer - 1] = 1.0f - alphaMaps[x, y, activeLayer];
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
		int numTrees = 500 + (int)(Random.value * 3000.0f);
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
			tree.lightmapColor = Color.white;
			tree.position = new Vector3(treeY, heightMap[(int)(treeX * terrain.terrainData.alphamapWidth), (int)(treeY * terrain.terrainData.alphamapHeight)], treeX);

			int activeLayer = (int)(treeX * (float)layers);
			float positionInLayer = treeX - (float)activeLayer / (float)layers;

			// Trees are mostly the same in the layer but may also be random
			if (Random.value > positionInLayer * 3) {
				tree.prototypeIndex = activeLayer % terrain.terrainData.treePrototypes.Length;
			} else {
				tree.prototypeIndex = ((int)(Random.value * 100.0f)) % terrain.terrainData.treePrototypes.Length;
			}

			tree.widthScale = 0.75f + Random.value * 0.5f;
			trees.Add(tree);
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

			return alpha * (0.5f * Mathf.Exp(-(param*param) / (float)(0.5f * mountainWidth * mountainWidth))
				+ Mathf.PerlinNoise(xCoord, yCoord) + Mathf.PerlinNoise (xCoord * 3.0f, yCoord * 3.0f)) + 
				(1.0f - alpha) * (Mathf.Min (Mathf.PerlinNoise(xCoord, yCoord), maxNoiseProportion) * noiseScale);
		}

		return Mathf.Min (Mathf.PerlinNoise(xCoord, yCoord), maxNoiseProportion) * noiseScale;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
