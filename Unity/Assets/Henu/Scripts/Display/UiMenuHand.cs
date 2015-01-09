﻿using System.Collections.Generic;
using Henu.Input;
using Henu.State;
using UnityEngine;

namespace Henu.Display {

	/*================================================================================================*/
	public class UiMenuHand : MonoBehaviour {

		private MenuHandState vMenuHand;
		private IList<UiMenuPoint> vUiPoints;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Build(MenuHandState pMenuHand, Renderers pRenderers) {
			vMenuHand = pMenuHand;

			vUiPoints = new List<UiMenuPoint>();

			foreach ( InputPointZone zone in MenuHandState.PointZones ) {
				var pointObj = new GameObject("Point-"+zone);
				pointObj.transform.parent = gameObject.transform;

				UiMenuPoint uiPoint = pointObj.AddComponent<UiMenuPoint>();
				uiPoint.Build(vMenuHand, vMenuHand.GetPointState(zone), pRenderers);
				vUiPoints.Add(uiPoint);
			}

			vMenuHand.OnLevelChange += HandleLevelChange;
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			foreach ( UiMenuPoint uiPoint in vUiPoints ) {
				uiPoint.gameObject.SetActive(vMenuHand.IsActive && uiPoint.IsActive());
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void HandleLevelChange(int pDirection) {
			Update(); //reset point visibility
		}

	}

}