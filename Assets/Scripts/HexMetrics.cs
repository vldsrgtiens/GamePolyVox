using UnityEngine;

public static class HexMetrics {

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
	

	
	public static Vector3 GetPositionXYFromHW(int w, int h)
	{
		Vector3 position;
		position.y = 0f;
		position.z = (h + w * 0.5f - w / 2) * (innerRadius * 2f);
		position.x = w * (outerRadius * 1.5f);
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
		int h = (int)Mathf.Ceil(z / (innerRadius * 2));
		int w = (int)Mathf.Ceil(x / (innerRadius * 2));
		int result = -1;
		if (w >= 0 && w < MapHex.Width && h >= 0 && h < MapHex.Height) result = (h * MapHex.Width + w);
		return  result;
	}
}