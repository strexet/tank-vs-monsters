using UnityEngine.EventSystems;

namespace StrexetGames.TankVsMonsters.Scripts.UI.SimpleInputExtension
{
    public interface ISimpleInputDraggable : IPointerDownHandler, IDragHandler, IPointerUpHandler { }
}