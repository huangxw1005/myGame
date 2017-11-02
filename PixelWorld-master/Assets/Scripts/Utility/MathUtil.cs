using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class MathUtil {

	// point distance to segment
	// ref: https://zhuanlan.zhihu.com/p/23903445
	public static float PointToSegmentSqrDistance(Vector2 p, Vector2 s0, Vector2 s1) {
		Vector2 u = s1 - s0;
		float t = Vector2.Dot(p-s0, u) / u.sqrMagnitude;
		return (p - (s0 + Mathf.Clamp(t, 0, 1) * u)).sqrMagnitude;
	}

	// circle vs circle
	public static bool CheckCircleIntersect(Vector3 sourcePos, float sourceRadius, Vector3 targetPos, float targetRadius) {
		Vector3 v = targetPos - sourcePos;
		v.y = 0.0f;

		float dist = sourceRadius + targetRadius;
		return  v.sqrMagnitude < dist * dist;
	}

	// sector vs circle （180度内）
	// 可用SAT优化
	public static bool CheckSectorIntersect(Vector3 sourcePos, Vector3 dir, float angle, float sourceRadius, Vector3 targetPos, float targetRadius) {
		Vector3 v = targetPos - sourcePos;
		v.y = 0.0f;

		dir.Normalize();

		float targetRadiusSqr = targetRadius * targetRadius;

		// sector在circle内
		if (  v.sqrMagnitude < targetRadiusSqr ) {
			return true;
		}

		// sector 于circle 足够远
		float dist = sourceRadius + targetRadius;
		if (  v.sqrMagnitude >= dist * dist ) {
			return false;
		}

		// 检查扇形两边和v夹角情况
		Vector3 leftV = Quaternion.Euler(0, -angle, 0) * dir;
		leftV.y = 0.0f;
		Vector3 rightV = Quaternion.Euler(0, angle, 0) * dir;
		rightV.y = 0.0f;

		Vector3 crossLeft = Vector3.Cross(leftV, v);
		Vector3 crossRight = Vector3.Cross(rightV, v);

		if (crossLeft.y * crossRight.y < 0) {
			// circle 在扇形中间区域内
			return true;
		} else {
			// circle 在扇形两侧
			float dotLeft = Vector3.Dot(leftV, v);
			float dotRight = Vector3.Dot(rightV, v);

			// left
			if (PointToSegmentSqrDistance(targetPos, sourcePos, leftV*sourceRadius) < targetRadiusSqr) {
				return true;
			}
			// right
			if (PointToSegmentSqrDistance(targetPos, sourcePos, rightV*sourceRadius) < targetRadiusSqr) {
				return true;
			}
		}

		return false;
	}
}