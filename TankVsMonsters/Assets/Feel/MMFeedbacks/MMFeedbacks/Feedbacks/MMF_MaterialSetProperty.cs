using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	/// <summary>
	/// This feedback will let you set a property on the target renderer's material
	/// </summary>
	[AddComponentMenu("")]
	[FeedbackHelp("This feedback will let you set a property on the target renderer's material")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks")]
	[FeedbackPath("Renderer/Material Set Property")]
	public class MMF_MaterialSetProperty : MMF_Feedback
	{
		/// a static bool used to disable all feedbacks of this type at once
		public static bool FeedbackTypeAuthorized = true;
		#if UNITY_EDITOR
		public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.UIColor; } }
		public override bool EvaluateRequiresSetup() { return (TargetRenderer == null); }
		public override string RequiredTargetText { get { return TargetRenderer != null ? TargetRenderer.name : "";  } }
		public override string RequiresSetupText { get { return "This feedback requires that a TargetRenderer be set to be able to work properly. You can set one below."; } }
		#endif
		public override bool HasAutomatedTargetAcquisition => true;
		protected override void AutomateTargetAcquisition() => TargetRenderer = FindAutomatedTarget<Renderer>();
		
		public enum PropertyTypes { Color, Float, Integer, Texture, TextureOffset, TextureScale, Vector }
		
		[MMFInspectorGroup("Material", true, 12, true)]
		/// the renderer to change the material on
		[Tooltip("the renderer to change the material on")]
		public Renderer TargetRenderer;
		/// the ID of the material to target on the renderer
		[Tooltip("the ID of the material to target on the renderer")]
		public int MaterialID = 0;
		/// the ID of the property to set, as exposed by the Visual Effect Graph
		[Tooltip("the ID of the property to set, as exposed by the Visual Effect Graph")] 
		public string PropertyID;
		/// the type of the property to set
		[Tooltip("the type of the property to set")]
		public PropertyTypes PropertyType = PropertyTypes.Float;
		
		/// if the property is a color, the new color to set
		[Tooltip("if the property is a color, the new color to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.Color)]
		public Color NewColor = Color.red;
		/// if the property is a float, the new float to set
		[Tooltip("if the property is a float, the new float to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.Float)]
		public float NewFloat = 1f;
		/// if the property is an int, the new int to set
		[Tooltip("if the property is an int, the new int to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.Integer)]
		public int NewInt;
		/// if the property is a texture, the new texture to set
		[Tooltip("if the property is a texture, the new texture to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.Texture)]
		public Texture NewTexture;
		/// if the property is a texture offset, the new offset to set
		[Tooltip("if the property is a texture offset, the new offset to set")] 
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.TextureOffset)]
		public Vector2 NewOffset;
		/// if the property is a texture scale, the new scale to set
		[Tooltip("if the property is a texture scale, the new scale to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.TextureScale)]
		public Vector2 NewScale;
		/// if the property is a vector, the new vector4 to set
		[Tooltip("if the property is a vector4, the new vector4 to set")]
		[MMFEnumCondition("PropertyType", (int)PropertyTypes.Vector)]
		public Vector4 NewVector;
		
		protected int _propertyID;
		protected Color _initialColor;
		protected float _initialFloat;
		protected int _initialInt;
		protected Texture _initialTexture;
		protected Vector2 _initialOffset;
		protected Vector2 _initialScale;
		protected Vector4 _initialVector;
		
		/// <summary>
		/// On init we turn the sprite renderer off if needed
		/// </summary>
		/// <param name="owner"></param>
		protected override void CustomInitialization(MMF_Player owner)
		{
			base.CustomInitialization(owner);

			_propertyID = Shader.PropertyToID(PropertyID);
			
			// we store the initial value of the property based on its type
			if (Active)
			{
				switch (PropertyType)
				{
					case PropertyTypes.Color:
						_initialColor = TargetRenderer.materials[MaterialID].GetColor(_propertyID);
						break;
					case PropertyTypes.Float:
						_initialFloat = TargetRenderer.materials[MaterialID].GetFloat(_propertyID);
						break;
					case PropertyTypes.Integer:
						_initialInt = TargetRenderer.materials[MaterialID].GetInt(_propertyID);
						break;
					case PropertyTypes.Texture:
						_initialTexture = TargetRenderer.materials[MaterialID].GetTexture(_propertyID);
						break;
					case PropertyTypes.TextureOffset:
						_initialOffset = TargetRenderer.materials[MaterialID].GetTextureOffset(_propertyID);
						break;
					case PropertyTypes.TextureScale:
						_initialScale = TargetRenderer.materials[MaterialID].GetTextureScale(_propertyID);
						break;
					case PropertyTypes.Vector:
						_initialVector = TargetRenderer.materials[MaterialID].GetVector(_propertyID);
						break;
				}
			}
		}
		
		/// <summary>
		/// On play we turn raycastTarget on or off
		/// </summary>
		/// <param name="position"></param>
		/// <param name="feedbacksIntensity"></param>
		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
		{
			if (!Active || !FeedbackTypeAuthorized)
			{
				return;
			}

			if (TargetRenderer == null)
			{
				return;
			}

			switch (PropertyType)
			{
				case PropertyTypes.Color:
					TargetRenderer.materials[MaterialID].SetColor(_propertyID, NewColor);
					break;
				case PropertyTypes.Float:
					TargetRenderer.materials[MaterialID].SetFloat(_propertyID, NewFloat);
					break;
				case PropertyTypes.Integer:
					TargetRenderer.materials[MaterialID].SetInt(_propertyID, NewInt);
					break;
				case PropertyTypes.Texture:
					TargetRenderer.materials[MaterialID].SetTexture(_propertyID, NewTexture);
					break;
				case PropertyTypes.TextureOffset:
					TargetRenderer.materials[MaterialID].SetTextureOffset(_propertyID, NewOffset);
					break;
				case PropertyTypes.TextureScale:
					TargetRenderer.materials[MaterialID].SetTextureScale(_propertyID, NewScale);
					break;
				case PropertyTypes.Vector:
					TargetRenderer.materials[MaterialID].SetVector(_propertyID, NewVector);
					break;
			}
		}
		
		/// <summary>
		/// On restore, we restore our initial state
		/// </summary>
		protected override void CustomRestoreInitialValues()
		{
			if (!Active || !FeedbackTypeAuthorized)
			{
				return;
			}
			// we restore initial values based on the property type
			switch (PropertyType)
			{
				case PropertyTypes.Color:
					TargetRenderer.materials[MaterialID].SetColor(_propertyID, _initialColor);
					break;
				case PropertyTypes.Float:
					TargetRenderer.materials[MaterialID].SetFloat(_propertyID, _initialFloat);
					break;
				case PropertyTypes.Integer:
					TargetRenderer.materials[MaterialID].SetInt(_propertyID, _initialInt);
					break;
				case PropertyTypes.Texture:
					TargetRenderer.materials[MaterialID].SetTexture(_propertyID, _initialTexture);
					break;
				case PropertyTypes.TextureOffset:
					TargetRenderer.materials[MaterialID].SetTextureOffset(_propertyID, _initialOffset);
					break;
				case PropertyTypes.TextureScale:
					TargetRenderer.materials[MaterialID].SetTextureScale(_propertyID, _initialScale);
					break;
				case PropertyTypes.Vector:
					TargetRenderer.materials[MaterialID].SetVector(_propertyID, _initialVector);
					break;
			}
		}
	}
}