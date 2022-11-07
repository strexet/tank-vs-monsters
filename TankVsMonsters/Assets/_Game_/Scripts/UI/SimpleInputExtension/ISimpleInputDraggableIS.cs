using UnityEngine.EventSystems;

namespace UI.SimpleInputExtension
{
	public interface ISimpleInputDraggable : IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
	}
}