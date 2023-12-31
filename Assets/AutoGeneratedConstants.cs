/* ------------------------ */
/* ---- AUTO GENERATED ---- */
/* ---- AVOID TOUCHING ---- */
/* ------------------------ */

using UnityEngine;
using System.Collections.Generic;
using Framework;

public enum LayerName
{
	Default = 0,
	TransparentFX = 1,
	IgnoreRaycast = -1,
	Water = 4,
	UI = 5,
	Ground = 6,
	Player = 7,
	Enemy = 8
}

public enum SortingLayerName
{
	Default = 0
}

public static class Layer
{

	public const int Default = 0;
	public const int TransparentFX = 1;
	public const int IgnoreRaycast = -1;
	public const int Water = 4;
	public const int UI = 5;
	public const int Ground = 6;
	public const int Player = 7;
	public const int Enemy = 8;

}

public static class SortingLayer
{

	public const int Default = 0;

}

public static class Tag
{

	public const string Untagged = "Untagged";
	public const string Respawn = "Respawn";
	public const string Finish = "Finish";
	public const string EditorOnly = "EditorOnly";
	public const string MainCamera = "MainCamera";
	public const string Player = "Player";
	public const string GameController = "GameController";

}

public static partial class LayerMasks
{

	public static readonly LayerMask ALL_LAYERS = ~0;
	public static readonly LayerMask NO_LAYERS = 0;
	public static readonly LayerMask Default = 1;
	public static readonly LayerMask TransparentFX = 2;
	public static readonly LayerMask IgnoreRaycast = -2147483648;
	public static readonly LayerMask Water = 16;
	public static readonly LayerMask UI = 32;
	public static readonly LayerMask Ground = 64;
	public static readonly LayerMask Player = 128;
	public static readonly LayerMask Enemy = 256;

}

public static class CollisionMatrix
{

	public static readonly LayerMask ALL_LAYERS = ~0;
	public static readonly LayerMask NO_LAYERS = 0;
	public static readonly LayerMask DefaultCollisionMask = -1;
	public static readonly LayerMask TransparentFXCollisionMask = -1;
	public static readonly LayerMask IgnoreRaycastCollisionMask = -1;
	public static readonly LayerMask WaterCollisionMask = -1;
	public static readonly LayerMask UICollisionMask = -1;
	public static readonly LayerMask GroundCollisionMask = -1;
	public static readonly LayerMask PlayerCollisionMask = -1;
	public static readonly LayerMask EnemyCollisionMask = -1;

}

public static class SceneNames
{

	public const string SampleScene = "SampleScene";

}

[CreateAssetMenu(fileName = "Fluid Type", menuName = "Scriptable Enum/Fluid Type")]
public partial class FluidType
{

	public static FluidType[] AllFluidTypes { get { if (__allFluidTypes == null) __allFluidTypes = GetValues<FluidType>(); return __allFluidTypes; } }
	public static FluidType Blood { get { if (__blood == null) __blood = GetValue<FluidType>("Blood"); return __blood; } }
	public static FluidType Goblin { get { if (__goblin == null) __goblin = GetValue<FluidType>("Goblin"); return __goblin; } }
	public static FluidType Rat { get { if (__rat == null) __rat = GetValue<FluidType>("Rat"); return __rat; } }
	public static FluidType Semen { get { if (__semen == null) __semen = GetValue<FluidType>("Semen"); return __semen; } }
	
	protected static FluidType[] __allFluidTypes;
	protected static FluidType __blood;
	protected static FluidType __goblin;
	protected static FluidType __rat;
	protected static FluidType __semen;

}

[System.Serializable]
public class FluidTypeMask : ScriptableEnumMask<FluidType> {}