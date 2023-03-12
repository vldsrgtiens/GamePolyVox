using UnityEngine;
using System.Collections.Generic;

public class HexMetrics : MonoBehaviour{

	public const float outerRadius = 4.6f;

	public const float innerRadius = 4f;//outerRadius * 0.866025404f;

	public static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
	};
	

	
	public static Vector3 GetPositionCenterFromHW(int w, int h)
	{
		Vector3 position;
		position.y = 0f;
		//position.z = (h + w * 0.5f - w / 2) * (innerRadius * 2f);
		position.z = (h * (innerRadius * 2f) + ((w % 2) * innerRadius));
		//position.x = w * (outerRadius * 1.5f);
		position.x = w * (outerRadius + innerRadius / 2);
		return position;
	}

	public static int GetPositionNumFromHW(int w, int h)
	{
		int result = -1;
		if (w >= 0 && w < MapHex.Width && h >= 0 && h < MapHex.Height) result = (h * MapHex.Width + w);
		return result;
	}

	public static int GetPositionNumFromXY(float x, float z)
	{
		x = x + outerRadius;
		z = z + innerRadius;
		print("x="+x+" z="+z);
		int columnIsOdd = 1; //нечетный столбец=1 четный=0
		bool upperRect = true;
		int column;
		int row;
		float offset = innerRadius;
		
		column = (int)(x / (innerRadius/2 + outerRadius));
		float w_rem = (x % (innerRadius/2 + outerRadius));
		if (column % 2 == 0) columnIsOdd = 0;

		row = (int)((z - (columnIsOdd * offset)) / (innerRadius * 2));
		float h_rem = (z - (columnIsOdd * offset)) % (innerRadius * 2);
		if (h_rem < innerRadius) upperRect = false;
		
		print("column="+column+" row="+row+" w_rem="+w_rem+" h_rem="+h_rem);
		
		if (w_rem < (outerRadius - innerRadius))
		{
			if (upperRect)
			{
				if (((outerRadius - innerRadius) - (w_rem)) / (innerRadius - (h_rem -(columnIsOdd * offset))) < 1f)
				{
					if (columnIsOdd == 0) column = column - 1;
					else
					{
						column = column - 1;
						row = row + 1;
					}
				}
			}
			else
			{
				if (((outerRadius - innerRadius) - (w_rem)) / (-innerRadius - (h_rem  - (columnIsOdd * offset))) > -1f)
				{
					if (columnIsOdd == 0)
					{
						column = column - 1;
						row = row - 1;
					}
					else
					{
						column = column - 1;
					}
				}
			}
		}
		//print("w_int="+w_int+" w_rem="+w_rem);
		
		return  GetPositionNumFromHW(column,row);
	}
}