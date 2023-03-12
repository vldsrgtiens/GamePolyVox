using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {
	
	public HexCell cellPrefab;
	public GameObject hexCanvasPrefab;
	public ObjectItem grass;
	public ObjectItem stenaLevel1;
	public ObjectItem stenaLevel2;
	public ObjectItem stenaLevel1_2;
	public ObjectItem stenaLevel2_1;
	public ObjectItem rock1;
	public Text cellLabelPrefab;
	public static float qqq = 5f;

	public static HexCell[] cells;
	

	Canvas gridCanvas;

	void Start () {
		gridCanvas = GetComponentInChildren<Canvas>();

		cells = new HexCell[MapHex.Height * MapHex.Width];
		

		Debug.Log("Entry: ");

		for (int z = 0, i = 0; z < MapHex.Height; z++) {
			for (int x = 0; x < MapHex.Width; x++) {
				CreateCell(x, z, i++);
			}
		}
		
		for (int z = 0, i = 0; z < MapHex.Height; z++) {
			for (int x = 0; x < MapHex.Width; x++)
			{
				if (x % 2 == 0)
				{
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[0] =
						HexMetrics.GetPositionNumFromHW(x, z+1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[1] =
						HexMetrics.GetPositionNumFromHW(x + 1, z);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[2] =
						HexMetrics.GetPositionNumFromHW(x + 1, z-1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[3] =
						HexMetrics.GetPositionNumFromHW(x, z - 1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[4] =
						HexMetrics.GetPositionNumFromHW(x - 1, z-1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[5] =
						HexMetrics.GetPositionNumFromHW(x - 1, z);
				}
				else
				{
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[0] =
						HexMetrics.GetPositionNumFromHW(x, z + 1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[1] =
						HexMetrics.GetPositionNumFromHW(x + 1, z + 1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[2] =
						HexMetrics.GetPositionNumFromHW(x + 1, z);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[3] =
						HexMetrics.GetPositionNumFromHW(x, z - 1);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[4] =
						HexMetrics.GetPositionNumFromHW(x - 1, z);
					cells[HexMetrics.GetPositionNumFromHW(x, z)].neighbors[5] =
						HexMetrics.GetPositionNumFromHW(x - 1, z + 1);
				}
			}
		}

		for (int i = 0; i < MapHex.Height * MapHex.Width; i++)
		{
			string str = "";
			for (int x = 0; x < 6; x++)
			{
				str = str + ":" + cells[i].neighbors[x];
			}
			print("cells[" + i + "]  "+str);
		}

		FillTerrain();
		
		

	}

	
    
	// Start is called before the first frame update
	void FillTerrain()
	{
		float iRot=0f;
		for (int z = 0, i = 0; z < MapHex.Height; z++)
			for (int x = 0; x < MapHex.Width; x++ ) {
				Vector3 position = HexMetrics.GetPositionXYFromHW(x, z);
				cells[i].layer0 = ObjectList.AddItem(position, 0f,grass);

				if (MapHex.GridFull[x, z].Length == 2)
				{
					if (MapHex.GridFull[x, z] == "NN") iRot = 0f;
					if (MapHex.GridFull[x, z] == "NE") iRot = 60f;
					if (MapHex.GridFull[x, z] == "SE") iRot = 120f;
					if (MapHex.GridFull[x, z] == "SS") iRot = 180f;
					if (MapHex.GridFull[x, z] == "SW") iRot = 240f;
					if (MapHex.GridFull[x, z] == "NW") iRot = 300f;
					
					cells[i].layer1 = ObjectList.AddItem(position, iRot,stenaLevel1);
					cells[i].layer2 = ObjectList.AddItem(position+ new Vector3(0f,4.6f,0f), iRot,stenaLevel2);
					cells[i].canMove = 0;
				}

				if (MapHex.GridFull[x, z].Length == 3)
				{
					cells[i].layer2 = ObjectList.AddItem(position + new Vector3(0f,24.6f,0f), 0f,rock1);
					cells[i].canMove = 0;
				}
				
				if (MapHex.GridFull[x, z].Length == 4)
				{
					if (MapHex.GridFull[x, z] == "SESW") iRot = 0f;
					if (MapHex.GridFull[x, z] == "NENW") iRot = 180f;
					
					cells[i].layer1 = ObjectList.AddItem(position, iRot+180f,stenaLevel1_2);
					cells[i].layer2 = ObjectList.AddItem(position+ new Vector3(0f,4.8f,0f), iRot,stenaLevel2_1);
					cells[i].canMove = 0;
				}
				
				if (MapHex.GridFull[x, z].Length > 4)
				{
					cells[i].layer2 = ObjectList.AddItem(position+ new Vector3(0f,24.6f,0f), 0f,grass);
					cells[i].canMove = 0;
				}
					
				i++;
			}
	}
	
	void CreateCell (int x, int z, int i)
	{
		Vector3 position = HexMetrics.GetPositionXYFromHW(x, z);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cells[i].myIndex = i;
		cells[i].myPosition = position;
		cells[i].myHexCanvas = Instantiate<GameObject>(hexCanvasPrefab);
		cells[i].myHexCanvas.transform.position=position+new Vector3(0f,0.1f,0f);
		//cells[i].myHexCanvas.transform.position=position;
		
		

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = x.ToString() + ":" + z.ToString()+"\n["+i+"]";
	}


}