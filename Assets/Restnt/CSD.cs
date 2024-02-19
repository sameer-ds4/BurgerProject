using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSD : MonoBehaviour
{
	public int[] numbers;
	// public int[] das;

	public string csc;
	public string ccd;

	void Start()
	{
		// Reverse(numbers);
		// Sort012(numbers, numbers.Length - 1);
		// RotateK(numbers, 3);
		// MinMax(numbers);
		// MinMax_K(numbers, 2);
		// Union(numbers, das);
		// Intersection(numbers, das);
		// NegativeSide(numbers);
		// SubASum(numbers);
		// MinMax_Heights(numbers, 3);
		// Debug.LogError(DuplicateNum(numbers));
		// ReverseString(csc);
		// PalindromCheck(csc);
		Debug.LogError(StringsSimilar(csc, ccd));
	}


	void ReverseString(string sas)
	{
		char[] ss = sas.ToCharArray();

		for (int i = ss.Length - 1; i >= 0; i--)
		{
			Debug.LogError(ss[i]);
		}
	}

	void PalindromCheck(string str)
	{
		char[] ss = str.ToCharArray();
		int n = 0; 
		n = str.Length - 1;

		for (int i = 0; i < str.Length/2; i++)
		{
			if (ss[i] != ss[n])
				Debug.LogError("Not Palindrome");

			n--;
		}
	}

	bool StringsSimilar(string s1, string s2)
	{
		return (s1.Length == s2.Length)
            && ((s1 + s1).IndexOf(s2) != -1);
	}

	//-----------------

	void MinMax_K(int[] a, int k)
	{
		int min = a[0];
		int max = a[0];

		Array.Sort(a);

		Debug.LogError("K min " + a[k - 1]);
		Debug.LogError("K max " + a[a.Length - k]);
	}

	void Reverse(int[] a)
	{
		for (int i = a.Length - 1; i > 0; i--)
		{
			Debug.LogError(a);
		}
	}

	int DuplicateNum(int[] a)
	{
		Array.Sort(a);

		int z = 0;
		for (int i = 0; i < a.Length - 1; i++)
		{
			if (a[i] == a[i + 1])
				z = a[i];
		}
		return z;
	}


	void JumpArray(int[] a, int l, int n)
	{
		if (a[l] == 0) return;
		if (l == n) return;

		// for (int i = l + 1; i < a.Length && i < l + a[l]; i++)
		// {
		// 	int jumps = JumpArray(a, i, n); 
        //     if (jumps != int.MaxValue && jumps + 1 < min) 
        //         min = jumps + 1; 
		// }
	}

	
	void MinMax(int[] aa)
	{
		int min = aa[0];
		int max = aa[0];

		for (int i = 1; i < aa.Length; i++)
		{
			if(aa[i] < min)
			{
				min = aa[i];
			}
			if(aa[i] > max)
			{
				max = aa[i];
			}
		}

		Debug.LogError("min " + min);
		Debug.LogError("max " + max);
	}
	
	void MinMax_Heights(int[] a, int k)
	{
		Array.Sort(a);

		int min, max;
		int ans = a[a.Length - 1] - a[0];
		Debug.LogError(ans);
		
		for (int i = 0; i < a.Length; i++)
		{
			min = (int)MathF.Min(a[0] + k, a[i + 1] - k);
			max = (int)MathF.Max(a[a.Length - 1], a[i] + k);

			ans = (int)MathF.Min(ans, max - min);
		}

		Debug.LogError(ans);
	}

	void SubASum(int[] asd)
	{
		int maxMain = 0;
		int maxCur = 0;
		
		for (int i = 0; i < asd.Length; i++)
		{
			maxCur = maxCur + asd[i];

			if (maxCur > maxMain)
				maxMain = maxCur;
			if (maxCur < 0)
				maxCur = 0;
		}

		Debug.LogError(maxMain);
	}

	void Sort012(int[] nums, int c)
	{
		int lo = 0;
		int mid = 0;
		int hi = c;
		int tmp;

		while(mid < hi)
		{
			switch (nums[mid])
			{
				case 0:
					tmp = nums[lo];
					nums[lo] = nums[mid];
					nums[mid] = tmp;

					lo++;
					mid++;
					break;

				case 1:
					mid++;
					break;

				case 2:
					tmp = nums[mid];
					nums[mid] = nums[hi];
					nums[hi] = tmp;

					hi--;
					break;
			}
		}
		
		PrintArray(nums);
	}
	
	void NegativeSide(int[] n)
	{
		int temp = 0;
		int j = 0;

		for (int i = 0; i < n.Length; i++)
		{
			if(n[i] < 0)
			{
				temp = n[i];
				n[i] = n[j];
				n[j] = temp;

				j++;
				Debug.LogError(j);
			}
		}

		PrintArray(n);
	}
	
	void RotateK(int[] a, int k)
	{
		int n = a.Length ;
		k = k % n;
		for (int i = 0; i < a.Length; i++)
		{
			if(i < k)
			{
				Debug.LogError(a[n + i - k]);
			}
			else
			{
				Debug.LogError(a[i - k]);
			}
		}
	}
	
	void Union(int[] a, int[] b)
	{
		Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

		for (int i = 0; i < a.Length; i++)
		{
			if (!keyValuePairs.ContainsKey(a[i]))
				keyValuePairs.Add(a[i], a[i]);
		}
		for (int i = 0; i < b.Length; i++)
		{
			if (!keyValuePairs.ContainsKey(b[i]))
				keyValuePairs.Add(b[i], b[i]);
		}

		foreach (var item in keyValuePairs)
		{
			Debug.LogError(item.Key);
		}
	}

	void Intersection(int[] a, int[] b)
	{
		Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
		
		
		
		for (int i = 0; i < a.Length; i++)
		{
			if(!keyValuePairs.ContainsKey(a[i]))
			{
				for (int j = 0; j < b.Length; j++)
				{
					if (!keyValuePairs.ContainsKey(b[j]))
						Debug.LogError(b[j]);
				}
			}
		}
	}
	
	void PrintArray(int[] nums)
	{
		foreach (var item in nums)
		{
			Debug.LogError(item);
		}
	}
}
