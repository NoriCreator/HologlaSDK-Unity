using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using UnityEngine.EventSystems;

// エディタ上での確認用に、入力操作をキー操作で行えるようにするためのクラス.
// デフォルトではZキーでだんグラの左ボタン、Xキーでだんグラの右ボタンの入力を行える.
public class EditorInputController : MonoBehaviour
{
#if UNITY_EDITOR && (ENABLE_INPUT_SYSTEM || ENABLE_LEGACY_INPUT_MANAGER)
	// 入力を与える対象となるHologlaInputクラス.
	[SerializeField]private Hologla.HologlaInput targetHologlaInput = null;

#if ENABLE_INPUT_SYSTEM
	// Bothの場合もInput Systemを優先する。旧KeyCode設定とは別に保存する.
	[SerializeField]private Key leftButtonInputSystemKey = Key.Z;
	[SerializeField]private Key rightButtonInputSystemKey = Key.X;
#elif ENABLE_LEGACY_INPUT_MANAGER
	// だんグラの左ボタンに対応するキー設定.
	[SerializeField]private KeyCode leftButtonKey = KeyCode.Z;
	// だんグラの右ボタンに対応するキー設定.
	[SerializeField]private KeyCode rightButtonKey = KeyCode.X;
#endif

    // Update is called once per frame
    void Update()
    {
		if( null == targetHologlaInput ){
			return;
		}
		PointerEventData eventData;

		eventData = new PointerEventData(EventSystem.current);
		eventData.button = PointerEventData.InputButton.Left;

		// だんグラの左ボタンに対応するキー(デフォルトZキー)を押した際に、左ボタン用のオブジェクトに押した時用のイベントを飛ばす.
#if ENABLE_INPUT_SYSTEM
		if( Keyboard.current != null && leftButtonInputSystemKey != Key.None && Keyboard.current[leftButtonInputSystemKey].wasPressedThisFrame ){
#elif ENABLE_LEGACY_INPUT_MANAGER
		if( Input.GetKeyDown(leftButtonKey) ){
#endif
			ExecuteEvents.Execute<IPointerDownHandler>(targetHologlaInput.LeftButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerDown((PointerEventData)eventDataArg);
			});
		}
		// だんグラの左ボタンに対応するキー(デフォルトZキー)を離した際に、左ボタン用のオブジェクトに離した時用のイベントを飛ばす.
#if ENABLE_INPUT_SYSTEM
		if( Keyboard.current != null && leftButtonInputSystemKey != Key.None && Keyboard.current[leftButtonInputSystemKey].wasReleasedThisFrame ){
#elif ENABLE_LEGACY_INPUT_MANAGER
		if( Input.GetKeyUp(leftButtonKey) ){
#endif
			ExecuteEvents.Execute<IPointerUpHandler>(targetHologlaInput.LeftButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerUp((PointerEventData)eventDataArg);
			});
			targetHologlaInput.LeftButtonComp.OnPointerClick(eventData);
		}
		// だんグラの右ボタンに対応するキー(デフォルトXキー)を押した際に、右ボタン用のオブジェクトに押した時用のイベントを飛ばす.
#if ENABLE_INPUT_SYSTEM
		if( Keyboard.current != null && rightButtonInputSystemKey != Key.None && Keyboard.current[rightButtonInputSystemKey].wasPressedThisFrame ){
#elif ENABLE_LEGACY_INPUT_MANAGER
		if( Input.GetKeyDown(rightButtonKey) ){
#endif
			ExecuteEvents.Execute<IPointerDownHandler>(targetHologlaInput.RightButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerDown((PointerEventData)eventDataArg);
			});
		}
		// だんグラの右ボタンに対応するキー(デフォルトXキー)を離した際に、右ボタン用のオブジェクトに離した時用のイベントを飛ばす.
#if ENABLE_INPUT_SYSTEM
		if( Keyboard.current != null && rightButtonInputSystemKey != Key.None && Keyboard.current[rightButtonInputSystemKey].wasReleasedThisFrame ){
#elif ENABLE_LEGACY_INPUT_MANAGER
		if( Input.GetKeyUp(rightButtonKey) ){
#endif
			ExecuteEvents.Execute<IPointerUpHandler>(targetHologlaInput.RightButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerUp((PointerEventData)eventDataArg);
			});
			targetHologlaInput.RightButtonComp.OnPointerClick(eventData);
		}

		return;
    }

	private void Awake( )
	{
		if( null == targetHologlaInput ){
			targetHologlaInput = GetComponent<Hologla.HologlaInput>( );
		}

		return;
	}

	// #if UNITY_EDITOR.
#endif
}
