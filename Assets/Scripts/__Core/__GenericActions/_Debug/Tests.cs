using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
	void Test()
	{
		var (n1, n2, _) = Give();
		n1 = 3;
		_ = 3;

		var test = Give();
		test.n1 = 2000;

		var named = (Answer: 42, Message: "The meaning of life");
		named.Answer = 44;

		var unnamed = (42, "The meaning of life");
		unnamed.Item1 = 33;
		//int (n1, n2) = Give();

		(int Count, double Sum, double SumOfSquares) computation = (1, 2f, 3f);
		computation.Count = 20;

		var (t1, t2, t3) = Give();
		t1 = 22;

	}

	public (int n1, int n2, int n3) Give()
	{
		return (1, 2, 3);
	}
}
