﻿using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrexetGames.TankVsMonsters.Scripts.UI.SimpleInputExtension
{
	public class SimpleInputDragListenerIS : MonoBehaviour, ISimpleInputDraggable
	{
		public ISimpleInputDraggable Listener { get; set; }

		private int pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;

		private void Awake()
		{
			var graphic = GetComponent<Graphic>();

			if (!graphic)
			{
				graphic = gameObject.AddComponent<NonDrawingGraphic>();
			}

			graphic.raycastTarget = true;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			Listener.OnPointerDown(eventData);
			pointerId = eventData.pointerId;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (pointerId != eventData.pointerId)
			{
				return;
			}

			Listener.OnDrag(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (pointerId != eventData.pointerId)
			{
				return;
			}

			Listener.OnPointerUp(eventData);
			pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
		}

		public void Stop() => pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
	}
}